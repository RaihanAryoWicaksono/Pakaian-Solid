using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PakaianApi.Data;
using PakaianApi.Models;
using PakaianApi.Services;
using PakaianAPI.Models;
using PakaianLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KeranjangController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly KatalogPakaian _katalog;

        public KeranjangController(ApplicationDbContext context)
        {
            _context = context;
            _katalog = new KatalogPakaian(context);
        }
            
        // GET: api/keranjang
        [HttpGet]
        public IActionResult GetKeranjang()
        {
            var items = _context.Keranjang
                .Include(k => k.Pakaian)
                .Where(k => k.Pakaian != null)
                .ToList();

            var keranjangDto = new KeranjangDto
            {
                Items = items.Select(k => new PakaianDto
                {
                    Kode = k.Pakaian.Kode,
                    Nama = k.Pakaian.Nama,
                    Kategori = k.Pakaian.Kategori,
                    Warna = k.Pakaian.Warna,
                    Ukuran = k.Pakaian.Ukuran,
                    Harga = k.Pakaian.Harga,
                    Stok = k.Pakaian.Stok,
                    Status = k.Pakaian.Status
                }).ToList(),

                TotalHarga = items.Sum(k => k.Pakaian.Harga * k.Quantity),
                JumlahItem = items.Sum(k => k.Quantity)
            };

            return Ok(keranjangDto);
        }

        // POST: api/keranjang
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartDto dto)
        {
            var existing = _context.Keranjang.FirstOrDefault(k => k.KodePakaian == dto.KodePakaian);
            if (existing != null)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Produk sudah ada di keranjang."
                });
            }

            var pakaian = _katalog.CariPakaianByKode(dto.KodePakaian);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = "Pakaian tidak ditemukan."
                });
            }

            if (pakaian.Stok < dto.Quantity)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Stok tidak mencukupi."
                });
            }

            if (!pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang))
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Gagal menambahkan ke keranjang."
                });
            }

            var keranjang = new Keranjang
            {
                KodePakaian = dto.KodePakaian,
                Quantity = dto.Quantity
            };

            _context.Keranjang.Add(keranjang);
            _context.SaveChanges();

            return Ok(new { Message = "Berhasil ditambahkan ke keranjang." });
        }

        // DELETE: api/keranjang/{id}
        // DELETE: api/keranjang/{id}
        [HttpDelete("{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            var item = _context.Keranjang
                .Include(k => k.Pakaian)
                .FirstOrDefault(k => k.Id == id);

            if (item == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = "Item tidak ditemukan di keranjang."
                });
            }

            // Cek apakah relasi Pakaian berhasil dimuat
            if (item.Pakaian != null)
            {
                item.Pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
            }

            _context.Keranjang.Remove(item);
            _context.SaveChanges();

            return Ok(new { Message = "Item berhasil dihapus dari keranjang." });
        }

        // DELETE: api/keranjang
        [HttpDelete]
        public IActionResult ClearCart()
        {
            var items = _context.Keranjang
                .Include(k => k.Pakaian)
                .ToList();

            foreach (var item in items)
            {
                // Cek apakah relasi Pakaian berhasil dimuat
                if (item.Pakaian != null)
                {
                    item.Pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
                }
            }

            _context.Keranjang.RemoveRange(items);
            _context.SaveChanges();

            return Ok(new { Message = "Keranjang dikosongkan." });
        }

    }
}

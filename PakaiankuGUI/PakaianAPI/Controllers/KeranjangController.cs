// PakaianApi/Controllers/KeranjangController.cs
using Microsoft.AspNetCore.Mvc;
using PakaianApi.Data;
using PakaianApi.Models;
using PakaianLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KeranjangController : ControllerBase
    {
        private readonly KeranjangBelanja<Pakaian> _keranjang;
        private readonly ApplicationDbContext _context;

        public KeranjangController(KeranjangBelanja<Pakaian> keranjang, ApplicationDbContext context)
        {
            _keranjang = keranjang;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        public IActionResult GetKeranjang()
        {
            var items = _keranjang.GetSemuaItem();
            var keranjangDto = new KeranjangDto
            {
                Items = items.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                JumlahItem = _keranjang.JumlahItem()
            };

            return Ok(keranjangDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == addToCartDto.KodePakaian);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {addToCartDto.KodePakaian} tidak ditemukan"
                });
            }

            if (pakaian.Stok <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Pakaian '{pakaian.Nama}' stok tidak tersedia (Stok: {pakaian.Stok})"
                });
            }

            if (pakaian.Status != StatusPakaian.Tersedia)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Pakaian '{pakaian.Nama}' tidak dapat ditambahkan ke keranjang. Status saat ini: {pakaian.Status}"
                });
            }

            bool prosesAksi = pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);
            if (!prosesAksi)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Status saat ini: {pakaian.Status}"
                });
            }

            pakaian.Stok--;
            await _context.SaveChangesAsync();

            _keranjang.TambahKeKeranjang(pakaian);

            var items = _keranjang.GetSemuaItem();
            var keranjangDto = new KeranjangDto
            {
                Items = items.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                JumlahItem = _keranjang.JumlahItem()
            };

            return Ok(keranjangDto);
        }

        [HttpDelete("{index}")]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> RemoveFromCart(int index)
        {
            var items = _keranjang.GetSemuaItem();
            if (index < 0 || index >= items.Count)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Index item {index} tidak valid"
                });
            }

            var pakaian = items[index];

            pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
            pakaian.Stok++;
            await _context.SaveChangesAsync();

            bool berhasil = _keranjang.KeluarkanDariKeranjangByIndex(index);
            if (!berhasil)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal menghapus item dari keranjang"
                });
            }

            var updatedItems = _keranjang.GetSemuaItem();
            var keranjangDto = new KeranjangDto
            {
                Items = updatedItems.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                JumlahItem = _keranjang.JumlahItem()
            };

            return Ok(keranjangDto);
        }

        [HttpPost("checkout")]
        [ProducesResponseType(typeof(CheckoutResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
        {
            if (_keranjang.JumlahItem() == 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Keranjang kosong. Tidak ada yang bisa di-checkout."
                });
            }

            var items = _keranjang.GetSemuaItem();
            foreach (var item in items)
            {
                var dbPakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == item.Kode);
                if (dbPakaian == null || dbPakaian.Stok < 0 || dbPakaian.Status != StatusPakaian.DalamKeranjang)
                {
                    if (dbPakaian != null && dbPakaian.Stok <= 0)
                    {
                        return BadRequest(new ErrorResponse
                        {
                            Status = 400,
                            Message = $"Pakaian '{item.Nama}' tidak dapat di-checkout. Stok habis. Silakan hapus dari keranjang."
                        });
                    }
                    else if (dbPakaian != null && dbPakaian.Status != StatusPakaian.DalamKeranjang)
                    {
                        return BadRequest(new ErrorResponse
                        {
                            Status = 400,
                            Message = $"Pakaian '{item.Nama}' tidak dapat di-checkout. Status berubah. Stok: {dbPakaian.Stok}, Status: {dbPakaian.Status}"
                        });
                    }
                    else
                    {
                        return BadRequest(new ErrorResponse
                        {
                            Status = 400,
                            Message = $"Pakaian '{item.Nama}' tidak dapat di-checkout. Item tidak ditemukan atau dalam status tidak valid."
                        });
                    }
                }
            }

            var orderId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            foreach (var item in new List<Pakaian>(items))
            {
                item.ProsesAksi(AksiPakaian.Pesan);
                item.ProsesAksi(AksiPakaian.Bayar);
                item.ProsesAksi(AksiPakaian.Kirim);
                item.ProsesAksi(AksiPakaian.TerimaPakaian);
                await _context.SaveChangesAsync();
            }

            var checkoutResponse = new CheckoutResponseDto
            {
                OrderId = orderId,
                TanggalPemesanan = DateTime.Now,
                Items = items.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                StatusPemesanan = "DalamPengiriman",
                AlamatPengiriman = checkoutDto.AlamatPengiriman,
                MetodePembayaran = checkoutDto.MetodePembayaran
            };

            _keranjang.KosongkanKeranjang();

            return Ok(checkoutResponse);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        public IActionResult ClearCart()
        {
            var items = _keranjang.GetSemuaItem();
            foreach (var item in items)
            {
                item.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
                item.Stok++;
            }
            _context.SaveChangesAsync();

            _keranjang.KosongkanKeranjang();

            var keranjangDto = new KeranjangDto
            {
                Items = new List<PakaianDto>(),
                TotalHarga = 0,
                JumlahItem = 0
            };

            return Ok(keranjangDto);
        }

        private PakaianDto MapToDto(Pakaian pakaian)
        {
            return new PakaianDto
            {
                Kode = pakaian.Kode,
                Nama = pakaian.Nama,
                Kategori = pakaian.Kategori,
                Warna = pakaian.Warna,
                Ukuran = pakaian.Ukuran,
                Harga = pakaian.Harga,
                Stok = pakaian.Stok,
                Status = pakaian.Status
            };
        }
    }
}

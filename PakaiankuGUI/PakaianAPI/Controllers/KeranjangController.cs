using Microsoft.AspNetCore.Mvc;
using Pakaianku;
using PakaianApi.Models;
using PakaianLib;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KeranjangController : ControllerBase
    {
        private readonly KeranjangBelanja<Pakaian> _keranjang;
        private readonly KatalogPakaian _katalog;

        public KeranjangController(KeranjangBelanja<Pakaian> keranjang, KatalogPakaian katalog)
        {
            _keranjang = keranjang;
            _katalog = katalog;
        }

        /// <summary>
        /// Mendapatkan isi keranjang belanja
        /// </summary>
        /// <returns>Informasi keranjang belanja</returns>
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

        /// <summary>
        /// Menambahkan pakaian ke keranjang belanja
        /// </summary>
        /// <param name="addToCartDto">Data pakaian yang akan ditambahkan</param>
        /// <returns>Keranjang belanja yang sudah diupdate</returns>
        [HttpPost]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var pakaian = _katalog.CariPakaianByKode(addToCartDto.KodePakaian);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {addToCartDto.KodePakaian} tidak ditemukan"
                });
            }

            // Periksa stok dan status pakaian
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

            // Lakukan aksi pakaian: menambahkan ke keranjang
            bool prosesAksi = pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);
            if (!prosesAksi)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Status saat ini: {pakaian.Status}"
                });
            }

            _keranjang.TambahKeKeranjang(pakaian);

            // Return updated cart
            var items = _keranjang.GetSemuaItem();
            var keranjangDto = new KeranjangDto
            {
                Items = items.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                JumlahItem = _keranjang.JumlahItem()
            };

            return Ok(keranjangDto);
        }

        /// <summary>
        /// Menghapus pakaian dari keranjang belanja
        /// </summary>
        /// <param name="index">Index item yang akan dihapus</param>
        /// <returns>Keranjang belanja yang sudah diupdate</returns>
        [HttpDelete("{index}")]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult RemoveFromCart(int index)
        {
            // Ambil item yang akan dihapus untuk mengembalikan status ke Tersedia
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

            // Kembalikan status pakaian
            pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);

            // Hapus dari keranjang
            bool berhasil = _keranjang.KeluarkanDariKeranjangByIndex(index);
            if (!berhasil)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal menghapus item dari keranjang"
                });
            }

            // Return updated cart
            var updatedItems = _keranjang.GetSemuaItem();
            var keranjangDto = new KeranjangDto
            {
                Items = updatedItems.Select(MapToDto).ToList(),
                TotalHarga = _keranjang.HitungTotal(),
                JumlahItem = _keranjang.JumlahItem()
            };

            return Ok(keranjangDto);
        }

        /// <summary>
        /// Proses checkout keranjang belanja
        /// </summary>
        /// <param name="checkoutDto">Data checkout</param>
        /// <returns>Hasil checkout</returns>
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(CheckoutResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult Checkout([FromBody] CheckoutDto checkoutDto)
        {
            if (_keranjang.JumlahItem() == 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Keranjang kosong. Tidak ada yang bisa di-checkout."
                });
            }

            // Periksa apakah semua item masih memiliki stok yang cukup
            var items = _keranjang.GetSemuaItem();
            foreach (var item in items)
            {
                if (item.Stok <= 0 || item.Status != StatusPakaian.DalamKeranjang)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = 400,
                        Message = $"Pakaian '{item.Nama}' tidak dapat di-checkout. Stok: {item.Stok}, Status: {item.Status}"
                    });
                }
            }

            var orderId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            foreach (var item in new List<Pakaian>(items))
            {
                item.ProsesAksi(AksiPakaian.Pesan);
                item.ProsesAksi(AksiPakaian.Bayar);
                item.ProsesAksi(AksiPakaian.Kirim);
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

            // Kosongkan keranjang
            _keranjang.KosongkanKeranjang();

            return Ok(checkoutResponse);
        }

        /// <summary>
        /// Mengosongkan keranjang belanja
        /// </summary>
        /// <returns>Keranjang belanja yang sudah dikosongkan</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        public IActionResult ClearCart()
        {
            // Kembalikan status semua item di keranjang ke Tersedia
            var items = _keranjang.GetSemuaItem();
            foreach (var item in items)
            {
                item.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
            }

            _keranjang.KosongkanKeranjang();

            // Return empty cart
            var keranjangDto = new KeranjangDto
            {
                Items = new List<PakaianDto>(),
                TotalHarga = 0,
                JumlahItem = 0
            };

            return Ok(keranjangDto);
        }

        // Helper method untuk mapping Pakaian ke PakaianDto
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
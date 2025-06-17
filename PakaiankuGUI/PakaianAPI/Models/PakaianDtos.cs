// Models/DTOs.cs
using PakaianLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PakaianApi.Models
{
    // DTO untuk output informasi pakaian
    public class PakaianDto
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public StatusPakaian Status { get; set; }
    }

    // DTO untuk input pembuatan pakaian baru
    public class CreatePakaianDto
    {
        [Required]
        public string Kode { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Kategori { get; set; }

        [Required]
        public string Warna { get; set; }

        [Required]
        public string Ukuran { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal Harga { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stok { get; set; }
    }

    // DTO untuk input pembaruan pakaian
    public class UpdatePakaianDto
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal? Harga { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int? Stok { get; set; }
    }

    // DTO untuk keranjang belanja
    public class KeranjangDto
    {
        public List<PakaianDto> Items { get; set; } = new List<PakaianDto>();
        public decimal TotalHarga { get; set; }
        public int JumlahItem { get; set; }
    }

    // DTO untuk menambahkan item ke keranjang
    public class AddToCartDto
    {
        [Required]
        public string KodePakaian { get; set; }
    }

    // DTO untuk checkout
    public class CheckoutDto
    {
        [Required]
        public string AlamatPengiriman { get; set; }

        [Required]
        public string MetodePembayaran { get; set; }
    }

    // DTO untuk response checkout
    public class CheckoutResponseDto
    {
        public string OrderId { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public List<PakaianDto> Items { get; set; }
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; }
        public string AlamatPengiriman { get; set; }
        public string MetodePembayaran { get; set; }
    }

    // DTO untuk proses aksi pakaian
    public class ProsesAksiDto
    {
        [Required]
        public string KodePakaian { get; set; }

        [Required]
        public AksiPakaian Aksi { get; set; }
    }

    // DTO untuk respons kesalahan
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}

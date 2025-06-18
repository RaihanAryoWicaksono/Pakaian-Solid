// PakaianForm/Models/ProductAndApiDtos.cs
// FILE INI HARUS BERISI SEMUA DEFINISI DTO TERKAIT PRODUK, KERANJANG, DAN RESPONS/PERMINTAAN API UMUM.
// PASTIKAN TIDAK ADA DEFINISI DTO INI DI FILE LAIN DALAM NAMESPACE PakaianForm.Models.

using PakaianLib; // Untuk StatusPakaian, AksiPakaian (enum dari PakaianLib)
using System;
using System.Collections.Generic;
using DataAnnotations = System.ComponentModel.DataAnnotations; // Alias untuk mengatasi ambiguitas Required/Range

namespace PakaianForm.Models // PASTIKAN NAMESPACE INI BENAR
{
    // DTO untuk output informasi pakaian
    public class PakaianDtos
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public PakaianLib.StatusPakaian Status { get; set; } // Menggunakan StatusPakaian dari PakaianLib
    }

    // DTO untuk input pembuatan pakaian baru
    public class CreatePakaianDto
    {
        [DataAnnotations.Required]
        public string Kode { get; set; }

        [DataAnnotations.Required]
        public string Nama { get; set; }

        [DataAnnotations.Required]
        public string Kategori { get; set; }

        [DataAnnotations.Required]
        public string Warna { get; set; }

        [DataAnnotations.Required]
        public string Ukuran { get; set; }

        [DataAnnotations.Required]
        [DataAnnotations.Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal Harga { get; set; }

        [DataAnnotations.Required]
        [DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stok { get; set; }
    }

    // DTO untuk input pembaruan pakaian
    public class UpdatePakaianDto
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }

        [DataAnnotations.Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal? Harga { get; set; }

        [DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int? Stok { get; set; }
    }

    // DTO untuk item dalam keranjang
    public class KeranjangItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Jika Anda memiliki ID User di API
        public string KodePakaian { get; set; } // Menggunakan KodePakaian sebagai FK
        public PakaianDtos Pakaian { get; set; } // Objek PakaianDto yang terkait
        public int Quantity { get; set; }
        public decimal TotalHargaItem { get; set; } // Total harga untuk item spesifik ini (Harga * Quantity)
        public DateTime CreatedAt { get; set; }
    }

    // DTO untuk representasi keranjang belanja keseluruhan
    public class KeranjangDto
    {
        public List<KeranjangItemDto> Items { get; set; } = new List<KeranjangItemDto>();
        public decimal TotalHarga { get; set; }
        public int JumlahItem { get; set; }
    }

    // DTO untuk menambahkan item ke keranjang
    public class AddToCartDto
    {
        [DataAnnotations.Required]
        public string KodePakaian { get; set; }
        // public int Quantity { get; set; } = 1; // Opsional: jika API AddToCart menerima kuantitas
    }

    // DTO untuk checkout
    public class CheckoutDto
    {
        [DataAnnotations.Required]
        public string AlamatPengiriman { get; set; }

        [DataAnnotations.Required]
        public string MetodePembayaran { get; set; }
    }

    // DTO untuk response checkout
    public class CheckoutResponseDto
    {
        public string OrderId { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public List<KeranjangItemDto> Items { get; set; } // Menggunakan KeranjangItemDto
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; }
        public string AlamatPengiriman { get; set; }
        public string MetodePembayaran { get; set; }
    }

    // DTO untuk proses aksi pakaian (digunakan untuk API ProsesAksi)
    public class ProsesAksiDto
    {
        [DataAnnotations.Required]
        public string KodePakaian { get; set; }

        [DataAnnotations.Required]
        public PakaianLib.AksiPakaian Aksi { get; set; } // Menggunakan AksiPakaian dari PakaianLib
    }

    // DTO untuk respons kesalahan dari API
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}

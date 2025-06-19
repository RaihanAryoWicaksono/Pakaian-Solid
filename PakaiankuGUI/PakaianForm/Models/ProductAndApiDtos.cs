// PakaianForm/Models/ProductAndApiDtos.cs
using PakaianLib;
using System;
using System.Collections.Generic;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace PakaianForm.Models
{
    public class PakaianDtos
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public PakaianLib.StatusPakaian Status { get; set; }
    }

    public class CreatePakaianDto
    {
        [DataAnnotations.Required] public string Kode { get; set; }
        [DataAnnotations.Required] public string Nama { get; set; }
        [DataAnnotations.Required] public string Kategori { get; set; }
        [DataAnnotations.Required] public string Warna { get; set; }
        [DataAnnotations.Required] public string Ukuran { get; set; }
        [DataAnnotations.Required][DataAnnotations.Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")] public decimal Harga { get; set; }
        [DataAnnotations.Required][DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")] public int Stok { get; set; }
    }

    public class UpdatePakaianDto
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        [DataAnnotations.Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")] public decimal? Harga { get; set; }
        [DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")] public int? Stok { get; set; }
        public PakaianLib.StatusPakaian? Status { get; set; }
    }

    // --- KERANJANG DTO UNTUK WINFORMS (Sinkron dengan API DB) ---
    public class KeranjangItemDto
    {
        public int Id { get; set; } // ID Item Keranjang dari Database
        public int UserId { get; set; }
        public string KodePakaian { get; set; }
        public PakaianDtos Pakaian { get; set; } // Menggunakan PakaianDtos
        public int Quantity { get; set; }
        public decimal HargaSatuan { get; set; } // Dari API
        public decimal TotalHargaItem { get; set; } // Dari API
        public DateTime AddedAt { get; set; } // Dari API
    }

    public class KeranjangDto
    {
        public List<KeranjangItemDto> Items { get; set; } = new List<KeranjangItemDto>();
        public decimal TotalHarga { get; set; }
        public int JumlahItem { get; set; }
    }
    // --- AKHIR KERANJANG DTO UNTUK WINFORMS ---

    public class AddToCartDto
    {
        [DataAnnotations.Required] public string KodePakaian { get; set; }
    }

    public class CheckoutDto
    {
        [DataAnnotations.Required] public string AlamatPengiriman { get; set; }
        [DataAnnotations.Required] public string MetodePembayaran { get; set; }
    }

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

    public class ProsesAksiDto
    {
        [DataAnnotations.Required] public string KodePakaian { get; set; }
        [DataAnnotations.Required] public PakaianLib.AksiPakaian Aksi { get; set; }
    }

    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}

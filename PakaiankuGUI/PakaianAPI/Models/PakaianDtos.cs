// PakaianApi/Models/PakaianDto.cs
using PakaianLib; // Untuk StatusPakaian, AksiPakaian
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
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

    public class CreatePakaianDto
    {
        [Required] public string Kode { get; set; }
        [Required] public string Nama { get; set; }
        [Required] public string Kategori { get; set; }
        [Required] public string Warna { get; set; }
        [Required] public string Ukuran { get; set; }
        [Required] public decimal Harga { get; set; }
        [Required] public int Stok { get; set; }
    }

    public class UpdatePakaianDto
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")] public decimal? Harga { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")] public int? Stok { get; set; }
        public StatusPakaian? Status { get; set; }
    }

    public class ProsesAksiDto
    {
        [Required] public string KodePakaian { get; set; }
        [Required] public AksiPakaian Aksi { get; set; }
    }

    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    // --- KERANJANG DTO UNTUK API (Sesuai dengan Entitas DB) ---
    public class KeranjangItemDto
    {
        public int Id { get; set; }
        public string KodePakaian { get; set; }
        public PakaianDto Pakaian { get; set; }
        public int Quantity { get; set; }
        public decimal HargaSatuan { get; set; }
        public decimal TotalHargaItem { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class KeranjangDto
    {
        public List<KeranjangItemDto> Items { get; set; } = new List<KeranjangItemDto>();
        public decimal TotalHarga { get; set; }
        public int JumlahItem { get; set; }
    }

    public class AddToCartDto
    {
        [Required] public string KodePakaian { get; set; }
    }

    public class CheckoutDto
    {
        [Required] public string AlamatPengiriman { get; set; }
        [Required] public string MetodePembayaran { get; set; }
    }

    public class CheckoutResponseDto
    {
        public string OrderId { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public List<KeranjangItemDto> Items { get; set; }
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; }
        public string AlamatPengiriman { get; set; }
        public string MetodePembayaran { get; set; }
    }
}

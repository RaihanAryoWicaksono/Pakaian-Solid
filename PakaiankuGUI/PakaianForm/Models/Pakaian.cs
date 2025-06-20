﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace PakaianForm.Models
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

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal Harga { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stok { get; set; }
    }

    public class UpdatePakaianDto
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        [DataAnnotations.Range(1, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")] public decimal? Harga { get; set; }
        [DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")] public int? Stok { get; set; }
        public StatusPakaian Status { get; set; }
    }

    public enum StatusPakaian
    {
        Tersedia,
        TidakTersedia,
        DalamKeranjang,
        Dipesan,
        Dibayar,
        DalamPengiriman,
        Diterima
    }

    public class AddToCartDto
    {
        public string KodePakaian { get; set; }
        public int Quantity { get; set; } = 1;
        //public string Warna { get; set; }
        //public string Ukuran { get; set; }
    }

    public class KeranjangItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string KodePakaian { get; set; }
        public PakaianDto Pakaian { get; set; }
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

    public class CheckoutDto
    {
        public string AlamatPengiriman { get; set; }
        public string MetodePembayaran { get; set; }
    }

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
}
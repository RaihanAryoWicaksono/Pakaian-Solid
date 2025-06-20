// PakaianApi/Models/KeranjangItem.cs
using PakaianAPI.Models;
using PakaianLib; // Untuk Pakaian (entitas domain inti)
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
    public class KeranjangItem // Ini adalah entitas database
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID unik untuk item keranjang

        public int KeranjangBelanjaId { get; set; } // Foreign Key ke KeranjangBelanja
        [ForeignKey("KeranjangBelanjaId")]
        public KeranjangBelanja KeranjangBelanja { get; set; } // Navigation property

        [Required]
        public string PakaianKode { get; set; } // Foreign Key ke Pakaian (entitas domain inti)
        [ForeignKey("PakaianKode")]
        public Pakaian Pakaian { get; set; } // Navigation property ke pakaian

        public int Quantity { get; set; }
        public decimal HargaSatuan { get; set; } // Harga pakaian saat item ditambahkan

        [NotMapped]
        public decimal TotalHargaItem => HargaSatuan * Quantity;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

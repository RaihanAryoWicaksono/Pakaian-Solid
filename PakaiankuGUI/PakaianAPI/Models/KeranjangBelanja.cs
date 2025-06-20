// PakaianApi/Models/KeranjangBelanja.cs
using PakaianAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
    public class KeranjangBelanja // Ini adalah entitas database
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; } // Foreign Key ke tabel User
        [ForeignKey("UserId")]
        public User User { get; set; } // Navigation property

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<KeranjangItem> Items { get; set; } = new List<KeranjangItem>(); // Item-item dalam keranjang ini
    }
}

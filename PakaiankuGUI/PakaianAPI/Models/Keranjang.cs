using PakaianAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
    public class Keranjang
    {
        public int Id { get; set; }
        public string KodePakaian { get; set; } // ← WAJIB ADA
        public int Quantity { get; set; } // ← WAJIB ADA
        public DateTime TanggalDitambahkan { get; set; } = DateTime.Now; // optional
        public Pakaian Pakaian { get; set; } // ← navigasi ke entitas Pakaian
    }
}

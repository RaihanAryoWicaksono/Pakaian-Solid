using PakaianAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
    public class Keranjang
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string KodePakaian { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public DateTime TanggalDitambahkan { get; set; } = DateTime.Now;

        [ForeignKey("KodePakaian")]
        public Pakaian Pakaian { get; set; }
    }
}

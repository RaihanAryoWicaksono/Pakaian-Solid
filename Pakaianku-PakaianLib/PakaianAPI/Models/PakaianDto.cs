using PakaianLib; // Pastikan namespace ini diimpor jika PakaianLib adalah proyek terpisah

namespace PakaianApi.Models
{
    // Data Transfer Object (DTO) untuk Pakaian
    // Digunakan untuk mengirim data pakaian melalui API
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

    // DTO untuk permintaan aksi pakaian
    public class AksiPakaianRequest
    {
        public string KodePakaian { get; set; }
        public AksiPakaian Aksi { get; set; }
    }

    // DTO untuk permintaan update pakaian
    public class UpdatePakaianRequest
    {
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal? Harga { get; set; }
        public int? Stok { get; set; }
        public StatusPakaian? Status { get; set; } // Tambahkan ini agar status bisa diupdate eksplisit
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakaianku
{

    // Enum untuk status pakaian dalam konteks penjualan
    public enum StatusPakaian
    {
        Tersedia,         // Pakaian tersedia di toko
        DalamKeranjang,   // Pakaian sudah dimasukkan ke keranjang belanja
        Dipesan,          // Pakaian sudah dipesan tapi belum dibayar
        Dibayar,          // Pakaian sudah dibayar tapi belum dikirim
        DalamPengiriman,  // Pakaian dalam proses pengiriman
        Diterima,         // Pakaian sudah diterima oleh pembeli
        Dikembalikan,     // Pakaian dikembalikan oleh pembeli
        TidakTersedia     // Pakaian tidak tersedia (habis stok)
    }

    // Enum untuk aksi/event pada pakaian dalam konteks penjualan
    public enum AksiPakaian
    {
        TambahKeKeranjang,
        KeluarkanDariKeranjang,
        Pesan,
        BatalPesan,
        Bayar,
        Kirim,
        TerimaPakaian,
        KembalikanPakaian,
        RestokPakaian,
        HabisStok
    }

    public class Pakaian
    {
        // Properti pakaian
        public string Kode { get; private set; }
        public string Nama { get; private set; }
        public string Kategori { get; private set; }
        public string Warna { get; private set; }
        public string Ukuran { get; private set; }
        public decimal Harga { get; private set; }
        public int Stok { get; private set; }
        public StatusPakaian Status { get; private set; }

        // Automata Transition table
        private Dictionary<(StatusPakaian, AksiPakaian), StatusPakaian> transisiStatus;

        public Pakaian(string kode, string nama, string kategori, string warna, string ukuran, decimal harga, int stok)
        {
            Kode = kode;
            Nama = nama;
            Kategori = kategori;
            Warna = warna;
            Ukuran = ukuran;
            Harga = harga;
            Stok = stok;
            Status = stok > 0 ? StatusPakaian.Tersedia : StatusPakaian.TidakTersedia;

            // Inisialisasi tabel transisi automata
            InisialisasiTabelTransisi();
        }

        private void InisialisasiTabelTransisi()
        {
            transisiStatus = new Dictionary<(StatusPakaian, AksiPakaian), StatusPakaian>
            {
                // Transisi dari status Tersedia
                { (StatusPakaian.Tersedia, AksiPakaian.TambahKeKeranjang), StatusPakaian.DalamKeranjang },
                { (StatusPakaian.Tersedia, AksiPakaian.HabisStok), StatusPakaian.TidakTersedia },
                
                // Transisi dari status DalamKeranjang
                { (StatusPakaian.DalamKeranjang, AksiPakaian.KeluarkanDariKeranjang), StatusPakaian.Tersedia },
                { (StatusPakaian.DalamKeranjang, AksiPakaian.Pesan), StatusPakaian.Dipesan },
                
                // Transisi dari status Dipesan
                { (StatusPakaian.Dipesan, AksiPakaian.BatalPesan), StatusPakaian.Tersedia },
                { (StatusPakaian.Dipesan, AksiPakaian.Bayar), StatusPakaian.Dibayar },
                
                // Transisi dari status Dibayar
                { (StatusPakaian.Dibayar, AksiPakaian.Kirim), StatusPakaian.DalamPengiriman },
                
                // Transisi dari status DalamPengiriman
                { (StatusPakaian.DalamPengiriman, AksiPakaian.TerimaPakaian), StatusPakaian.Diterima },
                
                // Transisi dari status Diterima
                { (StatusPakaian.Diterima, AksiPakaian.KembalikanPakaian), StatusPakaian.Dikembalikan },
                
                // Transisi dari status Dikembalikan
                { (StatusPakaian.Dikembalikan, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia },
                
                // Transisi dari status TidakTersedia
                { (StatusPakaian.TidakTersedia, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia }
            };
        }

        // Method untuk memproses aksi pada pakaian
        public bool ProsesAksi(AksiPakaian aksi)
        {
            var kunciTransisi = (Status, aksi);

            if (transisiStatus.ContainsKey(kunciTransisi))
            {
                // Update stok berdasarkan aksi
                switch (aksi)
                {
                    case AksiPakaian.TambahKeKeranjang:
                        Stok--;
                        break;
                    case AksiPakaian.KeluarkanDariKeranjang:
                    case AksiPakaian.BatalPesan:
                    case AksiPakaian.KembalikanPakaian:
                        Stok++;
                        break;
                    case AksiPakaian.RestokPakaian:
                        Stok += 10; // Contoh penambahan stok
                        break;
                    case AksiPakaian.HabisStok:
                        Stok = 0;
                        break;
                }

                // Update status
                Status = transisiStatus[kunciTransisi];
                Console.WriteLine($"Pakaian '{Nama}' sekarang dalam status: {Status}, Stok: {Stok}");
                return true;
            }
            else
            {
                Console.WriteLine($"Aksi {aksi} tidak valid untuk pakaian '{Nama}' dalam status {Status}");
                return false;
            }
        }

        // Method untuk mendapatkan aksi yang valid untuk status saat ini
        public List<AksiPakaian> GetAksiValid()
        {
            List<AksiPakaian> aksiValid = new List<AksiPakaian>();

            foreach (var transisi in transisiStatus)
            {
                if (transisi.Key.Item1 == Status)
                {
                    aksiValid.Add(transisi.Key.Item2);
                }
            }

            return aksiValid;
        }

        // Override ToString untuk menampilkan informasi pakaian
        public override string ToString()
        {
            return $"Kode: {Kode}, Nama: {Nama}, Kategori: {Kategori}, " +
                   $"Warna: {Warna}, Ukuran: {Ukuran}, " +
                   $"Harga: Rp{Harga:N0}, Stok: {Stok}, Status: {Status}";
        }
    }

    // Class untuk mengelola katalog pakaian
    public class KatalogPakaian
    {
        private List<Pakaian> daftarPakaian;

        public KatalogPakaian()
        {
            daftarPakaian = new List<Pakaian>();
        }

        public void TambahPakaian(Pakaian pakaian)
        {
            daftarPakaian.Add(pakaian);
        }

        public Pakaian CariPakaianByKode(string kode)
        {
            return daftarPakaian.Find(p => p.Kode.Equals(kode, StringComparison.OrdinalIgnoreCase));
        }

        public List<Pakaian> CariPakaian(string keyword)
        {
            keyword = keyword.ToLower();
            return daftarPakaian.FindAll(p =>
                p.Nama.ToLower().Contains(keyword) ||
                p.Kategori.ToLower().Contains(keyword) ||
                p.Warna.ToLower().Contains(keyword) ||
                p.Ukuran.ToLower().Contains(keyword) ||
                p.Kode.ToLower().Contains(keyword));
        }

        public List<Pakaian> CariPakaianByHarga(decimal min, decimal max)
        {
            return daftarPakaian.FindAll(p => p.Harga >= min && p.Harga <= max);
        }

        public List<Pakaian> CariPakaianByKategori(string kategori)
        {
            return daftarPakaian.FindAll(p => p.Kategori.ToLower().Contains(kategori.ToLower()));
        }

        public List<Pakaian> GetSemuaPakaian()
        {
            return daftarPakaian;
        }

        public void TampilkanSemuaPakaian()
        {
            Console.WriteLine("\n=== KATALOG PAKAIAN ===");
            if (daftarPakaian.Count == 0)
            {
                Console.WriteLine("Katalog kosong.");
                return;
            }

            foreach (var pakaian in daftarPakaian)
            {
                Console.WriteLine(pakaian.ToString());
            }
        }

        public void TampilkanPakaian(List<Pakaian> pakaianList)
        {
            if (pakaianList.Count == 0)
            {
                Console.WriteLine("Tidak ada pakaian yang ditemukan.");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                Console.WriteLine(pakaian.ToString());
            }
        }
    }

    // Class untuk mengelola keranjang belanja
    public class KeranjangBelanja
    {
        private List<Pakaian> itemKeranjang;

        public KeranjangBelanja()
        {
            itemKeranjang = new List<Pakaian>();
        }

        public bool TambahKeKeranjang(Pakaian pakaian)
        {
            if (pakaian.Status == StatusPakaian.Tersedia)
            {
                if (pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang))
                {
                    itemKeranjang.Add(pakaian);
                    return true;
                }
            }
            return false;
        }

        public bool KeluarkanDariKeranjang(Pakaian pakaian)
        {
            if (itemKeranjang.Contains(pakaian) && pakaian.Status == StatusPakaian.DalamKeranjang)
            {
                if (pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang))
                {
                    itemKeranjang.Remove(pakaian);
                    return true;
                }
            }
            return false;
        }

        public bool KeluarkanDariKeranjangByIndex(int index)
        {
            if (index >= 0 && index < itemKeranjang.Count)
            {
                Pakaian pakaian = itemKeranjang[index];
                return KeluarkanDariKeranjang(pakaian);
            }
            return false;
        }

        public decimal HitungTotal()
        {
            decimal total = 0;
            foreach (var item in itemKeranjang)
            {
                total += item.Harga;
            }
            return total;
        }

        public List<Pakaian> GetSemuaItem()
        {
            return itemKeranjang;
        }

        public void TampilkanKeranjang()
        {
            Console.WriteLine("\n=== ISI KERANJANG BELANJA ===");
            if (itemKeranjang.Count == 0)
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            for (int i = 0; i < itemKeranjang.Count; i++)
            {
                Console.WriteLine($"[{i}] {itemKeranjang[i].ToString()}");
            }
            Console.WriteLine($"Total: Rp{HitungTotal():N0}");
        }

        public void KosongkanKeranjang()
        {
            while (itemKeranjang.Count > 0)
            {
                KeluarkanDariKeranjang(itemKeranjang[0]);
            }
        }

        public int JumlahItem()
        {
            return itemKeranjang.Count;
        }
    }

    // Program utama
    class Program
    {
        static KatalogPakaian katalog = new KatalogPakaian();
        static KeranjangBelanja keranjang = new KeranjangBelanja();

        static void Main(string[] args)
        {
            InisialisasiKatalog();

            bool lanjutkan = true;
            while (lanjutkan)
            {
                TampilkanMenu();
                Console.Write("Pilih menu (1-7): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CariPakaian();
                        break;
                    case "2":
                        TambahKeKeranjang();
                        break;
                    case "3":
                        LihatKeranjang();
                        break;
                    case "4":
                        HapusDariKeranjang();
                        break;
                    case "5":
                        Checkout();
                        break;
                    case "6":
                        LihatKatalog();
                        break;
                    case "7":
                        lanjutkan = false;
                        Console.WriteLine("Terima kasih telah menggunakan sistem penjualan pakaian!");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan pilih 1-7.");
                        break;
                }

                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void InisialisasiKatalog()
        {
            // Menambahkan pakaian ke katalog
            katalog.TambahPakaian(new Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10));
            katalog.TambahPakaian(new Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8));
            katalog.TambahPakaian(new Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5));
            katalog.TambahPakaian(new Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15));
            katalog.TambahPakaian(new Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12));
            katalog.TambahPakaian(new Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7));
            katalog.TambahPakaian(new Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8));
            katalog.TambahPakaian(new Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6));
            katalog.TambahPakaian(new Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10));
            katalog.TambahPakaian(new Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5));
            katalog.TambahPakaian(new Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4));
            katalog.TambahPakaian(new Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7));
        }

        static void TampilkanMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("     SISTEM PENJUALAN PAKAIAN");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Cari Pakaian");
            Console.WriteLine("2. Tambah ke Keranjang");
            Console.WriteLine("3. Lihat Keranjang");
            Console.WriteLine("4. Hapus dari Keranjang");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Lihat Semua Pakaian");
            Console.WriteLine("7. Keluar");
            Console.WriteLine("======================================");
            Console.WriteLine($"Jumlah item di keranjang: {keranjang.JumlahItem()}");
        }

        static void CariPakaian()
        {
            Console.WriteLine("\n=== CARI PAKAIAN ===");
            Console.WriteLine("1. Cari berdasarkan kata kunci");
            Console.WriteLine("2. Cari berdasarkan kategori");
            Console.WriteLine("3. Cari berdasarkan rentang harga");
            Console.Write("Pilih opsi pencarian (1-3): ");

            string opsi = Console.ReadLine();
            List<Pakaian> hasilPencarian = new List<Pakaian>();

            switch (opsi)
            {
                case "1":
                    Console.Write("Masukkan kata kunci: ");
                    string keyword = Console.ReadLine();
                    hasilPencarian = katalog.CariPakaian(keyword);
                    break;
                case "2":
                    Console.Write("Masukkan kategori (Kemeja/Kaos/Celana/Jaket): ");
                    string kategori = Console.ReadLine();
                    hasilPencarian = katalog.CariPakaianByKategori(kategori);
                    break;
                case "3":
                    try
                    {
                        Console.Write("Masukkan harga minimum: ");
                        decimal min = decimal.Parse(Console.ReadLine());
                        Console.Write("Masukkan harga maksimum: ");
                        decimal max = decimal.Parse(Console.ReadLine());
                        hasilPencarian = katalog.CariPakaianByHarga(min, max);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Format harga tidak valid. Gunakan angka saja.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Opsi tidak valid.");
                    return;
            }

            Console.WriteLine("\n=== HASIL PENCARIAN ===");
            if (hasilPencarian.Count > 0)
            {
                katalog.TampilkanPakaian(hasilPencarian);
            }
            else
            {
                Console.WriteLine("Tidak ada pakaian yang sesuai dengan kriteria pencarian.");
            }
        }

        static void TambahKeKeranjang()
        {
            Console.Write("Masukkan kode pakaian yang ingin ditambahkan ke keranjang: ");
            string kode = Console.ReadLine();

            Pakaian pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian != null)
            {
                if (keranjang.TambahKeKeranjang(pakaian))
                {
                    Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.");
                }
                else
                {
                    Console.WriteLine($"Gagal menambahkan pakaian ke keranjang. Mungkin stok habis atau status tidak valid.");
                }
            }
            else
            {
                Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan.");
            }
        }

        static void LihatKeranjang()
        {
            keranjang.TampilkanKeranjang();
        }

        static void HapusDariKeranjang()
        {
            keranjang.TampilkanKeranjang();

            if (keranjang.JumlahItem() == 0)
            {
                return;
            }

            Console.Write("Masukkan nomor item yang ingin dihapus dari keranjang: ");
            try
            {
                int index = int.Parse(Console.ReadLine());
                if (keranjang.KeluarkanDariKeranjangByIndex(index))
                {
                    Console.WriteLine("Item berhasil dihapus dari keranjang.");
                }
                else
                {
                    Console.WriteLine("Gagal menghapus item dari keranjang. Nomor item tidak valid.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input tidak valid. Masukkan angka saja.");
            }
        }

        static void Checkout()
        {
            if (keranjang.JumlahItem() == 0)
            {
                Console.WriteLine("Keranjang kosong. Tidak ada yang bisa di-checkout.");
                return;
            }

            keranjang.TampilkanKeranjang();
            Console.WriteLine("\n=== CHECKOUT ===");
            Console.WriteLine($"Total pembelian: Rp{keranjang.HitungTotal():N0}");

            Console.Write("Lanjutkan checkout? (y/n): ");
            string konfirmasi = Console.ReadLine().ToLower();

            if (konfirmasi == "y")
            {
                List<Pakaian> itemKeranjang = keranjang.GetSemuaItem();
                foreach (var item in new List<Pakaian>(itemKeranjang))
                {
                    item.ProsesAksi(AksiPakaian.Pesan);
                    item.ProsesAksi(AksiPakaian.Bayar);
                    item.ProsesAksi(AksiPakaian.Kirim);
                }

                Console.WriteLine("Checkout berhasil! Pesanan Anda sedang dalam pengiriman.");
                keranjang.KosongkanKeranjang();
            }
            else
            {
                Console.WriteLine("Checkout dibatalkan.");
            }
        }

        static void LihatKatalog()
        {
            katalog.TampilkanSemuaPakaian();
        }
    }
}

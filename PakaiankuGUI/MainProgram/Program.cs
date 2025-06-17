// Program.cs (INI ADALAH FILE UNTUK APLIKASI KONSOL ANDA)
// File ini akan menjadi mandiri dan TIDAK BERKONFLIK dengan API Anda.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Menggunakan PakaianLib untuk definisi enum StatusPakaian dan AksiPakaian
// SERTA UNTUK KLAS Pakaian sebagai model data dasar.
using PakaianLib;

namespace PakaianConsoleApp // Namespace UNIK untuk aplikasi konsol ini
{
    // UserRole dan User (Tetap didefinisikan di dalam proyek konsol)
    public enum UserRole
    {
        Admin,
        Customer
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }

    // Kelas Pakaian (VERSI KHUSUS UNTUK KONSOLE APP INI,
    // TIDAK BERGUNA UNTUK DATABASE API. MENGANDUNG LOGIKA STATE MACHINE IN-MEMORY LENGKAP)
    // Kelas ini akan "membungkus" PakaianLib.Pakaian atau diimplementasikan mirip,
    // tetapi untuk tujuan konsol kita bisa membuatnya mandiri dari model EF Core
    // untuk menghindari ambiguitas jika Anda ingin mempertahankan kedua Pakaian
    // atau saya bisa mendefinisikan ulang Pakaian di sini.

    // Agar sederhana dan tidak duplikasi logika PakaianLib.Pakaian,
    // kita akan menggunakan PakaianLib.Pakaian sebagai base class
    // dan menambahkan logika state machine di sini, atau kita bisa
    // mendefinisikan ulang Pakaian di sini jika Anda tidak mau PakaianLib.Pakaian
    // punya logika state machine.

    // Pilihan terbaik: PakaianLib.Pakaian menjadi entitas DTO sederhana untuk DB/API.
    // Pakaian di konsol ini akan menjadi kelas lengkap dengan state machine.
    // Namun, agar konsisten dengan migrasi sebelumnya, mari kita buat PakaianLib.Pakaian
    // tetap sebagai entitas DB, dan *jika* konsol ini perlu state machine,
    // maka PakaianLib.Pakaian harus memiliki state machine, atau kita duplikasi
    // Pakaian untuk konsol.

    // Karena user ingin 'Pakaianku.cs' dipertahankan, maka saya akan mengasumsikan
    // Pakaian di namespace Pakaianku (sekarang PakaianConsoleApp) adalah versi lengkap
    // dengan state machine. Ini berarti akan ada dua definisi kelas Pakaian di sistem.
    // Untuk menghindari ambiguitas di Program.cs ini, kita bisa menggunakan alias
    // atau lebih baik, kita definisikan ulang Pakaian di sini agar mandiri.

    // ***** KRITIS: PENTING UNTUK MEMUTUSKAN APAPUN TENTANG INI *****
    // Jika PakaianLib.Pakaian sudah memiliki ProcessAksi dan status,
    // maka Pakaian ini di sini akan konflik.
    // Berdasarkan chat sebelumnya, PakaianLib.Pakaian sudah punya ProsesAksi.
    // Jadi, kita HARUS menggunakan PakaianLib.Pakaian di konsol ini,
    // dan pastikan PakaianLib.Pakaian memiliki logika state machine.

    // KARENA ITU, SAYA AKAN MENGASUMSIKAN PakaianLib.Pakaian.cs SUDAH MEMILIKI
    // LOGIKA STATE MACHINE DARI PERBAIKAN SEBELUMNYA.
    // KESALAHAN TERAKHIR ANDA ADALAH PADA Pakaianku.cs YANG INI, BUKAN PakaianLib.Pakaian.
    // Jadi, kita hanya perlu perbaiki referensi enum di sini.


    // Class untuk mengelola katalog pakaian (In-memory untuk konsol)
    public class KatalogPakaian
    {
        private List<PakaianLib.Pakaian> daftarPakaian; // Menggunakan Pakaian dari PakaianLib

        public KatalogPakaian()
        {
            daftarPakaian = new List<PakaianLib.Pakaian>();
        }

        public void TambahPakaian(PakaianLib.Pakaian pakaian)
        {
            daftarPakaian.Add(pakaian);
        }

        public PakaianLib.Pakaian CariPakaianByKode(string kode)
        {
            return daftarPakaian.Find(p => p.Kode.Equals(kode, StringComparison.OrdinalIgnoreCase));
        }

        public List<PakaianLib.Pakaian> CariPakaian(string keyword)
        {
            keyword = keyword.ToLower();
            return daftarPakaian.FindAll(p =>
                p.Nama.ToLower().Contains(keyword) ||
                p.Kategori.ToLower().Contains(keyword) ||
                p.Warna.ToLower().Contains(keyword) ||
                p.Ukuran.ToLower().Contains(keyword) ||
                p.Kode.ToLower().Contains(keyword));
        }

        public List<PakaianLib.Pakaian> CariPakaianByHarga(decimal min, decimal max)
        {
            return daftarPakaian.FindAll(p => p.Harga >= min && p.Harga <= max);
        }

        public List<PakaianLib.Pakaian> CariPakaianByKategori(string kategori)
        {
            return daftarPakaian.FindAll(p => p.Kategori.ToLower().Contains(kategori.ToLower()));
        }

        public List<PakaianLib.Pakaian> GetSemuaPakaian()
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
                Console.WriteLine($"Kode: {pakaian.Kode}, Nama: {pakaian.Nama}, Kategori: {pakaian.Kategori}, " +
                                  $"Warna: {pakaian.Warna}, Ukuran: {pakaian.Ukuran}, " +
                                  $"Harga: Rp{pakaian.Harga:N0}, Stok: {pakaian.Stok}, Status: {pakaian.Status}");
            }
        }

        public bool HapusPakaian(string kode)
        {
            var pakaian = CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return false;
            }

            // Aturan penghapusan in-memory
            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                return false;
            }

            return daftarPakaian.Remove(pakaian);
        }

        // UpdatePakaian tidak lagi dibutuhkan di sini karena logikanya ada di API controller
        // Namun, jika Anda ingin mempertahankan update in-memory untuk konsol,
        // Anda bisa menyalin kembali logikanya dari PakaianLib.Pakaian.cs versi lama
        // dan modifikasi PakaianLib.Pakaian.cs untuk memiliki setter publik untuk properti.
        // Atau, seperti di bawah, kita akan mengupdate properti langsung di method UpdatePakaian.

        // Method untuk restock pakaian (in-memory untuk konsol)
        public bool RestokPakaian(string kode, int jumlah)
        {
            var pakaian = CariPakaianByKode(kode);
            if (pakaian == null)
            {
                Console.WriteLine($"Pakaian dengan kode '{kode}' tidak ditemukan.");
                return false;
            }

            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah restok harus lebih dari 0.");
                return false;
            }

            pakaian.Stok += jumlah; // Tambahkan stok
            // Panggil aksi RestokPakaian untuk memperbarui status jika perlu
            pakaian.ProsesAksi(AksiPakaian.RestokPakaian);
            Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil di-restok. Stok sekarang: {pakaian.Stok}");
            return true;
        }
    }

    // Kelas KeranjangBelanja (In-memory untuk konsol)
    public class KeranjangBelanja<T> where T : PakaianLib.Pakaian // Menggunakan Pakaian dari PakaianLib
    {
        private List<T> items = new List<T>();

        public bool TambahKeKeranjang(T item)
        {
            items.Add(item);
            return true;
        }

        public bool KeluarkanDariKeranjangByIndex(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                var item = items[index];
                items.RemoveAt(index);
                item.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang); // Memanggil aksi untuk memperbarui status
                return true;
            }
            return false;
        }

        // TampilkanKeranjang tidak ada di PakaianLib.KeranjangBelanja, jadi diimplementasikan di sini
        // Namun, method ini sebenarnya harus di Program.cs atau helper display, bukan di kelas domain.
        // Saya akan mengimplementasikan ulang di Program.cs.
        // Public method GetSemuaItem() sudah ada di PakaianLib.KeranjangBelanja.

        public int JumlahItem()
        {
            return items.Count;
        }

        public decimal HitungTotal()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Harga;
            }
            return total;
        }

        public List<T> GetSemuaItem()
        {
            return items;
        }

        public void KosongkanKeranjang()
        {
            items.Clear();
        }
    }

    // Program utama
    class Program
    {
        static Dictionary<string, User> daftarUser = new();
        static User currentUser = null;

        // Menggunakan KatalogPakaian dan KeranjangBelanja dari namespace PakaianConsoleApp ini
        static KatalogPakaian katalog = new KatalogPakaian();
        static KeranjangBelanja<PakaianLib.Pakaian> keranjang = new KeranjangBelanja<PakaianLib.Pakaian>();

        static void Main(string[] args)
        {
            InisialisasiKatalog();

            bool lanjutkan = true;

            daftarUser["admin"] = new User("admin", "admin123", UserRole.Admin);
            daftarUser["customer1"] = new User("customer1", "pass123", UserRole.Customer);

            while (currentUser == null)
            {
                Console.WriteLine("=== SELAMAT DATANG DI PAKAIANKU ===");
                Console.WriteLine("1. Login (Admin/Customer)");
                Console.WriteLine("2. Register (Customer saja)");
                Console.Write("Pilih (1/2): ");
                var opsi = Console.ReadLine();

                if (opsi == "1")
                {
                    Login();
                }
                else if (opsi == "2")
                {
                    Register();
                }
                else
                {
                    Console.WriteLine("Opsi tidak valid.\n");
                }
            }

            while (lanjutkan)
            {
                TampilkanMenu();
                Console.Write("Pilih menu (1-9): ");
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
                        if (currentUser.Role == UserRole.Admin)
                        {
                            KelolaKatalog();
                        }
                        else
                        {
                            Console.WriteLine("Hanya admin yang dapat mengelola katalog.");
                        }
                        break;
                    case "8":
                        currentUser = null;
                        keranjang.KosongkanKeranjang();
                        Console.Clear();
                        break;
                    case "9":
                        lanjutkan = false;
                        Console.WriteLine("Terima kasih telah menggunakan sistem penjualan pakaian!");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan pilih 1-9.");
                        break;
                }

                if (lanjutkan && currentUser != null)
                {
                    Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (currentUser == null && input != "9")
                {
                    Console.WriteLine("\nAnda telah logout. Tekan Enter untuk kembali ke menu login.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void Register()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            if (daftarUser.ContainsKey(username))
            {
                Console.WriteLine("Username sudah terdaftar!");
                return;
            }

            daftarUser[username] = new User(username, password, UserRole.Customer);
            Console.WriteLine("Registrasi berhasil sebagai Customer!");
        }

        static void Login()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            if (daftarUser.TryGetValue(username, out var user) && user.Password == password)
            {
                currentUser = user;
                Console.WriteLine($"Login berhasil sebagai {user.Username} ({user.Role})");
            }
            else
            {
                Console.WriteLine("Username atau password salah.");
            }
        }

        static void InisialisasiKatalog()
        {
            // Menambahhkan pakaian ke katalog (menggunakan PakaianLib.Pakaian)
            katalog.TambahPakaian(new PakaianLib.Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10));
            katalog.TambahPakaian(new PakaianLib.Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8));
            katalog.TambahPakaian(new PakaianLib.Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5));
            katalog.TambahPakaian(new PakaianLib.Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15));
            katalog.TambahPakaian(new PakaianLib.Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12));
            katalog.TambahPakaian(new PakaianLib.Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7));
            katalog.TambahPakaian(new PakaianLib.Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8));
            katalog.TambahPakaian(new PakaianLib.Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6));
            katalog.TambahPakaian(new PakaianLib.Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10));
            katalog.TambahPakaian(new PakaianLib.Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5));
            katalog.TambahPakaian(new PakaianLib.Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4));
            katalog.TambahPakaian(new PakaianLib.Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7));
        }

        static void TampilkanMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Login sebagai: {currentUser.Username} ({currentUser.Role})");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Cari Pakaian");
            Console.WriteLine("2. Tambah ke Keranjang");
            Console.WriteLine("3. Lihat Keranjang");
            Console.WriteLine("4. Hapus dari Keranjang");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Lihat Semua Pakaian");

            if (currentUser.Role == UserRole.Admin)
                Console.WriteLine("7. Kelola Pakaian di Katalog");

            Console.WriteLine("8. Kembali Menu Login");
            Console.WriteLine("9. Keluar Aplikasi");
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
            List<PakaianLib.Pakaian> hasilPencarian = new List<PakaianLib.Pakaian>();

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
                TampilkanPakaian(hasilPencarian);
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

            PakaianLib.Pakaian pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian != null)
            {
                if (pakaian.Stok > 0)
                {
                    if (pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang)) // Memanggil ProsesAksi dari PakaianLib.Pakaian
                    {
                        keranjang.TambahKeKeranjang(pakaian);
                        pakaian.Stok--; // Update stok in-memory
                        Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.");
                    }
                    else
                    {
                        Console.WriteLine($"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Mungkin status tidak valid.");
                    }
                }
                else
                {
                    Console.WriteLine($"Stok pakaian '{pakaian.Nama}' habis. Tidak dapat ditambahkan ke keranjang.");
                }
            }
            else
            {
                Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan.");
            }
        }

        static void LihatKeranjang()
        {
            Console.WriteLine("\n=== KERANJANG BELANJA ===");
            var items = keranjang.GetSemuaItem();
            if (items.Count == 0)
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Nama} ({items[i].Kode}) - Rp{items[i].Harga:N0} (Stok: {items[i].Stok}, Status: {items[i].Status})");
            }
            Console.WriteLine($"Total: Rp{keranjang.HitungTotal():N0}");
            Console.WriteLine($"Jumlah Item: {keranjang.JumlahItem()}");
        }

        static void HapusDariKeranjang()
        {
            LihatKeranjang();

            if (keranjang.JumlahItem() == 0)
            {
                return;
            }

            Console.Write("Masukkan nomor item yang ingin dihapus dari keranjang: ");
            try
            {
                int indexUntukHapus = int.Parse(Console.ReadLine());
                if (indexUntukHapus > 0 && indexUntukHapus <= keranjang.JumlahItem())
                {
                    PakaianLib.Pakaian pakaianDihapus = keranjang.GetSemuaItem()[indexUntukHapus - 1];
                    if (keranjang.KeluarkanDariKeranjangByIndex(indexUntukHapus - 1))
                    {
                        pakaianDihapus.Stok++; // Kembalikan stok in-memory
                        // Tidak perlu panggil ProsesAksi lagi, KeluarkanDariKeranjangByIndex sudah memanggilnya
                        Console.WriteLine("Item berhasil dihapus dari keranjang.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal menghapus item dari keranjang.");
                    }
                }
                else
                {
                    Console.WriteLine("Nomor item tidak valid.");
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

            LihatKeranjang();
            Console.WriteLine("\n=== CHECKOUT ===");
            Console.WriteLine($"Total pembelian: Rp{keranjang.HitungTotal():N0}");

            Console.Write("Lanjutkan checkout? (y/n): ");
            string konfirmasi = Console.ReadLine().ToLower();

            if (konfirmasi == "y")
            {
                List<PakaianLib.Pakaian> itemKeranjang = keranjang.GetSemuaItem();
                foreach (var item in new List<PakaianLib.Pakaian>(itemKeranjang)) // Iterasi salinan
                {
                    // Pastikan item dalam status yang benar untuk setiap aksi
                    if (item.ProsesAksi(AksiPakaian.Pesan))
                    {
                        Console.WriteLine($"'{item.Nama}' berhasil dipesan.");
                        if (item.ProsesAksi(AksiPakaian.Bayar))
                        {
                            Console.WriteLine($"'{item.Nama}' berhasil dibayar.");
                            if (item.ProsesAksi(AksiPakaian.Kirim))
                            {
                                Console.WriteLine($"'{item.Nama}' sedang dikirim.");
                                if (item.ProsesAksi(AksiPakaian.TerimaPakaian)) // Aksi pelanggan menerima
                                {
                                    Console.WriteLine($"'{item.Nama}' berhasil diterima.");
                                    item.ProsesAksi(AksiPakaian.Selesai); // Menandai proses pengiriman selesai
                                    Console.WriteLine($"Proses checkout '{item.Nama}' selesai.");
                                }
                                else
                                {
                                    Console.WriteLine($"Gagal mengubah status '{item.Nama}' ke Diterima. Status saat ini: {item.Status}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Gagal mengubah status '{item.Nama}' ke DalamPengiriman. Status saat ini: {item.Status}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Gagal mengubah status '{item.Nama}' ke Dibayar. Status saat ini: {item.Status}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Gagal mengubah status '{item.Nama}' ke Dipesan. Status saat ini: {item.Status}");
                    }
                }

                Console.WriteLine("\nCheckout berhasil! Pesanan Anda telah diproses.");
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

        static void KelolaKatalog()
        {
            bool kembali = false;
            while (!kembali)
            {
                Console.WriteLine("\n=== MANAJEMEN KATALOG (ADMIN) ===");
                Console.WriteLine("1. Tambah Pakaian");
                Console.WriteLine("2. Lihat Semua Pakaian");
                Console.WriteLine("3. Update Pakaian");
                Console.WriteLine("4. Restock Pakaian");
                Console.WriteLine("5. Hapus Pakaian");
                Console.WriteLine("6. Kembali");
                Console.Write("Pilih menu (1-6): ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        TambahPakaian();
                        break;
                    case "2":
                        katalog.TampilkanSemuaPakaian();
                        break;
                    case "3":
                        UpdatePakaian();
                        break;
                    case "4":
                        RestockPakaian();
                        break;
                    case "5":
                        HapusPakaian();
                        break;
                    case "6":
                        kembali = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
                if (!kembali)
                {
                    Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        // Table-driven kategori dan ukuran
        static Dictionary<string, List<string>> validKategoriUkuran = new Dictionary<string, List<string>>()
        {
            { "Kemeja", new List<string> { "S", "M", "L", "XL" } },
            { "Kaos", new List<string> { "S", "M", "L", "XL" } },
            { "Celana", new List<string> { "28", "30", "32", "34", "36" } },
            { "Jaket", new List<string> { "M", "L", "XL", "XXL" } }
        };

        static void TambahPakaian()
        {
            Console.Write("Kode Pakaian: ");
            string kode = Console.ReadLine();

            if (katalog.CariPakaianByKode(kode) != null)
            {
                Console.WriteLine("Kode pakaian sudah ada.");
                return;
            }

            Console.Write("Nama: ");
            string nama = Console.ReadLine();

            Console.Write("Kategori (Kemeja/Kaos/Celana/Jaket): ");
            string kategori = Console.ReadLine();
            if (!validKategoriUkuran.ContainsKey(kategori))
            {
                Console.WriteLine("Kategori tidak valid.");
                return;
            }

            Console.Write("Warna: ");
            string warna = Console.ReadLine();

            Console.Write("Ukuran (" + string.Join("/", validKategoriUkuran[kategori]) + "): ");
            string ukuran = Console.ReadLine();
            if (!validKategoriUkuran[kategori].Contains(ukuran))
            {
                Console.WriteLine("Ukuran tidak valid.");
                return;
            }

            Console.Write("Harga: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal harga))
            {
                Console.WriteLine("Harga tidak valid.");
                return;
            }

            Console.Write("Stok: ");
            if (!int.TryParse(Console.ReadLine(), out int stok))
            {
                Console.WriteLine("Stok tidak valid.");
                return;
            }

            // Menggunakan konstruktor Pakaian dari PakaianLib
            var pakaian = new PakaianLib.Pakaian(kode, nama, kategori, warna, ukuran, harga, stok);
            katalog.TambahPakaian(pakaian);
            Console.WriteLine("Pakaian berhasil ditambahkan!");
        }

        static void UpdatePakaian()
        {
            Console.Write("Masukkan kode pakaian yang ingin diubah: ");
            string kode = Console.ReadLine();

            var pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                Console.WriteLine("Pakaian tidak ditemukan.");
                return;
            }

            Console.WriteLine($"Data saat ini: Kode: {pakaian.Kode}, Nama: {pakaian.Nama}, Kategori: {pakaian.Kategori}, Warna: {pakaian.Warna}, Ukuran: {pakaian.Ukuran}, Harga: Rp{pakaian.Harga:N0}, Stok: {pakaian.Stok}, Status: {pakaian.Status}");

            Console.Write("Nama baru (biarkan kosong jika tidak diubah): ");
            string namaInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(namaInput)) pakaian.Nama = namaInput;

            Console.Write("Kategori baru (Kemeja/Kaos/Celana/Jaket, biarkan kosong jika tidak diubah): ");
            string kategoriInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(kategoriInput))
            {
                if (!validKategoriUkuran.ContainsKey(kategoriInput))
                {
                    Console.WriteLine("Kategori tidak valid.");
                    return;
                }
                pakaian.Kategori = kategoriInput;
            }

            Console.Write("Warna baru (biarkan kosong jika tidak diubah): ");
            string warnaInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(warnaInput)) pakaian.Warna = warnaInput;

            string currentKategoriForSize = string.IsNullOrWhiteSpace(kategoriInput) ? pakaian.Kategori : kategoriInput;
            Console.Write($"Ukuran baru ({string.Join("/", validKategoriUkuran.GetValueOrDefault(currentKategoriForSize, new List<string> { "N/A" }))}, biarkan kosong jika tidak diubah): ");
            string ukuranInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ukuranInput))
            {
                if (!validKategoriUkuran.GetValueOrDefault(currentKategoriForSize, new List<string>()).Contains(ukuranInput))
                {
                    Console.WriteLine("Ukuran tidak valid untuk kategori tersebut.");
                    return;
                }
                pakaian.Ukuran = ukuranInput;
            }

            Console.Write("Harga baru (biarkan kosong jika tidak diubah): ");
            string hargaInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(hargaInput))
            {
                if (!decimal.TryParse(hargaInput, out decimal h))
                {
                    Console.WriteLine("Harga tidak valid.");
                    return;
                }
                pakaian.Harga = h;
            }

            Console.Write("Stok baru (biarkan kosong jika tidak diubah): ");
            string stokInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stokInput))
            {
                if (!int.TryParse(stokInput, out int s))
                {
                    Console.WriteLine("Stok tidak valid.");
                    return;
                }
                int oldStok = pakaian.Stok;
                pakaian.Stok = s;

                // Perbarui status jika ada perubahan stok yang signifikan
                if (oldStok > 0 && pakaian.Stok == 0)
                {
                    pakaian.ProsesAksi(AksiPakaian.StokHabis);
                }
                else if (oldStok == 0 && pakaian.Stok > 0)
                {
                    pakaian.ProsesAksi(AksiPakaian.RestokPakaian);
                }
            }

            Console.WriteLine("Pakaian berhasil diperbarui!");
        }

        static void RestockPakaian()
        {
            Console.Write("Masukkan kode pakaian yang ingin di-restock: ");
            string kode = Console.ReadLine();

            var pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                Console.WriteLine("Pakaian tidak ditemukan.");
                return;
            }

            Console.Write("Jumlah stok yang ingin ditambahkan: ");
            if (!int.TryParse(Console.ReadLine(), out int stokTambahan) || stokTambahan <= 0)
            {
                Console.WriteLine("Jumlah stok tidak valid.");
                return;
            }

            pakaian.Stok += stokTambahan;
            pakaian.ProsesAksi(AksiPakaian.RestokPakaian);
            Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil di-restok. Stok sekarang: {pakaian.Stok}");
        }

        static void HapusPakaian()
        {
            Console.Write("Masukkan kode pakaian yang ingin dihapus: ");
            string kode = Console.ReadLine();

            var pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                Console.WriteLine("Pakaian tidak ditemukan.");
                return;
            }

            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                Console.WriteLine($"Gagal menghapus pakaian. Status saat ini: {pakaian.Status}");
                return;
            }

            bool berhasil = katalog.HapusPakaian(kode);
            if (berhasil)
            {
                Console.WriteLine("Pakaian berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Gagal menghapus pakaian.");
            }
        }

        // Helper method untuk menampilkan daftar pakaian
        static void TampilkanPakaian(List<PakaianLib.Pakaian> pakaianList)
        {
            if (pakaianList.Count == 0)
            {
                Console.WriteLine("Tidak ada pakaian yang ditemukan.");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                Console.WriteLine($"Kode: {pakaian.Kode}, Nama: {pakaian.Nama}, Kategori: {pakaian.Kategori}, " +
                                  $"Warna: {pakaian.Warna}, Ukuran: {pakaian.Ukuran}, " +
                                  $"Harga: Rp{pakaian.Harga:N0}, Stok: {pakaian.Stok}, Status: {pakaian.Status}");
            }
        }
    }
}

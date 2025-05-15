using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakaianLib;

namespace Pakaianku
{
    //UserRole dan User (Dimas)
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


    // Program utama
    class Program
    {
        static Dictionary<string, User> daftarUser = new();
        static User currentUser = null;

        static KatalogPakaian katalog = new KatalogPakaian();
        static KeranjangBelanja<Pakaian> keranjang = new KeranjangBelanja<Pakaian>();
        static void Main(string[] args)
        {
            InisialisasiKatalog();

            bool lanjutkan = true;

            //Login Admin & User (Dimas)
            daftarUser["admin"] = new User("admin", "admin123", UserRole.Admin);
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
                bool kembaliLogin = false;
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
                        while (currentUser == null)
                        {
                            Console.Clear();
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
                        break;
                        break;
                    case "9":
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

        //Registe dan login (dimas)
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

            // Paksa role jadi Customer
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
            // Menambahhkan pakaian ke katalog
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
                if (pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang))
                {
                    keranjang.TambahKeKeranjang(pakaian);
                    Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.");
                }
                else
                {
                    Console.WriteLine($"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Mungkin stok habis atau status tidak valid.");
                }
            }
            else
            {
                // Jika pakaian dengan kode tidak ditemukan
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
                    // Memastikkan status item sesuai dengan aksi yang akan dilakukan
                    if (item.Status == StatusPakaian.DalamKeranjang)
                    {
                        item.ProsesAksi(AksiPakaian.Pesan);
                    }
                    else
                    {
                        Console.WriteLine($"Aksi Pesan tidak dapat dilakukan untuk '{item.Nama}' yang sedang dalam status '{item.Status}'");
                        continue;
                    }

                    if (item.Status == StatusPakaian.Dipesan)
                    {
                        item.ProsesAksi(AksiPakaian.Bayar);
                    }
                    else
                    {
                        Console.WriteLine($"Aksi Bayar tidak dapat dilakukan untuk '{item.Nama}' yang sedang dalam status '{item.Status}'");
                        continue;
                    }

                    if (item.Status == StatusPakaian.Dibayar)
                    {
                        item.ProsesAksi(AksiPakaian.Kirim); 
                    }
                    else
                    {
                        Console.WriteLine($"Aksi Kirim tidak dapat dilakukan untuk '{item.Nama}' yang sedang dalam status '{item.Status}'");
                        continue;
                    }

                    // Setelah pakaian diterima, kurangi stok dan kembalikan ke status 'Tersedia'
                    if (item.Status == StatusPakaian.Diterima)
                    {
                        item.ProsesAksi(AksiPakaian.SelesaiCheckout);
                    }
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

        static void KelolaKatalog()
        {
            bool kembali = false;
            while (!kembali)
            {
                Console.WriteLine("\n=== MANAJEMEN KATALOG (ADMIN) ===");
                Console.WriteLine("1. Tambah Pakaian");
                Console.WriteLine("2. Lihat Semua Pakaian");
                Console.WriteLine("3. Update Pakaian");
                Console.WriteLine("4. Hapus Pakaian");
                Console.WriteLine("5. Kembali");
                Console.Write("Pilih menu (1-5): ");
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
                        HapusPakaian();
                        break;
                    case "5":
                        kembali = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }


        // Table-driven kategori dan ukuran (dimas)
        static Dictionary<string, List<string>> validKategoriUkuran = new Dictionary<string, List<string>>()
            {
                { "Kemeja", new List<string> { "S", "M", "L", "XL" } },
                { "Kaos", new List<string> { "S", "M", "L", "XL" } },
                { "Celana", new List<string> { "28", "30", "32", "34", "36" } },
                { "Jaket", new List<string> { "M", "L", "XL", "XXL" } }
            };
        // CRUD Pakaian (dimas)
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

            var pakaian = new Pakaian(kode, nama, kategori, warna, ukuran, harga, stok);
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

            Console.Write("Nama baru (biarkan kosong jika tidak diubah): ");
            string nama = Console.ReadLine();

            Console.Write("Kategori baru (Kemeja/Kaos/Celana/Jaket): ");
            string kategori = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(kategori) && !validKategoriUkuran.ContainsKey(kategori))
            {
                Console.WriteLine("Kategori tidak valid.");
                return;
            }

            Console.Write("Warna baru: ");
            string warna = Console.ReadLine();

            Console.Write("Ukuran baru: ");
            string ukuran = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(kategori) && !string.IsNullOrWhiteSpace(ukuran))
            {
                if (!validKategoriUkuran[kategori].Contains(ukuran))
                {
                    Console.WriteLine("Ukuran tidak valid untuk kategori tersebut.");
                    return;
                }
            }

            Console.Write("Harga baru: ");
            decimal? harga = null;
            string hargaInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(hargaInput) && decimal.TryParse(hargaInput, out decimal h))
            {
                harga = h;
            }

            Console.Write("Stok baru: ");
            int? stok = null;
            string stokInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stokInput) && int.TryParse(stokInput, out int s))
            {
                stok = s;
            }

            katalog.UpdatePakaian(kode, string.IsNullOrWhiteSpace(nama) ? null : nama,
                                         string.IsNullOrWhiteSpace(kategori) ? null : kategori,
                                         string.IsNullOrWhiteSpace(warna) ? null : warna,
                                         string.IsNullOrWhiteSpace(ukuran) ? null : ukuran,
                                         harga, stok);

            Console.WriteLine("Pakaian berhasil diperbarui!");
        }

        static void HapusPakaian()
        {
            Console.Write("Masukkan kode pakaian yang ingin dihapus: ");
            string kode = Console.ReadLine();

            bool berhasil = katalog.HapusPakaian(kode);
            if (berhasil)
            {
                Console.WriteLine("Pakaian berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Gagal menghapus pakaian. Pastikan statusnya bukan sedang dipesan atau dikirim.");
            }

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }
    }
}
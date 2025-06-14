using Pakaianku; // Pastikan namespace Pakaianku diimpor
using PakaianLib; // Pastikan namespace PakaianLib diimpor
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; // Diperlukan untuk IWebHostEnvironment
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System; // Untuk Console, jika diperlukan untuk debugging
using System.Linq; // Untuk Cast<Pakaian>().ToList()

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<KatalogPakaian>(sp =>
{
<<<<<<< HEAD
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=localhost;Port=3306;Database=PakaianKu;Uid=root;Pwd=;";
    return new KatalogPakaian(connectionString);
});

builder.Services.AddControllersWithViews();

// Opsional: Untuk kompilasi runtime Razor Views selama pengembangan
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pakaian}/{action=Index}/{id?}");

// --- LOGIKA CLI SEKARANG BERADA DI SINI, DI LUAR BLOK NAMESPACE YANG TERPISAH ---
// Logic untuk menjalankan mode CLI atau API
// Jika aplikasi dijalankan dengan argumen "--cli", jalankan mode CLI.
// Jika tidak, atau jika environment variabel ASPNETCORE_URLS diset (indikasi web app), jalankan mode API.
bool runAsCli = args.Contains("--cli");

if (runAsCli)
{
    Console.WriteLine("Running in CLI mode...");
    // Dapatkan instance KatalogPakaian dari DI container
    var cliKatalog = app.Services.GetRequiredService<KatalogPakaian>();
    // Buat instance baru KeranjangBelanja untuk CLI (atau bisa juga singleton jika diinginkan)
    var cliKeranjang = new KeranjangBelanja<Pakaian>();

    Program.RunCliApp(cliKatalog, cliKeranjang); // Panggil metode dari kelas Program yang sama
}
else
{
    Console.WriteLine("Running in MVC Web App mode..."); // Ubah pesan untuk kejelasan
    app.Run(); // Ini memulai server web Kestrel
}

// Pindahkan semua metode CLI ke dalam partial class Program tanpa blok namespace terpisah
public partial class Program // Gunakan 'partial' jika Anda ingin memisahkan ini ke beberapa file
{
    // Metode ini akan dipanggil dari bagian atas Program.cs untuk menjalankan CLI
    public static void RunCliApp(KatalogPakaian cliKatalog, KeranjangBelanja<Pakaian> cliKeranjang)
    {
        // Mengisi katalog dengan data awal jika database kosong
        InisialisasiKatalog(cliKatalog);

        bool lanjutkan = true;
        while (lanjutkan)
        {
            TampilkanMenu(cliKeranjang);
            Console.Write("Pilih menu (1-7): ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CariPakaian(cliKatalog);
                    break;
                case "2":
                    TambahKeKeranjang(cliKatalog, cliKeranjang);
                    break;
                case "3":
                    LihatKeranjang(cliKeranjang);
                    break;
                case "4":
                    HapusDariKeranjang(cliKatalog, cliKeranjang);
                    break;
                case "5":
                    Checkout(cliKatalog, cliKeranjang);
                    break;
                case "6":
                    LihatKatalog(cliKatalog);
                    break;
                case "7":
                    lanjutkan = false;
                    Console.WriteLine("Terima kasih telah menggunakan sistem penjualan pakaian!");
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan pilih 1-7.");
                    break;
=======
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
>>>>>>> origin/1201230013_OWED
            }

<<<<<<< HEAD
            Console.WriteLine("\nTekan Enter untuk melanjutkan...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    // Pindahkan semua metode CLI ke sini, dan sesuaikan parameter
    // Ubah metode-metode CLI Anda agar menerima instance `KatalogPakaian` dan `KeranjangBelanja` sebagai parameter
    static void InisialisasiKatalog(KatalogPakaian katalog)
    {
        if (katalog.GetSemuaPakaian().Count == 0)
        {
            Console.WriteLine("Menginisialisasi katalog dengan data awal...");
=======
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
>>>>>>> origin/1201230013_OWED
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
            Console.WriteLine("Inisialisasi katalog selesai.");
        }
        else
        {
<<<<<<< HEAD
            Console.WriteLine("Katalog sudah berisi data. Melewatkan inisialisasi data awal.");
=======
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
>>>>>>> origin/1201230013_OWED
        }
    }

    static void TampilkanMenu(KeranjangBelanja<Pakaian> keranjang)
    {
        Console.WriteLine("======================================");
        Console.WriteLine("          SISTEM PENJUALAN PAKAIAN");
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

    static void CariPakaian(KatalogPakaian katalog)
    {
        Console.WriteLine("\n=== CARI PAKAIAN ===");
        Console.WriteLine("1. Cari berdasarkan kata kunci (Nama, Kategori, Warna, Ukuran, Kode)");
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
                Console.Write("Masukkan kategori (contoh: Kemeja, Kaos, Celana, Jaket): ");
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

    static void TambahKeKeranjang(KatalogPakaian katalog, KeranjangBelanja<Pakaian> keranjang)
    {
        Console.Write("Masukkan kode pakaian yang ingin ditambahkan ke keranjang: ");
        string kode = Console.ReadLine();

        Pakaian pakaian = katalog.CariPakaianByKode(kode);
        if (pakaian != null)
        {
            if (pakaian.Stok > 0)
            {
                if (pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang))
                {
<<<<<<< HEAD
                    pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);
                    katalog.UpdatePakaian(pakaian.Kode, stok: pakaian.Stok, status: pakaian.Status);
=======
                    keranjang.TambahKeKeranjang(pakaian);
>>>>>>> origin/1201230013_OWED
                    Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.");
                }
                else
                {
<<<<<<< HEAD
                    Console.WriteLine($"Gagal menambahkan pakaian ke keranjang.");
=======
                    Console.WriteLine($"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Mungkin stok habis atau status tidak valid.");
>>>>>>> origin/1201230013_OWED
                }
            }
            else
            {
<<<<<<< HEAD
                Console.WriteLine($"Pakaian '{pakaian.Nama}' dengan kode {kode} tidak memiliki stok yang tersedia.");
=======
                // Jika pakaian dengan kode tidak ditemukan
                Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan.");
>>>>>>> origin/1201230013_OWED
            }
        }
        else
        {
            Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan di katalog.");
        }
    }

    static void LihatKeranjang(KeranjangBelanja<Pakaian> keranjang)
    {
        keranjang.TampilkanKeranjang();
    }

    static void HapusDariKeranjang(KatalogPakaian katalog, KeranjangBelanja<Pakaian> keranjang)
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
            List<Pakaian> itemsInCart = keranjang.GetSemuaItem().Cast<Pakaian>().ToList();
            if (index > 0 && index <= itemsInCart.Count)
            {
                Pakaian pakaianDihapus = itemsInCart[index - 1];
                if (keranjang.KeluarkanDariKeranjangByIndex(index))
                {
                    pakaianDihapus.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
                    katalog.UpdatePakaian(pakaianDihapus.Kode, stok: pakaianDihapus.Stok, status: pakaianDihapus.Status);
                    Console.WriteLine($"Item '{pakaianDihapus.Nama}' berhasil dihapus dari keranjang.");
                }
                else
                {
                    Console.WriteLine("Gagal menghapus item dari keranjang. Nomor item tidak valid.");
                }
            }
<<<<<<< HEAD
=======
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
>>>>>>> origin/1201230013_OWED
            else
            {
                Console.WriteLine("Nomor item tidak valid.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Input tidak valid. Masukkan angka saja.");
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

            bool berhasil = katalog.RestokPakaian(kode, stokTambahan);
            if (berhasil)
            {
                Console.WriteLine("Stok pakaian berhasil ditambahkan!");
            }
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

    static void Checkout(KatalogPakaian katalog, KeranjangBelanja<Pakaian> keranjang)
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
            List<Pakaian> itemKeranjang = keranjang.GetSemuaItem().Cast<Pakaian>().ToList();
            foreach (var item in new List<Pakaian>(itemKeranjang))
            {
                item.ProsesAksi(AksiPakaian.Pesan);
                katalog.UpdatePakaian(item.Kode, stok: item.Stok, status: item.Status);

                item.ProsesAksi(AksiPakaian.Bayar);
                katalog.UpdatePakaian(item.Kode, stok: item.Stok, status: item.Status);

                item.ProsesAksi(AksiPakaian.Kirim);
                katalog.UpdatePakaian(item.Kode, stok: item.Stok, status: item.Status);
            }

            Console.WriteLine("Checkout berhasil! Pesanan Anda sedang dalam pengiriman.");
            keranjang.KosongkanKeranjang();
        }
        else
        {
            Console.WriteLine("Checkout dibatalkan.");
        }
    }

    static void LihatKatalog(KatalogPakaian katalog)
    {
        katalog.TampilkanSemuaPakaian();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakaianLib; 

namespace Pakaianku
{
    class Program
    {
        static string connectionString = "Server=localhost;Port=3306;Database=PakaianKu;Uid=root;Pwd=root;";

        // Inisialisasi objek KatalogPakaian dengan string koneksi
        static KatalogPakaian katalog = new KatalogPakaian(connectionString);

        // Inisialisasi objek KeranjangBelanja untuk menyimpan pakaian
        static KeranjangBelanja<Pakaian> keranjang = new KeranjangBelanja<Pakaian>();

        static void Main(string[] args)
        {
            // Mengisi katalog dengan data awal jika database kosong
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

        // Metode untuk menambahkan data pakaian awal ke database jika belum ada
        static void InisialisasiKatalog()
        {
            // Periksa apakah katalog sudah memiliki data. Jika tidak, tambahkan data awal.
            // Ini mencegah duplikasi data setiap kali aplikasi dijalankan.
            if (katalog.GetSemuaPakaian().Count == 0)
            {
                Console.WriteLine("Menginisialisasi katalog dengan data awal...");
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
                Console.WriteLine("Katalog sudah berisi data. Melewatkan inisialisasi data awal.");
            }
        }

        // Metode untuk menampilkan menu utama aplikasi
        static void TampilkanMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("         SISTEM PENJUALAN PAKAIAN");
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

        // Metode untuk melakukan pencarian pakaian berdasarkan berbagai kriteria
        static void CariPakaian()
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

        // Metode untuk menambahkan pakaian ke keranjang belanja
        static void TambahKeKeranjang()
        {
            Console.Write("Masukkan kode pakaian yang ingin ditambahkan ke keranjang: ");
            string kode = Console.ReadLine();

            Pakaian pakaian = katalog.CariPakaianByKode(kode);
            if (pakaian != null)
            {
                if (pakaian.Stok > 0)
                {
                    if (keranjang.TambahKeKeranjang(pakaian))
                    {
                        pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);
                        katalog.UpdatePakaian(pakaian.Kode, stok: pakaian.Stok, status: pakaian.Status);
                        Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.");
                    }
                    else
                    {
                        Console.WriteLine($"Gagal menambahkan pakaian ke keranjang.");
                    }
                }
                else
                {
                    Console.WriteLine($"Pakaian '{pakaian.Nama}' dengan kode {kode} tidak memiliki stok yang tersedia.");
                }
            }
            else
            {
                Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan di katalog.");
            }
        }

        // Metode untuk melihat isi keranjang belanja
        static void LihatKeranjang()
        {
            keranjang.TampilkanKeranjang();
        }

        // Metode untuk menghapus item dari keranjang belanja
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
                // Ambil item sebelum dihapus untuk memperbarui status dan stok di database
                List<Pakaian> itemsInCart = keranjang.GetSemuaItem().Cast<Pakaian>().ToList();
                if (index > 0 && index <= itemsInCart.Count)
                {
                    Pakaian pakaianDihapus = itemsInCart[index - 1]; // Dapatkan objek pakaian yang akan dihapus
                    if (keranjang.KeluarkanDariKeranjangByIndex(index))
                    {
                        // Perbarui status pakaian di database setelah dikeluarkan dari keranjang
                        pakaianDihapus.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
                        katalog.UpdatePakaian(pakaianDihapus.Kode, stok: pakaianDihapus.Stok, status: pakaianDihapus.Status);
                        Console.WriteLine($"Item '{pakaianDihapus.Nama}' berhasil dihapus dari keranjang.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal menghapus item dari keranjang. Nomor item tidak valid.");
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

        // Metode untuk menyelesaikan proses pembelian (checkout)
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
                List<Pakaian> itemKeranjang = keranjang.GetSemuaItem().Cast<Pakaian>().ToList();
                foreach (var item in new List<Pakaian>(itemKeranjang)) // Iterasi salinan untuk menghindari modifikasi koleksi saat iterasi
                {
                    // Proses transisi status dan update di database untuk setiap item
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

        // Metode untuk menampilkan semua pakaian yang ada di katalog
        static void LihatKatalog()
        {
            katalog.TampilkanSemuaPakaian();
        }
    }
}

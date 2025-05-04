using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakaianLib;

namespace Pakaianku
{
    // Program utama
    class Program
    {
        static KatalogPakaian katalog = new KatalogPakaian();
        static KeranjangBelanja<Pakaian> keranjang = new KeranjangBelanja<Pakaian>();
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
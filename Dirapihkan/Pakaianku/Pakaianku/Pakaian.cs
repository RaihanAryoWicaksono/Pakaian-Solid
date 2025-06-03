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
}
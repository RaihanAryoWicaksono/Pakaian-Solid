using PakaianLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pakaianku
{
    public class Pakaian
    {
        // Properti pakaian
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public StatusPakaian Status { get; set; }

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
                { (StatusPakaian.Tersedia, AksiPakaian.Pesan), StatusPakaian.Tersedia },
                
                // Transisi dari status DalamKeranjang
                { (StatusPakaian.DalamKeranjang, AksiPakaian.KeluarkanDariKeranjang), StatusPakaian.Tersedia },
                { (StatusPakaian.DalamKeranjang, AksiPakaian.Pesan), StatusPakaian.Dipesan },
                { (StatusPakaian.DalamKeranjang, AksiPakaian.TambahKeKeranjang), StatusPakaian.DalamKeranjang },
                
                // Transisi dari status Dipesan
                { (StatusPakaian.Dipesan, AksiPakaian.BatalPesan), StatusPakaian.Tersedia },
                { (StatusPakaian.Dipesan, AksiPakaian.Bayar), StatusPakaian.Dibayar },
                
                // Transisi dari status Dibayar
                { (StatusPakaian.Dibayar, AksiPakaian.Kirim), StatusPakaian.Diterima },
                
                // Transisi dari status DalamPengiriman
                { (StatusPakaian.DalamPengiriman, AksiPakaian.TerimaPakaian), StatusPakaian.Diterima },
                
                // Transisi dari status TidakTersedia
                { (StatusPakaian.TidakTersedia, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia },

                { (StatusPakaian.Diterima, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia },
                { (StatusPakaian.Diterima, AksiPakaian.SelesaiCheckout), StatusPakaian.Tersedia }
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
                        if (Stok > 0)
                        {
                            Stok--;
                        }
                        else
                        {
                            Console.WriteLine($"Stok pakaian '{Nama}' habis. Tidak dapat ditambahkan ke keranjang.");
                            return false;
                        }
                        break;
                    case AksiPakaian.KeluarkanDariKeranjang:
                        Stok++;
                        break;
                    case AksiPakaian.BatalPesan:
                    case AksiPakaian.HabisStok:
                        Stok = 0;
                        break;
                    case AksiPakaian.SelesaiCheckout:
                        Status = StatusPakaian.Tersedia;
                        break;

                }

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

        public bool HapusPakaian(string kode)
        {
            var pakaian = CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return false;
            }

            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                return false;
            }

            return daftarPakaian.Remove(pakaian);
        }
        public bool UpdatePakaian(string kode, string nama = null, string kategori = null,
                                  string warna = null, string ukuran = null,
                                  decimal? harga = null, int? stok = null)
        {
            var pakaian = CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return false;
            }

            if (stok.HasValue)
            {
                int currentStok = pakaian.Stok;

                if (currentStok > 0 && stok.Value == 0)
                {
                    pakaian.ProsesAksi(AksiPakaian.HabisStok);
                }

                else if (currentStok == 0 && stok.Value > 0)
                {
                    pakaian.ProsesAksi(AksiPakaian.RestokPakaian);
                }
            }

            return true;
        }

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

            // Jika status TidakTersedia dan ditambahkan stok, ubah status via aksi
            if (pakaian.Status == StatusPakaian.TidakTersedia || pakaian.Stok == 0)
            {
                pakaian.Stok += jumlah;
                pakaian.ProsesAksi(AksiPakaian.RestokPakaian);
            }
            else
            {
                pakaian.Stok += jumlah;
                Console.WriteLine($"Pakaian '{pakaian.Nama}' berhasil di-restok. Stok sekarang: {pakaian.Stok}");
            }

            return true;
        }
    }


    // generic
    public class KeranjangBelanja<T> where T : Pakaian
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
                item.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);

                return true;
            }
            return false;
        }

        public void TampilkanKeranjang()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            Console.WriteLine("=== Keranjang Belanja ===");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        public int JumlahItem()
        {
            return items.Count;
        }

        public decimal HitungTotal()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                if (item is Pakaian pakaian)
                {
                    total += pakaian.Harga;
                }
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
}
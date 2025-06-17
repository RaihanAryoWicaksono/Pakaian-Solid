// Pakaianku.cs (Ini adalah versi yang dipertahankan untuk tujuan legacy/terpisah)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakaianLib; // Penting: Untuk mengakses enum dari PakaianLib

namespace Pakaianku.Legacy // Namespace diubah untuk menghindari ambiguitas
{
    public class Pakaian
    {
        // Properti pakaian
        public string Kode { get; private set; }
        public string Nama { get; private set; }
        public string Kategori { get; private set; }
        public string Warna { get; private set; }
        public string Ukuran { get; private set; }
        public decimal Harga { get; private set; }
        public int Stok { get; set; }
        public PakaianLib.StatusPakaian Status { get; set; } // Menggunakan enum dari PakaianLib

        // Automata Transition table
        private Dictionary<(PakaianLib.StatusPakaian, PakaianLib.AksiPakaian), PakaianLib.StatusPakaian> transisiStatus;

        public Pakaian(string kode, string nama, string kategori, string warna, string ukuran, decimal harga, int stok)
        {
            Kode = kode;
            Nama = nama;
            Kategori = kategori;
            Warna = warna;
            Ukuran = ukuran;
            Harga = harga;
            Stok = stok;
            Status = stok > 0 ? PakaianLib.StatusPakaian.Tersedia : PakaianLib.StatusPakaian.TidakTersedia;

            // Inisialisasi tabel transisi automata
            InisialisasiTabelTransisi();
        }

        private void InisialisasiTabelTransisi()
        {
            transisiStatus = new Dictionary<(PakaianLib.StatusPakaian, PakaianLib.AksiPakaian), PakaianLib.StatusPakaian>
            {
                // Transisi dari status Tersedia
                { (PakaianLib.StatusPakaian.Tersedia, PakaianLib.AksiPakaian.TambahKeKeranjang), PakaianLib.StatusPakaian.DalamKeranjang },
                { (PakaianLib.StatusPakaian.Tersedia, PakaianLib.AksiPakaian.StokHabis), PakaianLib.StatusPakaian.TidakTersedia }, // Menggunakan StokHabis
                { (PakaianLib.StatusPakaian.Tersedia, PakaianLib.AksiPakaian.Pesan), PakaianLib.StatusPakaian.Dipesan }, // Pakaian langsung dipesan
                
                // Transisi dari status DalamKeranjang
                { (PakaianLib.StatusPakaian.DalamKeranjang, PakaianLib.AksiPakaian.KeluarkanDariKeranjang), PakaianLib.StatusPakaian.Tersedia },
                { (PakaianLib.StatusPakaian.DalamKeranjang, PakaianLib.AksiPakaian.Pesan), PakaianLib.StatusPakaian.Dipesan },
                { (PakaianLib.StatusPakaian.DalamKeranjang, PakaianLib.AksiPakaian.TambahKeKeranjang), PakaianLib.StatusPakaian.DalamKeranjang },
                
                // Transisi dari status Dipesan
                { (PakaianLib.StatusPakaian.Dipesan, PakaianLib.AksiPakaian.Batalkan), PakaianLib.StatusPakaian.Dibatalkan }, // Menggunakan Batalkan
                { (PakaianLib.StatusPakaian.Dipesan, PakaianLib.AksiPakaian.Bayar), PakaianLib.StatusPakaian.Dibayar },
                
                // Transisi dari status Dibayar
                { (PakaianLib.StatusPakaian.Dibayar, PakaianLib.AksiPakaian.Kirim), PakaianLib.StatusPakaian.DalamPengiriman }, // Mengarah ke DalamPengiriman
                { (PakaianLib.StatusPakaian.Dibayar, PakaianLib.AksiPakaian.Batalkan), PakaianLib.StatusPakaian.Dibatalkan }, // Dapat dibatalkan setelah dibayar
                
                // Transisi dari status DalamPengiriman
                { (PakaianLib.StatusPakaian.DalamPengiriman, PakaianLib.AksiPakaian.TerimaPakaian), PakaianLib.StatusPakaian.Diterima }, // Pelanggan menerima
                { (PakaianLib.StatusPakaian.DalamPengiriman, PakaianLib.AksiPakaian.Retur), PakaianLib.StatusPakaian.Retur },
                { (PakaianLib.StatusPakaian.DalamPengiriman, PakaianLib.AksiPakaian.Selesai), PakaianLib.StatusPakaian.Selesai }, // Admin menandai selesai
                
                // Transisi dari status Diterima (setelah TerimaPakaian oleh pelanggan)
                { (PakaianLib.StatusPakaian.Diterima, PakaianLib.AksiPakaian.Selesai), PakaianLib.StatusPakaian.Selesai }, // Admin menandai selesai
                { (PakaianLib.StatusPakaian.Diterima, PakaianLib.AksiPakaian.Retur), PakaianLib.StatusPakaian.Retur },

                // Transisi dari status Selesai
                { (PakaianLib.StatusPakaian.Selesai, PakaianLib.AksiPakaian.Retur), PakaianLib.StatusPakaian.Retur },

                // Transisi dari status TidakTersedia
                { (PakaianLib.StatusPakaian.TidakTersedia, PakaianLib.AksiPakaian.RestokPakaian), PakaianLib.StatusPakaian.Tersedia },

                // Transisi dari status Dibatalkan
                { (PakaianLib.StatusPakaian.Dibatalkan, PakaianLib.AksiPakaian.RestokPakaian), PakaianLib.StatusPakaian.Tersedia },

                // Transisi dari status Retur
                { (PakaianLib.StatusPakaian.Retur, PakaianLib.AksiPakaian.RestokPakaian), PakaianLib.StatusPakaian.Tersedia },
                
                // Transisi untuk SelesaiCheckout (jika masih digunakan)
                { (PakaianLib.StatusPakaian.Tersedia, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai },
                { (PakaianLib.StatusPakaian.DalamKeranjang, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai },
                { (PakaianLib.StatusPakaian.Dipesan, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai },
                { (PakaianLib.StatusPakaian.Dibayar, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai },
                { (PakaianLib.StatusPakaian.DalamPengiriman, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai },
                { (PakaianLib.StatusPakaian.Diterima, PakaianLib.AksiPakaian.SelesaiCheckout), PakaianLib.StatusPakaian.Selesai }
            };
        }

        // Method untuk memproses aksi pada pakaian
        public bool ProsesAksi(PakaianLib.AksiPakaian aksi) // Menggunakan AksiPakaian dari PakaianLib
        {
            var kunciTransisi = (Status, aksi);

            if (transisiStatus.ContainsKey(kunciTransisi))
            {
                // Update stok (perubahan ini hanya berlaku untuk instance ini,
                // tidak secara otomatis memperbarui database).
                // Perubahan stok sebenarnya untuk database ditangani di Controller.
                switch (aksi)
                {
                    case PakaianLib.AksiPakaian.TambahKeKeranjang:
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
                    case PakaianLib.AksiPakaian.KeluarkanDariKeranjang:
                        Stok++;
                        break;
                    case PakaianLib.AksiPakaian.StokHabis:
                        Stok = 0;
                        break;
                    case PakaianLib.AksiPakaian.RestokPakaian:
                        // Stok akan ditambahkan di tempat lain (di controller/logic utama)
                        break;
                        // Untuk aksi lain seperti Batalkan, Pesan, Bayar, Kirim, TerimaPakaian, Selesai, Retur
                        // perubahan stok tidak langsung terjadi di sini, karena sudah ditangani di level controller/main logic.
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
        public List<PakaianLib.AksiPakaian> GetAksiValid()
        {
            List<PakaianLib.AksiPakaian> aksiValid = new List<PakaianLib.AksiPakaian>();

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

    // Kelas KatalogPakaian dan KeranjangBelanja<T> telah dihapus dari file ini
    // karena sudah ada versi yang dikelola database atau PakaianLib.
    // Jika Anda memerlukan versi in-memory ini, Anda harus merevisinya
    // dan menggunakannya secara terpisah dari API utama.
}

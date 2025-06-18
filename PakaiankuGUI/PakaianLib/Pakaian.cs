// PakaianLib/Pakaian.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianLib
{
    public class Pakaian
    {
        [Key]
        public string Kode { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Kategori { get; set; }

        [Required]
        public string Warna { get; set; }

        [Required]
        public string Ukuran { get; set; }

        public decimal Harga { get; set; }
        public int Stok { get; set; }

        [Column(TypeName = "varchar(20)")]
        public StatusPakaian Status { get; private set; }

        public Pakaian() { }

        public Pakaian(string kode, string nama, string kategori, string warna, string ukuran, decimal harga, int stok)
        {
            Kode = kode;
            Nama = nama;
            Kategori = kategori;
            Warna = warna;
            Ukuran = ukuran;
            Harga = harga;
            Stok = stok;
            Status = Stok > 0 ? StatusPakaian.Tersedia : StatusPakaian.TidakTersedia;
        }

        public bool ProsesAksi(AksiPakaian aksi)
        {
            switch (aksi)
            {
                case AksiPakaian.TambahKeKeranjang:
                    if (Status == StatusPakaian.Tersedia && Stok > 0)
                    {
                        Status = StatusPakaian.DalamKeranjang;
                        return true;
                    }
                    return false;
                case AksiPakaian.KeluarkanDariKeranjang:
                    if (Status == StatusPakaian.DalamKeranjang)
                    {
                        Status = StatusPakaian.Tersedia;
                        return true;
                    }
                    return false;
                case AksiPakaian.Pesan:
                    if (Status == StatusPakaian.DalamKeranjang)
                    {
                        Status = StatusPakaian.Dipesan;
                        return true;
                    }
                    return false;
                case AksiPakaian.Bayar:
                    if (Status == StatusPakaian.Dipesan)
                    {
                        Status = StatusPakaian.Dibayar;
                        return true;
                    }
                    return false;
                case AksiPakaian.Kirim:
                    if (Status == StatusPakaian.Dibayar)
                    {
                        Status = StatusPakaian.DalamPengiriman;
                        return true;
                    }
                    return false;
                case AksiPakaian.Selesai:
                    if (Status == StatusPakaian.DalamPengiriman || Status == StatusPakaian.Diterima)
                    {
                        Status = StatusPakaian.Selesai;
                        return true;
                    }
                    return false;
                case AksiPakaian.Retur:
                    if (Status == StatusPakaian.Selesai || Status == StatusPakaian.DalamPengiriman || Status == StatusPakaian.Diterima)
                    {
                        Status = StatusPakaian.Retur;
                        return true;
                    }
                    return false;
                case AksiPakaian.Batalkan:
                    if (Status == StatusPakaian.Dipesan || Status == StatusPakaian.Dibayar || Status == StatusPakaian.DalamKeranjang)
                    {
                        Status = StatusPakaian.Dibatalkan;
                        return true;
                    }
                    return false;
                case AksiPakaian.StokHabis:
                    Status = StatusPakaian.TidakTersedia;
                    return true;
                case AksiPakaian.RestokPakaian:
                    if (Status == StatusPakaian.TidakTersedia || Status == StatusPakaian.Dibatalkan || Status == StatusPakaian.Retur)
                    {
                        Status = StatusPakaian.Tersedia;
                        return true;
                    }
                    return false;
                case AksiPakaian.TerimaPakaian:
                    if (Status == StatusPakaian.DalamPengiriman)
                    {
                        Status = StatusPakaian.Diterima;
                        return true;
                    }
                    return false;
                case AksiPakaian.SelesaiCheckout:
                    if (Status != StatusPakaian.Selesai)
                    {
                        Status = StatusPakaian.Selesai;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}

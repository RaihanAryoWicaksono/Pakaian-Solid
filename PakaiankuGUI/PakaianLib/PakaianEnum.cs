// PakaianLib/PakaianEnum.cs (Diperbarui)
namespace PakaianLib
{
    public enum StatusPakaian
    {
        Tersedia,
        DalamKeranjang,
        Dipesan,
        Dibayar,
        DalamPengiriman,
        Selesai, // Menandai pesanan selesai (misal: barang sudah sampai dan tidak ada komplain)
        Retur,
        Dibatalkan, // Menggantikan BatalPesan
        TidakTersedia, // Menggantikan HabisStok (status jika stok 0)
        Diterima // Status ketika pakaian sudah diterima oleh pelanggan
    }

    public enum AksiPakaian
    {
        TambahKeKeranjang,
        KeluarkanDariKeranjang,
        Pesan,
        Bayar,
        Kirim,
        Selesai, // Aksi untuk menandai pengiriman selesai
        Retur,
        Batalkan, // Menggantikan BatalPesan
        StokHabis, // Menggantikan HabisStok
        RestokPakaian,
        TerimaPakaian, // Aksi baru untuk pelanggan menerima pakaian
        SelesaiCheckout // Aksi ini secara konseptual lebih luas, namun didefinisikan untuk kompatibilitas jika masih ada referensi lama.
                        // Biasanya ini adalah hasil dari serangkaian aksi (Pesan, Bayar, Kirim)
    }
}

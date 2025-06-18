// PakaianLib/PakaianEnum.cs
namespace PakaianLib
{
    public enum StatusPakaian
    {
        Tersedia,
        DalamKeranjang,
        Dipesan,
        Dibayar,
        DalamPengiriman,
        Selesai,
        Retur,
        Dibatalkan,
        TidakTersedia,
        Diterima
    }

    public enum AksiPakaian
    {
        TambahKeKeranjang,
        KeluarkanDariKeranjang,
        Pesan,
        Bayar,
        Kirim,
        Selesai,
        Retur,
        Batalkan,
        StokHabis,
        RestokPakaian,
        TerimaPakaian,
        SelesaiCheckout
    }
}

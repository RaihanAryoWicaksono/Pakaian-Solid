using System;
using System.Collections.Generic;
using System.Linq;

namespace PakaianLib
{
    public enum StatusPakaian
    {
        Tersedia,         
        DalamKeranjang,   
        Dipesan,          
        Dibayar,          
        DalamPengiriman,  
        Diterima,            
        TidakTersedia,
        Selesai,
        Dibatalkan,
        Retur
    }

    
    public enum AksiPakaian
    {
        TambahKeKeranjang,
        KeluarkanDariKeranjang,
        Pesan,
        BatalPesan,
        Bayar,
        Kirim,
        TerimaPakaian,
        RestokPakaian,
        HabisStok,
        SelesaiCheckout,
        StokHabis,
        Batalkan,
        Retur,
        Selesai
    }
}
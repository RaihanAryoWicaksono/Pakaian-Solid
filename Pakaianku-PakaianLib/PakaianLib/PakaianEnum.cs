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
        Dikembalikan,     
        TidakTersedia     
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
        KembalikanPakaian,
        RestokPakaian,
        HabisStok
    }


}
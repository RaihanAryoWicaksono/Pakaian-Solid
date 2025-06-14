using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
=======

>>>>>>> origin/1201230013_OWED
namespace PakaianLib
{
    public enum StatusPakaian
    {
<<<<<<< HEAD
        Tersedia,
        DalamKeranjang,
        Dipesan,
        Dibayar,
        DalamPengiriman,
        Diterima,
        Dikembalikan,
        TidakTersedia
    }
=======
        Tersedia,         
        DalamKeranjang,   
        Dipesan,          
        Dibayar,          
        DalamPengiriman,  
        Diterima,            
        TidakTersedia     
    }

    
>>>>>>> origin/1201230013_OWED
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
<<<<<<< HEAD
        HabisStok
=======
        HabisStok,
        Tersedia,
        SelesaiCheckout
>>>>>>> origin/1201230013_OWED
    }
}
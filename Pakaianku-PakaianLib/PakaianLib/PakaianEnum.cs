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
<<<<<<< HEAD
        Diterima,         
        Dikembalikan,     
=======
        Diterima,            
>>>>>>> 1201230013_OWED
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
<<<<<<< HEAD
        HabisStok
    }


=======
        HabisStok,
        Tersedia,
        SelesaiCheckout
    }
>>>>>>> 1201230013_OWED
}
//using Microsoft.EntityFrameworkCore;
//using PakaianApi.Data;
//using PakaianApi.Models;
//using PakaianAPI.Models;
//using PakaianLib;


//namespace PakaianApi.Services
//{
//    public class KeranjangService : IKeranjangService
//    {
//        private readonly ApplicationDbContext _context;

//        public KeranjangService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//            public async Task<bool> TambahKeKeranjangAsync(string kodePakaian, int qty)
//            {
//                var existing = await _context.Keranjang.FirstOrDefaultAsync(k => k.KodePakaian == kodePakaian);
//                if (existing != null)
//                    return false;

//                var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == kodePakaian);
//                if (pakaian == null || pakaian.Stok < qty)
//                    return false;

//                pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);

//                var item = new Keranjang
//                {
//                    KodePakaian = kodePakaian,
//                    Quantity = qty
//                };

//                _context.Keranjang.Add(item);
//                await _context.SaveChangesAsync();
//                return true;
//            }

//        public async Task<KeranjangDto> GetKeranjangAsync()
//        {
//            var items = await _context.Keranjang.Include(k => k.Pakaian).ToListAsync();

//            return new KeranjangDto
//            {
//                Items = items
//        .Where(k => k.Pakaian != null)
//        .Select(k => new KeranjangItemDto
//        {
//                Id = k.Id,
             
//                Quantity = k.Quantity,
//                TotalHarga = k.Pakaian.Harga * k.Quantity,
//                TanggalDitambahkan = k.TanggalDitambahkan,
//                Pakaian = new PakaianDto
//            {
//                Kode = k.Pakaian.Kode,
//                Nama = k.Pakaian.Nama,
//                Kategori = k.Pakaian.Kategori,
//                Warna = k.Pakaian.Warna,
//                Ukuran = k.Pakaian.Ukuran,
//                Harga = k.Pakaian.Harga,
//                Stok = k.Pakaian.Stok,
//                Status = k.Pakaian.Status
//            }
//        }).ToList(),

//                TotalHarga = items
//        .Where(k => k.Pakaian != null)
//        .Sum(k => k.Pakaian.Harga * k.Quantity),

//                JumlahItem = items.Sum(k => k.Quantity)
//            };

//        }

//        public async Task<bool> HapusItemAsync(int id)
//        {
//            var item = await _context.Keranjang.Include(k => k.Pakaian).FirstOrDefaultAsync(k => k.Id == id);
//            if (item == null) return false;

//            item.Pakaian?.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);

//            _context.Keranjang.Remove(item);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> KosongkanKeranjangAsync()
//        {
//            var items = await _context.Keranjang.Include(k => k.Pakaian).ToListAsync();
//            foreach (var item in items)
//            {
//                item.Pakaian?.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
//            }

//            _context.Keranjang.RemoveRange(items);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<List<Keranjang>> GetEntityItemsAsync()
//        {
//            return await _context.Keranjang
//                .Include(k => k.Pakaian) // untuk ambil detail pakaian
//                .ToListAsync();
//        }

//    }
//}
using Microsoft.AspNetCore.Mvc;
using PakaianAPI.Models;
using PakaianApi.Services;
using PakaianApi.Models;
using PakaianLib;
using Microsoft.EntityFrameworkCore;
using PakaianApi.Data;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KeranjangController : ControllerBase
    {
        //private readonly IKeranjangService _keranjangService;
        private readonly ApplicationDbContext _context;

        public KeranjangController(ApplicationDbContext context)
        {
            _context = context;
            //_keranjangService = keranjangService;
        }

        private async Task<KeranjangBelanja> GetOrCreateUserCartAsync(int userId)
        {
            var keranjangDb = await _context.KeranjangBelanja
                .Include(kb => kb.Items)
                    .ThenInclude(ki => ki.Pakaian)
                .FirstOrDefaultAsync(kb => kb.UserId == userId);

            if (keranjangDb == null)
            {
                // Pastikan User dengan userId ini ada di database sebelum membuat KeranjangBelanja
                var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (userEntity == null)
                {
                    throw new InvalidOperationException($"User with ID {userId} not found in database. Cannot create cart.");
                }

                keranjangDb = new KeranjangBelanja { UserId = userId, User = userEntity, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }; // Kaitkan dengan entitas User
                _context.KeranjangBelanja.Add(keranjangDb);
                await _context.SaveChangesAsync();
                // Muat ulang keranjang untuk memastikan semua properti navigasi siap setelah ID di-generate
                keranjangDb = await _context.KeranjangBelanja
                    .Include(kb => kb.Items)
                        .ThenInclude(ki => ki.Pakaian)
                    .FirstOrDefaultAsync(kb => kb.UserId == userId);
            }
            return keranjangDb;
        }

        private KeranjangItemDto MapKeranjangItemToDto(KeranjangItem item)
        {
            if (item == null)
            {
                // Mengembalikan DTO null jika KeranjangItem null (seharusnya tidak terjadi)
                return null;
            }
            if (item.Pakaian == null)
            {
                // Jika pakaian null, kembalikan DTO default untuk mencegah crash
                return new PakaianApi.Models.KeranjangItemDto
                {
                    Id = item.Id,
                    KodePakaian = item.PakaianKode,
                    Quantity = item.Quantity,
                    HargaSatuan = item.HargaSatuan,
                    TotalHarga = item.TotalHargaItem,
                    Pakaian = MapPakaianToDto(null), // Kirim null ke MapPakaianToDto untuk dapatkan default
                    TanggalDitambahkan = item.AddedAt
                };
            }

            return new PakaianApi.Models.KeranjangItemDto
            {
                Id = item.Id,
                KodePakaian = item.PakaianKode,
                Quantity = item.Quantity,
                HargaSatuan = item.HargaSatuan,
                TotalHarga = item.TotalHargaItem,
                Pakaian = MapPakaianToDto(item.Pakaian),
                TanggalDitambahkan = item.AddedAt
            };
        }

        // Helper method untuk mapping Pakaian (entitas inti) ke PakaianDto (untuk API)
        private PakaianDto MapPakaianToDto(Pakaian pakaian)
        {
            if (pakaian == null)
            {
                return new PakaianDto
                {
                    Kode = "UNKNOWN",
                    Nama = "UNKNOWN ITEM",
                    Kategori = "UNKNOWN",
                    Warna = "UNKNOWN",
                    Ukuran = "UNKNOWN",
                    Harga = 0,
                    Stok = 0,
                    Status = StatusPakaian.TidakTersedia
                };
            }
            return new PakaianDto
            {
                Kode = pakaian.Kode,
                Nama = pakaian.Nama,
                Kategori = pakaian.Kategori,
                Warna = pakaian.Warna,
                Ukuran = pakaian.Ukuran,
                Harga = pakaian.Harga,
                Stok = pakaian.Stok,
                Status = pakaian.Status
            };
        }


        // GET: api/keranjang
        [HttpGet]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        public async Task<IActionResult> GetKeranjang([FromQuery] int userId) // userId sebagai parameter
        {
            if (userId == 0) return BadRequest("User ID diperlukan untuk mendapatkan keranjang.");

            var keranjangDb = await GetOrCreateUserCartAsync(userId);

            var keranjangDto = new KeranjangDto
            {
                Items = keranjangDb.Items.Select(MapKeranjangItemToDto).Where(item => item != null).ToList(), // Filter item null
                TotalHarga = keranjangDb.Items.Sum(item => item.TotalHargaItem),
                JumlahItem = keranjangDb.Items.Sum(item => item.Quantity)
            };

            return Ok(keranjangDto);
        }

        // POST: api/keranjang
        [HttpPost]
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto, [FromQuery] int userId) // userId sebagai parameter
        {
            if (userId == 0) return BadRequest("User ID diperlukan untuk menambahkan item ke keranjang.");

            var keranjangDb = await GetOrCreateUserCartAsync(userId);
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == addToCartDto.KodePakaian);

            if (pakaian == null)
            {
                return NotFound(new ErrorResponse { Status = 404, Message = $"Pakaian dengan kode {addToCartDto.KodePakaian} tidak ditemukan." });
            }

            if (pakaian.Stok <= 0)
            {
                return BadRequest(new ErrorResponse { Status = 400, Message = $"Pakaian '{pakaian.Nama}' stok tidak tersedia (Stok: {pakaian.Stok})." });
            }

            var existingCartItem = keranjangDb.Items.FirstOrDefault(ci => ci.PakaianKode == pakaian.Kode);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                existingCartItem.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                var newCartItem = new KeranjangItem
                {
                    KeranjangBelanjaId = keranjangDb.Id,
                    PakaianKode = pakaian.Kode,
                    Quantity = 1,
                    HargaSatuan = pakaian.Harga,
                    AddedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                keranjangDb.Items.Add(newCartItem);
            }

            pakaian.Stok--;
            pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang);

            keranjangDb.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetKeranjang(userId);
        }

        // DELETE: api/keranjang/{id}
        [HttpDelete("{id}")] // ID ini adalah ID KeranjangItem dari DB
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> RemoveFromCart(int id, [FromQuery] int userId) // userId sebagai parameter
        {
            if (userId == 0) return BadRequest("User ID diperlukan untuk menghapus item dari keranjang.");

            var keranjangDb = await GetOrCreateUserCartAsync(userId);
            var itemToRemove = keranjangDb.Items.FirstOrDefault(ci => ci.Id == id);

            if (itemToRemove == null)
            {
                return NotFound(new ErrorResponse { Status = 404, Message = $"Item keranjang dengan ID {id} tidak ditemukan." });
            }

            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == itemToRemove.PakaianKode);
            if (pakaian != null)
            {
                pakaian.Stok += itemToRemove.Quantity;
                pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
            }

            keranjangDb.Items.Remove(itemToRemove);
            keranjangDb.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetKeranjang(userId);
        }

        // DELETE: api/keranjang
        [HttpDelete] // Endpoint untuk mengosongkan seluruh keranjang
        [ProducesResponseType(typeof(KeranjangDto), 200)]
        public async Task<IActionResult> ClearCart([FromQuery] int userId) // userId sebagai parameter
        {
            if (userId == 0) return BadRequest("User ID diperlukan untuk mengosongkan keranjang.");

            var keranjangDb = await GetOrCreateUserCartAsync(userId);

            foreach (var item in keranjangDb.Items)
            {
                var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == item.PakaianKode);
                if (pakaian != null)
                {
                    pakaian.Stok += item.Quantity;
                    pakaian.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang);
                }
            }
            await _context.SaveChangesAsync();

            _context.KeranjangItems.RemoveRange(keranjangDb.Items);
            keranjangDb.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return await GetKeranjang(userId);
        }

        [HttpPost("checkout")]
        [ProducesResponseType(typeof(CheckoutResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto, [FromQuery] int userId)
        {
            if (userId == 0)
                return BadRequest("User ID diperlukan untuk checkout.");

            var keranjangDb = await GetOrCreateUserCartAsync(userId);
            if (keranjangDb == null || !keranjangDb.Items.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Keranjang kosong. Tidak ada yang bisa di-checkout."
                });
            }

            // Validasi
            foreach (var item in keranjangDb.Items)
            {
                var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == item.PakaianKode);
                if (pakaian == null || pakaian.Stok < item.Quantity || pakaian.Status != StatusPakaian.DalamKeranjang)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = 400,
                        Message = $"Pakaian '{pakaian?.Nama ?? item.PakaianKode}' tidak bisa di-checkout. " +
                                  $"Stok tidak cukup ({pakaian?.Stok ?? 0} tersedia, {item.Quantity} dibutuhkan) atau status tidak valid ({pakaian?.Status})."
                    });
                }
            }

            // Proses Checkout
            foreach (var item in keranjangDb.Items)
            {
                var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == item.PakaianKode);
                if (pakaian != null)
                {
                    pakaian.ProsesAksi(AksiPakaian.Pesan);
                    pakaian.ProsesAksi(AksiPakaian.Bayar);
                    pakaian.ProsesAksi(AksiPakaian.Kirim);
                    pakaian.ProsesAksi(AksiPakaian.Selesai);

                    pakaian.Stok -= item.Quantity;
                    pakaian.Status = StatusPakaian.Tersedia;
                }
            }

            // Simpan perubahan
            _context.KeranjangItems.RemoveRange(keranjangDb.Items);
            keranjangDb.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Bangun response
            var checkoutResponse = new CheckoutResponseDto
            {
                OrderId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                TanggalPemesanan = DateTime.Now,
                Items = keranjangDb.Items.Select(MapKeranjangItemToDto).ToList(),
                TotalHarga = keranjangDb.Items.Sum(item => item.TotalHargaItem),
                StatusPemesanan = "Selesai",
                AlamatPengiriman = checkoutDto.AlamatPengiriman,
                MetodePembayaran = checkoutDto.MetodePembayaran
            };

            return Ok(checkoutResponse);
        }



        private PakaianDto MapToDto(Pakaian p)
        {
            return new PakaianDto
            {
                Kode = p.Kode,
                Nama = p.Nama,
                Kategori = p.Kategori,
                Warna = p.Warna,
                Ukuran = p.Ukuran,
                Harga = p.Harga,
                Stok = p.Stok,
                Status = p.Status
            };
        }

    }
}
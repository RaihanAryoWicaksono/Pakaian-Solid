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
        private readonly IKeranjangService _keranjangService;
        private readonly ApplicationDbContext _context;

        public KeranjangController(IKeranjangService keranjangService, ApplicationDbContext context)
        {
            _context = context;
            _keranjangService = keranjangService;
        }

        // GET: api/keranjang
        [HttpGet]
        public async Task<IActionResult> GetKeranjang()
        {
            var keranjang = await _keranjangService.GetKeranjangAsync();
            return Ok(keranjang);
        }

        // POST: api/keranjang
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var success = await _keranjangService.TambahKeKeranjangAsync(dto.KodePakaian, dto.Quantity);
            if (!success)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Gagal menambahkan ke keranjang. Produk mungkin sudah ada, stok tidak cukup, atau tidak ditemukan."
                });
            }

            return Ok(new { Message = "Berhasil ditambahkan ke keranjang." });
        }

        // DELETE: api/keranjang/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var success = await _keranjangService.HapusItemAsync(id);
            if (!success)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = "Item tidak ditemukan atau gagal dihapus."
                });
            }

            return Ok(new { Message = "Item berhasil dihapus dari keranjang." });
        }

        // DELETE: api/keranjang
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var success = await _keranjangService.KosongkanKeranjangAsync();
            if (!success)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Gagal mengosongkan keranjang."
                });
            }

            return Ok(new { Message = "Keranjang berhasil dikosongkan." });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
        {
            var items = await _keranjangService.GetEntityItemsAsync(); // Harus return List<Keranjang>

            if (items == null || items.Count == 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = "Keranjang kosong."
                });
            }

            // Validasi stok & status
            foreach (var item in items)
            {
                if (item.Pakaian == null || item.Pakaian.Stok < item.Quantity || item.Pakaian.Status != StatusPakaian.DalamKeranjang)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = 400,
                        Message = $"Pakaian '{item.Pakaian?.Nama ?? "Tidak diketahui"}' tidak dapat di-checkout."
                    });
                }
            }

            string orderId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            foreach (var item in items)
            {
                var pakaian = item.Pakaian;

                pakaian.ProsesAksi(AksiPakaian.Pesan);
                pakaian.ProsesAksi(AksiPakaian.Bayar);
                pakaian.ProsesAksi(AksiPakaian.Kirim);
                pakaian.ProsesAksi(AksiPakaian.Selesai);

                pakaian.Stok -= item.Quantity;
                pakaian.Status = StatusPakaian.Tersedia;

                await _keranjangService.HapusItemAsync(item.Id);
            }

            var checkoutResponse = new CheckoutResponseDto
            {
                OrderId = orderId,
                TanggalPemesanan = DateTime.Now,
                Items = items.Select(k => new KeranjangItemDto
                {
                    Id = k.Id,
                    KodePakaian = k.KodePakaian,
                    Pakaian = new PakaianDto
                    {
                        Kode = k.Pakaian.Kode,
                        Nama = k.Pakaian.Nama,
                        Kategori = k.Pakaian.Kategori,
                        Warna = k.Pakaian.Warna,
                        Ukuran = k.Pakaian.Ukuran,
                        Harga = k.Pakaian.Harga,
                        Stok = k.Pakaian.Stok,
                        Status = k.Pakaian.Status
                    },
                    Quantity = k.Quantity,
                    TotalHarga = k.Pakaian.Harga * k.Quantity,
                    TanggalDitambahkan = k.TanggalDitambahkan
                }).ToList(),
                TotalHarga = items.Sum(k => k.Pakaian.Harga * k.Quantity),
                StatusPemesanan = "DalamPengiriman",
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
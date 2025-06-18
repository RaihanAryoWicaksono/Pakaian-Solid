// PakaianApi/Controllers/KatalogController.cs
using Microsoft.AspNetCore.Mvc;
using PakaianApi.Data;
using PakaianApi.Models;
using PakaianLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KatalogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PakaianDto>), 200)]
        public async Task<IActionResult> GetAllPakaian()
        {
            var pakaianList = await _context.Pakaian.ToListAsync();
            var pakaianDtos = pakaianList.Select(MapToDto).ToList();
            return Ok(pakaianDtos);
        }

        [HttpGet("{kode}")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetPakaianByKode(string kode)
        {
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == kode);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {kode} tidak ditemukan"
                });
            }

            var pakaianDto = MapToDto(pakaian);
            return Ok(pakaianDto);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<PakaianDto>), 200)]
        public async Task<IActionResult> SearchPakaian([FromQuery] string keyword = null,
                                                       [FromQuery] string kategori = null,
                                                       [FromQuery] decimal? minHarga = null,
                                                       [FromQuery] decimal? maxHarga = null)
        {
            IQueryable<Pakaian> query = _context.Pakaian;

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Nama.Contains(keyword) ||
                                         p.Kategori.Contains(keyword) ||
                                         p.Warna.Contains(keyword) ||
                                         p.Ukuran.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(kategori))
            {
                query = query.Where(p => p.Kategori == kategori);
            }

            if (minHarga.HasValue)
            {
                query = query.Where(p => p.Harga >= minHarga.Value);
            }

            if (maxHarga.HasValue)
            {
                query = query.Where(p => p.Harga <= maxHarga.Value);
            }

            var hasilPencarian = await query.ToListAsync();
            var pakaianDtos = hasilPencarian.Select(MapToDto).ToList();
            return Ok(pakaianDtos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PakaianDto), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> AddPakaian([FromBody] CreatePakaianDto createDto, [FromQuery] string username)
        {
            var (success, role) = await AuthController.TryGetUserRoleAsync(_context, username);
            if (!success || role != UserRole.Admin)
            {
                return Forbid("Hanya admin yang dapat menambahkan pakaian.");
            }

            if (await _context.Pakaian.AnyAsync(p => p.Kode == createDto.Kode))
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Pakaian dengan kode {createDto.Kode} sudah ada"
                });
            }

            var pakaianBaru = new Pakaian(
                createDto.Kode,
                createDto.Nama,
                createDto.Kategori,
                createDto.Warna,
                createDto.Ukuran,
                createDto.Harga,
                createDto.Stok
            );

            _context.Pakaian.Add(pakaianBaru);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPakaianByKode), new { kode = pakaianBaru.Kode }, MapToDto(pakaianBaru));
        }

        [HttpPut("{kode}")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdatePakaian(string kode, [FromBody] UpdatePakaianDto updateDto)
        {
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == kode);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {kode} tidak ditemukan"
                });
            }

            if (updateDto.Nama != null) pakaian.Nama = updateDto.Nama;
            if (updateDto.Kategori != null) pakaian.Kategori = updateDto.Kategori;
            if (updateDto.Warna != null) pakaian.Warna = updateDto.Warna;
            if (updateDto.Ukuran != null) pakaian.Ukuran = updateDto.Ukuran;
            if (updateDto.Harga.HasValue) pakaian.Harga = updateDto.Harga.Value;
            if (updateDto.Stok.HasValue) pakaian.Stok = updateDto.Stok.Value;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(MapToDto(pakaian));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal memperbarui pakaian dengan kode {kode}. Konflik konkurensi."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal memperbarui pakaian dengan kode {kode}: {ex.Message}"
                });
            }
        }

        [HttpDelete("{kode}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> DeletePakaian(string kode)
        {
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == kode);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {kode} tidak ditemukan"
                });
            }

            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Pakaian dengan kode {kode} tidak dapat dihapus karena status saat ini: {pakaian.Status}"
                });
            }

            _context.Pakaian.Remove(pakaian);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("aksi")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> ProsesAksi([FromBody] ProsesAksiDto aksiDto)
        {
            var pakaian = await _context.Pakaian.FirstOrDefaultAsync(p => p.Kode == aksiDto.KodePakaian);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {aksiDto.KodePakaian} tidak ditemukan"
                });
            }

            bool berhasil = pakaian.ProsesAksi(aksiDto.Aksi);
            if (!berhasil)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Aksi {aksiDto.Aksi} tidak valid untuk pakaian '{pakaian.Nama}' dalam status {pakaian.Status}"
                });
            }

            await _context.SaveChangesAsync();
            return Ok(MapToDto(pakaian));
        }

        private PakaianDto MapToDto(Pakaian pakaian)
        {
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
    }
}

using Microsoft.AspNetCore.Mvc;
using Pakaianku;
using PakaianApi.Models;
using PakaianLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KatalogController : ControllerBase
    {
        private readonly KatalogPakaian _katalog;

        public KatalogController(KatalogPakaian katalog)
        {
            _katalog = katalog;
        }

        /// <summary>
        /// Mendapatkan semua pakaian dalam katalog
        /// </summary>
        /// <returns>Daftar semua pakaian</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PakaianDto>), 200)]
        public IActionResult GetAllPakaian()
        {
            var pakaianList = _katalog.GetSemuaPakaian();
            var pakaianDtos = pakaianList.Select(MapToDto).ToList();
            return Ok(pakaianDtos);
        }

        /// <summary>
        /// Mendapatkan pakaian berdasarkan kode
        /// </summary>
        /// <param name="kode">Kode pakaian</param>
        /// <returns>Pakaian yang ditemukan</returns>
        [HttpGet("{kode}")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult GetPakaianByKode(string kode)
        {
            var pakaian = _katalog.CariPakaianByKode(kode);
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

        /// <summary>
        /// Mencari pakaian berdasarkan kriteria
        /// </summary>
        /// <param name="keyword">Kata kunci pencarian</param>
        /// <param name="kategori">Kategori pakaian</param>
        /// <param name="minHarga">Harga minimum</param>
        /// <param name="maxHarga">Harga maksimum</param>
        /// <returns>Daftar pakaian yang sesuai dengan kriteria</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<PakaianDto>), 200)]
        public IActionResult SearchPakaian([FromQuery] string keyword = null,
                                           [FromQuery] string kategori = null,
                                           [FromQuery] decimal? minHarga = null,
                                           [FromQuery] decimal? maxHarga = null)
        {
            List<Pakaian> hasilPencarian = new List<Pakaian>();

            if (!string.IsNullOrEmpty(keyword))
            {
                hasilPencarian = _katalog.CariPakaian(keyword);
            }
            else if (!string.IsNullOrEmpty(kategori))
            {
                hasilPencarian = _katalog.CariPakaianByKategori(kategori);
            }
            else if (minHarga.HasValue && maxHarga.HasValue)
            {
                hasilPencarian = _katalog.CariPakaianByHarga(minHarga.Value, maxHarga.Value);
            }
            else
            {
                hasilPencarian = _katalog.GetSemuaPakaian();
            }

            var pakaianDtos = hasilPencarian.Select(MapToDto).ToList();
            return Ok(pakaianDtos);
        }

        /// <summary>
        /// Menambahkan pakaian baru ke katalog (hanya untuk admin)
        /// </summary>
        /// <param name="createDto">Data pakaian baru</param>
        /// <returns>Pakaian yang baru ditambahkan</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PakaianDto), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult AddPakaian([FromBody] CreatePakaianDto createDto, [FromQuery] string username)
        {
            // Cek role
            if (!AuthController.TryGetUserRole(username, out var role) || role != PakaianApi.Models.UserRole.Admin)
            {
                return Forbid("Hanya admin yang dapat menambahkan pakaian.");
            }

            if (_katalog.CariPakaianByKode(createDto.Kode) != null)
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

            _katalog.TambahPakaian(pakaianBaru);
            return CreatedAtAction(nameof(GetPakaianByKode), new { kode = pakaianBaru.Kode }, MapToDto(pakaianBaru));
        }
        //public IActionResult AddPakaian([FromBody] CreatePakaianDto createDto)
        //{
        //    // Cek jika kode sudah ada
        //    if (_katalog.CariPakaianByKode(createDto.Kode) != null)
        //    {
        //        return BadRequest(new ErrorResponse
        //        {
        //            Status = 400,
        //            Message = $"Pakaian dengan kode {createDto.Kode} sudah ada"
        //        });
        //    }

        //    var pakaianBaru = new Pakaian(
        //        createDto.Kode,
        //        createDto.Nama,
        //        createDto.Kategori,
        //        createDto.Warna,
        //        createDto.Ukuran,
        //        createDto.Harga,
        //        createDto.Stok
        //    );

        //    _katalog.TambahPakaian(pakaianBaru);

        //    return CreatedAtAction(nameof(GetPakaianByKode), new { kode = pakaianBaru.Kode }, MapToDto(pakaianBaru));
        //}

        /// <summary>
        /// Memperbarui data pakaian yang sudah ada
        /// </summary>
        /// <param name="kode">Kode pakaian</param>
        /// <param name="updateDto">Data pakaian yang diperbarui</param>
        /// <returns>Pakaian yang telah diperbarui</returns>
        [HttpPut("{kode}")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult UpdatePakaian(string kode, [FromBody] UpdatePakaianDto updateDto)
        {
            var pakaian = _katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {kode} tidak ditemukan"
                });
            }

            // Update pakaian menggunakan method yang baru ditambahkan
            bool berhasil = _katalog.UpdatePakaian(
                kode,
                updateDto.Nama,
                updateDto.Kategori,
                updateDto.Warna,
                updateDto.Ukuran,
                updateDto.Harga,
                updateDto.Stok
            );

            if (!berhasil)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal memperbarui pakaian dengan kode {kode}"
                });
            }

            // Ambil kembali pakaian yang sudah diupdate
            var updatedPakaian = _katalog.CariPakaianByKode(kode);

            return Ok(MapToDto(updatedPakaian));
        }

        /// <summary>
        /// Menghapus pakaian dari katalog
        /// </summary>
        /// <param name="kode">Kode pakaian</param>
        /// <returns>Status penghapusan</returns>
        [HttpDelete("{kode}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult DeletePakaian(string kode)
        {
            var pakaian = _katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = $"Pakaian dengan kode {kode} tidak ditemukan"
                });
            }

            // Cek apakah pakaian sedang dalam proses (tidak bisa dihapus)
            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Pakaian dengan kode {kode} tidak dapat dihapus karena status saat ini: {pakaian.Status}"
                });
            }

            // Implementasi penghapusan pakaian menggunakan method yang baru ditambahkan
            bool berhasil = _katalog.HapusPakaian(kode);
            if (!berhasil)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Message = $"Gagal menghapus pakaian dengan kode {kode}"
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Memproses aksi pada pakaian
        /// </summary>
        /// <param name="aksiDto">Data aksi</param>
        /// <returns>Status hasil pemrosesan aksi</returns>
        [HttpPost("aksi")]
        [ProducesResponseType(typeof(PakaianDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult ProsesAksi([FromBody] ProsesAksiDto aksiDto)
        {
            var pakaian = _katalog.CariPakaianByKode(aksiDto.KodePakaian);
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

            return Ok(MapToDto(pakaian));
        }

        // Helper method untuk mapping Pakaian ke PakaianDto
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
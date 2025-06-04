using Microsoft.AspNetCore.Mvc;
using Pakaianku; // Mengakses kelas Pakaian, KatalogPakaian
using PakaianLib; // Mengakses enum StatusPakaian, AksiPakaian
using System.Linq; // Untuk LINQ

namespace Pakaianku.Controllers
{
    public class PakaianController : Controller
    {
        private readonly KatalogPakaian _katalog;

        // Dependency Injection: KatalogPakaian akan diinjeksikan secara otomatis
        public PakaianController(KatalogPakaian katalog)
        {
            _katalog = katalog;
        }

        // GET: /Pakaian (atau /Pakaian/Index)
        public IActionResult Index()
        {
            var semuaPakaian = _katalog.GetSemuaPakaian();
            return View(semuaPakaian); // Meneruskan daftar pakaian ke View
        }

        // GET: /Pakaian/Details/{kode}
        public IActionResult Details(string id) // 'id' akan di-bind dari {id?} di route
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var pakaian = _katalog.CariPakaianByKode(id);
            if (pakaian == null)
            {
                return NotFound();
            }
            return View(pakaian);
        }

        // GET: /Pakaian/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Pakaian/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Penting untuk keamanan
        public IActionResult Create(Pakaian pakaian)
        {
            if (ModelState.IsValid) // Memeriksa validasi model (jika ada Data Annotations di kelas Pakaian)
            {
                var existingPakaian = _katalog.CariPakaianByKode(pakaian.Kode);
                if (existingPakaian != null)
                {
                    ModelState.AddModelError("Kode", "Pakaian dengan kode ini sudah ada.");
                    return View(pakaian);
                }

                // Inisialisasi status berdasarkan stok jika tidak diatur secara eksplisit
                if (pakaian.Stok > 0)
                {
                    pakaian.Status = StatusPakaian.Tersedia;
                }
                else
                {
                    pakaian.Status = StatusPakaian.TidakTersedia;
                }

                _katalog.TambahPakaian(pakaian);
                return RedirectToAction(nameof(Index)); // Redirect ke halaman daftar setelah berhasil
            }
            return View(pakaian); // Kembali ke form jika ada error validasi
        }

        // GET: /Pakaian/Edit/{kode}
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var pakaian = _katalog.CariPakaianByKode(id);
            if (pakaian == null)
            {
                return NotFound();
            }
            return View(pakaian);
        }

        // POST: /Pakaian/Edit/{kode}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Pakaian pakaian) // 'id' dari URL, 'pakaian' dari form
        {
            if (id != pakaian.Kode) // Pastikan kode di URL cocok dengan kode di model
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Ambil pakaian yang ada dari database untuk mempertahankan properti yang tidak diupdate
                var existingPakaian = _katalog.CariPakaianByKode(id);
                if (existingPakaian == null)
                {
                    return NotFound();
                }

                // Perbarui properti yang relevan dari existingPakaian dengan nilai dari form
                existingPakaian.Nama = pakaian.Nama;
                existingPakaian.Kategori = pakaian.Kategori;
                existingPakaian.Warna = pakaian.Warna;
                existingPakaian.Ukuran = pakaian.Ukuran;
                existingPakaian.Harga = pakaian.Harga;
                existingPakaian.Stok = pakaian.Stok;
                existingPakaian.Status = pakaian.Status; // Izinkan update status langsung

                // Panggil metode UpdatePakaian di KatalogPakaian
                bool updated = _katalog.UpdatePakaian(
                    existingPakaian.Kode,
                    existingPakaian.Nama,
                    existingPakaian.Kategori,
                    existingPakaian.Warna,
                    existingPakaian.Ukuran,
                    existingPakaian.Harga,
                    existingPakaian.Stok,
                    existingPakaian.Status
                );

                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Gagal memperbarui pakaian. Mungkin tidak ada perubahan atau terjadi kesalahan.");
                }
            }
            return View(pakaian);
        }

        // GET: /Pakaian/Delete/{kode}
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var pakaian = _katalog.CariPakaianByKode(id);
            if (pakaian == null)
            {
                return NotFound();
            }
            return View(pakaian);
        }

        // POST: /Pakaian/Delete/{kode}
        [HttpPost, ActionName("Delete")] // ActionName agar tidak bentrok dengan GET Delete
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var pakaianToDelete = _katalog.CariPakaianByKode(id);
            if (pakaianToDelete == null)
            {
                return NotFound();
            }

            // Periksa status sebelum menghapus (sesuai logika CLI Anda)
            if (pakaianToDelete.Status != StatusPakaian.Tersedia && pakaianToDelete.Status != StatusPakaian.TidakTersedia)
            {
                // Tambahkan pesan kesalahan ke ModelState dan kembali ke view
                ModelState.AddModelError("", $"Pakaian '{pakaianToDelete.Nama}' dengan status '{pakaianToDelete.Status}' tidak dapat dihapus. Status harus 'Tersedia' atau 'TidakTersedia'.");
                return View("Delete", pakaianToDelete); // Kembali ke view Delete dengan pesan error
            }

            if (_katalog.HapusPakaian(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Tambahkan pesan kesalahan ke ModelState dan kembali ke view
                ModelState.AddModelError("", "Gagal menghapus pakaian. Terjadi kesalahan.");
                return View("Delete", pakaianToDelete);
            }
        }

        // POST: /Pakaian/ProcessAction
        [HttpPost]
        public IActionResult ProcessAction(string kode, AksiPakaian aksi)
        {
            var pakaian = _katalog.CariPakaianByKode(kode);
            if (pakaian == null)
            {
                return NotFound();
            }

            // Simpan status dan stok saat ini untuk memeriksa perubahan
            var currentStatus = pakaian.Status;
            var currentStok = pakaian.Stok;

            if (pakaian.ProsesAksi(aksi))
            {
                // Hanya update jika ada perubahan status atau stok
                if (currentStatus != pakaian.Status || currentStok != pakaian.Stok)
                {
                    _katalog.UpdatePakaian(pakaian.Kode, stok: pakaian.Stok, status: pakaian.Status);
                }
                // Setelah aksi berhasil, redirect kembali ke halaman detail atau index
                return RedirectToAction(nameof(Details), new { id = kode });
            }
            else
            {
                // Jika aksi tidak valid, tambahkan error ke ModelState dan kembali ke view Details
                ModelState.AddModelError("", $"Aksi {aksi} tidak valid untuk pakaian '{pakaian.Nama}' dalam status {pakaian.Status}.");
                return View(nameof(Details), pakaian); // Kembali ke view Details dengan pesan error
            }
        }
    }
}

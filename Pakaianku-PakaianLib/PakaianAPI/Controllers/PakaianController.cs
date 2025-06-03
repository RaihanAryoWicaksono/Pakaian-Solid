using Microsoft.AspNetCore.Mvc;
using Pakaianku; 
using PakaianLib; 
using PakaianApi.Models;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rute dasar untuk controller ini akan menjadi /api/Pakaian
    public class PakaianController : ControllerBase
    {
        private readonly KatalogPakaian _katalog;
        // KeranjangBelanja tetap in-memory untuk sesi API saat ini
        private static KeranjangBelanja<Pakaian> _keranjang = new KeranjangBelanja<Pakaian>();

        // Constructor akan menerima KatalogPakaian melalui Dependency Injection
        public PakaianController(KatalogPakaian katalog)
        {
            _katalog = katalog;
            // Inisialisasi katalog awal hanya jika database kosong
            if (!_katalog.GetSemuaPakaian().Any())
            {
                InisialisasiKatalogAwal();
            }
        }

        // Metode ini hanya akan dijalankan jika database kosong
        private void InisialisasiKatalogAwal()
        {
            _katalog.TambahPakaian(new Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10));
            _katalog.TambahPakaian(new Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8));
            _katalog.TambahPakaian(new Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5));
            _katalog.TambahPakaian(new Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15));
            _katalog.TambahPakaian(new Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12));
            _katalog.TambahPakaian(new Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7));
            _katalog.TambahPakaian(new Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8));
            _katalog.TambahPakaian(new Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6));
            _katalog.TambahPakaian(new Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10));
            _katalog.TambahPakaian(new Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5));
            _katalog.TambahPakaian(new Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4));
            _katalog.TambahPakaian(new Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7));
        }

        // GET: api/Pakaian
        // Mengambil semua pakaian dari katalog
        [HttpGet]
        public ActionResult<IEnumerable<PakaianDto>> GetSemuaPakaian()
        {
            try
            {
                var pakaianList = _katalog.GetSemuaPakaian().Select(p => new PakaianDto
                {
                    Kode = p.Kode,
                    Nama = p.Nama,
                    Kategori = p.Kategori,
                    Warna = p.Warna,
                    Ukuran = p.Ukuran,
                    Harga = p.Harga,
                    Stok = p.Stok,
                    Status = p.Status
                }).ToList();
                return Ok(pakaianList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Pakaian/{kode}
        // Mengambil pakaian berdasarkan kode
        [HttpGet("{kode}")]
        public ActionResult<PakaianDto> GetPakaianByKode(string kode)
        {
            try
            {
                var pakaian = _katalog.CariPakaianByKode(kode);
                if (pakaian == null)
                {
                    return NotFound($"Pakaian dengan kode '{kode}' tidak ditemukan.");
                }

                // Memetakan Pakaian ke PakaianDto
                var pakaianDto = new PakaianDto
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
                return Ok(pakaianDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Pakaian/search?keyword=kemeja
        // Mencari pakaian berdasarkan kata kunci
        [HttpGet("search")]
        public ActionResult<IEnumerable<PakaianDto>> SearchPakaian([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Kata kunci pencarian tidak boleh kosong.");
            }
            try
            {
                var hasilPencarian = _katalog.CariPakaian(keyword).Select(p => new PakaianDto
                {
                    Kode = p.Kode,
                    Nama = p.Nama,
                    Kategori = p.Kategori,
                    Warna = p.Warna,
                    Ukuran = p.Ukuran,
                    Harga = p.Harga,
                    Stok = p.Stok,
                    Status = p.Status
                }).ToList();

                if (!hasilPencarian.Any())
                {
                    return NotFound($"Tidak ada pakaian yang ditemukan untuk kata kunci '{keyword}'.");
                }
                return Ok(hasilPencarian);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Pakaian/category/{kategori}
        // Mencari pakaian berdasarkan kategori
        [HttpGet("category/{kategori}")]
        public ActionResult<IEnumerable<PakaianDto>> GetPakaianByKategori(string kategori)
        {
            if (string.IsNullOrWhiteSpace(kategori))
            {
                return BadRequest("Kategori tidak boleh kosong.");
            }
            try
            {
                var hasilPencarian = _katalog.CariPakaianByKategori(kategori).Select(p => new PakaianDto
                {
                    Kode = p.Kode,
                    Nama = p.Nama,
                    Kategori = p.Kategori,
                    Warna = p.Warna,
                    Ukuran = p.Ukuran,
                    Harga = p.Harga,
                    Stok = p.Stok,
                    Status = p.Status
                }).ToList();

                if (!hasilPencarian.Any())
                {
                    return NotFound($"Tidak ada pakaian yang ditemukan untuk kategori '{kategori}'.");
                }
                return Ok(hasilPencarian);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Pakaian/price-range?min=100000&max=300000
        // Mencari pakaian berdasarkan rentang harga
        [HttpGet("price-range")]
        public ActionResult<IEnumerable<PakaianDto>> GetPakaianByHarga([FromQuery] decimal min, [FromQuery] decimal max)
        {
            if (min < 0 || max < 0 || min > max)
            {
                return BadRequest("Rentang harga tidak valid.");
            }
            try
            {
                var hasilPencarian = _katalog.CariPakaianByHarga(min, max).Select(p => new PakaianDto
                {
                    Kode = p.Kode,
                    Nama = p.Nama,
                    Kategori = p.Kategori,
                    Warna = p.Warna,
                    Ukuran = p.Ukuran,
                    Harga = p.Harga,
                    Stok = p.Stok,
                    Status = p.Status
                }).ToList();

                if (!hasilPencarian.Any())
                {
                    return NotFound($"Tidak ada pakaian yang ditemukan dalam rentang harga Rp{min:N0} - Rp{max:N0}.");
                }
                return Ok(hasilPencarian);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Pakaian
        // Menambahkan pakaian baru
        [HttpPost]
        public ActionResult<PakaianDto> AddPakaian([FromBody] PakaianDto newPakaianDto)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(newPakaianDto.Kode) || string.IsNullOrWhiteSpace(newPakaianDto.Nama) ||
                string.IsNullOrWhiteSpace(newPakaianDto.Kategori) || newPakaianDto.Harga <= 0 || newPakaianDto.Stok < 0)
            {
                return BadRequest("Data pakaian tidak lengkap atau tidak valid.");
            }

            try
            {
                // Periksa apakah kode pakaian sudah ada
                if (_katalog.CariPakaianByKode(newPakaianDto.Kode) != null)
                {
                    return Conflict($"Pakaian dengan kode '{newPakaianDto.Kode}' sudah ada.");
                }

                var pakaian = new Pakaian(newPakaianDto.Kode, newPakaianDto.Nama, newPakaianDto.Kategori,
                                          newPakaianDto.Warna, newPakaianDto.Ukuran, newPakaianDto.Harga, newPakaianDto.Stok);
                _katalog.TambahPakaian(pakaian);

                // Mengembalikan pakaian yang baru ditambahkan
                newPakaianDto.Status = pakaian.Status; // Pastikan status diperbarui setelah pembuatan
                return CreatedAtAction(nameof(GetPakaianByKode), new { kode = newPakaianDto.Kode }, newPakaianDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Pakaian/{kode}
        // Memperbarui informasi pakaian
        [HttpPut("{kode}")]
        public IActionResult UpdatePakaian(string kode, [FromBody] UpdatePakaianRequest updateRequest)
        {
            try
            {
                var pakaian = _katalog.CariPakaianByKode(kode);
                if (pakaian == null)
                {
                    return NotFound($"Pakaian dengan kode '{kode}' tidak ditemukan.");
                }

                // Panggil metode UpdatePakaian di KatalogPakaian dengan semua parameter
                if (_katalog.UpdatePakaian(kode,
                                            nama: updateRequest.Nama,
                                            kategori: updateRequest.Kategori,
                                            warna: updateRequest.Warna,
                                            ukuran: updateRequest.Ukuran,
                                            harga: updateRequest.Harga,
                                            stok: updateRequest.Stok,
                                            status: updateRequest.Status))
                {
                    return NoContent(); // 204 No Content, menunjukkan update berhasil tanpa mengembalikan data
                }
                else
                {
                    return BadRequest("Gagal memperbarui pakaian. Periksa input Anda.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Pakaian/{kode}
        // Menghapus pakaian
        [HttpDelete("{kode}")]
        public IActionResult DeletePakaian(string kode)
        {
            try
            {
                var pakaian = _katalog.CariPakaianByKode(kode);
                if (pakaian == null)
                {
                    return NotFound($"Pakaian dengan kode '{kode}' tidak ditemukan.");
                }

                // Periksa status pakaian sebelum menghapus
                if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
                {
                    return BadRequest($"Pakaian dengan kode '{kode}' tidak dapat dihapus karena statusnya '{pakaian.Status}'.");
                }

                if (_katalog.HapusPakaian(kode))
                {
                    return NoContent(); // 204 No Content
                }
                else
                {
                    return StatusCode(500, "Gagal menghapus pakaian."); // Kesalahan server internal
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Pakaian/process-action
        // Memproses aksi pada pakaian (misalnya, TambahKeKeranjang, Pesan, dll.)
        [HttpPost("process-action")]
        public IActionResult ProcessPakaianAction([FromBody] AksiPakaianRequest request)
        {
            try
            {
                var pakaian = _katalog.CariPakaianByKode(request.KodePakaian);
                if (pakaian == null)
                {
                    return NotFound($"Pakaian dengan kode '{request.KodePakaian}' tidak ditemukan.");
                }

                if (pakaian.ProsesAksi(request.Aksi))
                {
                    // Setelah memproses aksi, update status dan stok di database
                    _katalog.UpdatePakaian(pakaian.Kode, stok: pakaian.Stok, status: pakaian.Status);

                    // Mengembalikan status pakaian yang diperbarui
                    return Ok(new PakaianDto
                    {
                        Kode = pakaian.Kode,
                        Nama = pakaian.Nama,
                        Kategori = pakaian.Kategori,
                        Warna = pakaian.Warna,
                        Ukuran = pakaian.Ukuran,
                        Harga = pakaian.Harga,
                        Stok = pakaian.Stok,
                        Status = pakaian.Status
                    });
                }
                else
                {
                    return BadRequest($"Aksi '{request.Aksi}' tidak valid untuk pakaian '{pakaian.Nama}' dalam status '{pakaian.Status}'.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Pakaian/cart
        // Mengambil semua item di keranjang
        [HttpGet("cart")]
        public ActionResult<IEnumerable<PakaianDto>> GetCartItems()
        {
            var cartItems = _keranjang.GetSemuaItem().Select(p => new PakaianDto
            {
                Kode = p.Kode,
                Nama = p.Nama,
                Kategori = p.Kategori,
                Warna = p.Warna,
                Ukuran = p.Ukuran,
                Harga = p.Harga,
                Stok = p.Stok,
                Status = p.Status
            }).ToList();
            return Ok(cartItems);
        }

        // POST: api/Pakaian/cart/add
        // Menambahkan pakaian ke keranjang
        [HttpPost("cart/add")]
        public IActionResult AddToCart([FromBody] AksiPakaianRequest request)
        {
            try
            {
                var pakaian = _katalog.CariPakaianByKode(request.KodePakaian);
                if (pakaian == null)
                {
                    return NotFound($"Pakaian dengan kode '{request.KodePakaian}' tidak ditemukan.");
                }

                // Memproses aksi TambahKeKeranjang pada objek pakaian
                if (pakaian.ProsesAksi(AksiPakaian.TambahKeKeranjang))
                {
                    // Update stok dan status di database
                    _katalog.UpdatePakaian(pakaian.Kode, stok: pakaian.Stok, status: pakaian.Status);

                    // Menambahkan pakaian ke keranjang belanja in-memory
                    _keranjang.TambahKeKeranjang(pakaian);
                    return Ok(new { message = $"Pakaian '{pakaian.Nama}' berhasil ditambahkan ke keranjang.", currentCartCount = _keranjang.JumlahItem() });
                }
                else
                {
                    return BadRequest($"Gagal menambahkan pakaian '{pakaian.Nama}' ke keranjang. Status saat ini: {pakaian.Status}. Stok: {pakaian.Stok}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Pakaian/cart/remove/{index}
        // Menghapus item dari keranjang berdasarkan indeks
        [HttpDelete("cart/remove/{index}")]
        public IActionResult RemoveFromCart(int index)
        {
            try
            {
                // Index di API biasanya berbasis 0, sesuaikan jika di frontend berbasis 1
                if (index < 0 || index >= _keranjang.JumlahItem())
                {
                    return BadRequest("Indeks item keranjang tidak valid.");
                }

                var itemToRemove = _keranjang.GetSemuaItem()[index];

                if (_keranjang.KeluarkanDariKeranjangByIndex(index))
                {
                    // Kembalikan status pakaian di katalog ke Tersedia atau tambahkan stok
                    itemToRemove.ProsesAksi(AksiPakaian.KeluarkanDariKeranjang); // Mengembalikan stok
                    // Update stok dan status di database
                    _katalog.UpdatePakaian(itemToRemove.Kode, stok: itemToRemove.Stok, status: itemToRemove.Status);

                    return Ok(new { message = $"Item berhasil dihapus dari keranjang.", currentCartCount = _keranjang.JumlahItem() });
                }
                else
                {
                    return StatusCode(500, "Gagal menghapus item dari keranjang.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Pakaian/cart/checkout
        // Melakukan checkout keranjang
        [HttpPost("cart/checkout")]
        public IActionResult CheckoutCart()
        {
            try
            {
                if (_keranjang.JumlahItem() == 0)
                {
                    return BadRequest("Keranjang kosong. Tidak ada yang bisa di-checkout.");
                }

                decimal total = _keranjang.HitungTotal();
                List<Pakaian> itemKeranjang = _keranjang.GetSemuaItem();

                // Lakukan proses checkout untuk setiap item
                foreach (var item in new List<Pakaian>(itemKeranjang)) // Buat salinan untuk menghindari modifikasi koleksi saat iterasi
                {
                    // Proses transisi status pakaian
                    item.ProsesAksi(AksiPakaian.Pesan);
                    item.ProsesAksi(AksiPakaian.Bayar);
                    item.ProsesAksi(AksiPakaian.Kirim);

                    // Update status pakaian di database setelah setiap aksi
                    _katalog.UpdatePakaian(item.Kode, status: item.Status);
                }

                _keranjang.KosongkanKeranjang(); // Kosongkan keranjang setelah checkout
                return Ok(new { message = "Checkout berhasil! Pesanan Anda sedang dalam pengiriman.", totalPembelian = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
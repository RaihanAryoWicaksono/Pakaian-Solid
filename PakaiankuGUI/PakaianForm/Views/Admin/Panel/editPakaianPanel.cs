// PakaianForm/Views/Admin/Panel/panelEditPakaian.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk DTO (PakaianDtos)
using PakaianForm.Services;
using PakaianLib; // Untuk StatusPakaian, AksiPakaian (enum)
using System.Linq; // Untuk Enum.GetNames

namespace PakaianForm.Views.Admin.Panel
{
    public partial class panelEditPakaian : UserControl
    {
        private PakaianDtos _pakaianToEdit; // Menggunakan PakaianDtos

        public event Action<PakaianDtos> OnPakaianUpdated; // Menggunakan PakaianDtos di event
        public event Action<UserControl> OnNavigateToPanel; // Untuk navigasi kembali ke KelolaPakaian

        // --- Kontrol UI yang diasumsikan dari desainer Anda ---
        // Anda HARUS memastikan nama-nama ini cocok dengan yang ada di Designer.cs Anda
        private Guna.UI2.WinForms.Guna2TextBox tbKode;
        private Guna.UI2.WinForms.Guna2TextBox tbNama;
        private Guna.UI2.WinForms.Guna2TextBox tbKategori;
        private Guna.UI2.WinForms.Guna2TextBox tbWarna;
        private Guna.UI2.WinForms.Guna2TextBox tbUkuran;
        private Guna.UI2.WinForms.Guna2TextBox tbHarga;
        private Guna.UI2.WinForms.Guna2TextBox tbStok;
        private Guna.UI2.WinForms.Guna2ComboBox cbStatus; // Diasumsikan ComboBox untuk Status
        private PictureBox pbPakaian; // PictureBox untuk gambar pakaian (DIJAGA, untuk menampilkan)

        // Tombol terkait foto DIHAPUS
        // private Guna.UI2.WinForms.Guna2Button btnUploadFoto;
        // private Guna.UI2.WinForms.Guna2Button btnHapusFoto;

        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private Guna.UI2.WinForms.Guna2Button btnBack;

        public panelEditPakaian(PakaianDtos pakaian) // <--- Menggunakan PakaianDtos di konstruktor
        {
            InitializeComponent();
            _pakaianToEdit = pakaian;

            InitializeCustomControls(); // Metode untuk inisialisasi custom

            this.Load += PanelEditPakaian_Load;
        }

        private void InitializeCustomControls()
        {
            // Inisialisasi Guna2TextBox (jika belum ada di Designer.cs)
            // Anda perlu menyesuaikan lokasi dan ukuran
            tbKode = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 100), Size = new Size(250, 30), ReadOnly = true };
            tbNama = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 140), Size = new Size(250, 30) };
            tbKategori = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 180), Size = new Size(250, 30) };
            tbWarna = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 220), Size = new Size(250, 30) };
            tbUkuran = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 260), Size = new Size(250, 30) };
            tbHarga = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 300), Size = new Size(250, 30) };
            tbStok = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(200, 340), Size = new Size(250, 30) };
            cbStatus = new Guna.UI2.WinForms.Guna2ComboBox { Location = new Point(200, 380), Size = new Size(250, 30) };
            pbPakaian = new PictureBox { Location = new Point(500, 100), Size = new Size(150, 150), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };

            // Tambahkan item ke ComboBox Status dari enum PakaianLib.StatusPakaian
            cbStatus.DataSource = Enum.GetNames(typeof(PakaianLib.StatusPakaian)).ToList();

            btnSave = new Guna.UI2.WinForms.Guna2Button { Text = "Save", Location = new Point(500, 340), Size = new Size(150, 30), FillColor = Color.LimeGreen, ForeColor = Color.White };
            btnReset = new Guna.UI2.WinForms.Guna2Button { Text = "Reset", Location = new Point(500, 380), Size = new Size(150, 30), FillColor = Color.Orange, ForeColor = Color.White };
            btnBack = new Guna.UI2.WinForms.Guna2Button { Text = "Back", Location = new Point(500, 420), Size = new Size(150, 30), FillColor = Color.Gray, ForeColor = Color.White };


            this.Controls.Add(tbKode);
            this.Controls.Add(tbNama);
            this.Controls.Add(tbKategori);
            this.Controls.Add(tbWarna);
            this.Controls.Add(tbUkuran);
            this.Controls.Add(tbHarga);
            this.Controls.Add(tbStok);
            this.Controls.Add(cbStatus);
            this.Controls.Add(pbPakaian); // Tambahkan PictureBox
            // Tombol foto DIHAPUS dari penambahan kontrol
            // this.Controls.Add(btnUploadFoto);
            // this.Controls.Add(btnHapusFoto);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnReset);
            this.Controls.Add(btnBack);

            // Hubungkan event handlers
            btnSave.Click += BtnSimpan_Click;
            btnReset.Click += BtnReset_Click;
            btnBack.Click += BtnBack_Click;
            // Event handler tombol foto DIHAPUS
            // btnUploadFoto.Click += BtnUploadFoto_Click;
            // btnHapusFoto.Click += BtnHapusFoto_Click;
        }

        private void PanelEditPakaian_Load(object sender, EventArgs e)
        {
            if (_pakaianToEdit != null)
            {
                // Isi kontrol UI dengan data pakaian yang ada
                tbKode.Text = _pakaianToEdit.Kode;
                tbKode.ReadOnly = true;
                tbNama.Text = _pakaianToEdit.Nama;
                tbKategori.Text = _pakaianToEdit.Kategori;
                tbWarna.Text = _pakaianToEdit.Warna;
                tbUkuran.Text = _pakaianToEdit.Ukuran;
                tbHarga.Text = _pakaianToEdit.Harga.ToString();
                tbStok.Text = _pakaianToEdit.Stok.ToString();
                cbStatus.SelectedItem = _pakaianToEdit.Status.ToString();

                // Muat gambar statis dari resource
                LoadStaticImage(); // Metode baru untuk memuat gambar statis
            }
            else
            {
                // Mode Tambah Pakaian Baru
                tbKode.Clear();
                tbNama.Clear();
                tbKategori.Clear();
                tbWarna.Clear();
                tbUkuran.Clear();
                tbHarga.Clear();
                tbStok.Clear();
                cbStatus.SelectedIndex = -1;
                LoadStaticImage(); // Gambar default untuk tambah baru
            }
        }

        private void LoadStaticImage()
        {
            try
            {
                // Coba memuat resource 'pakaian'
                if (Properties.Resources.pakaian != null)
                {
                    pbPakaian.Image = Properties.Resources.pakaian;
                }
                // Jika 'pakaian' tidak ada, coba 'tshirt'
                else if (Properties.Resources.tshirt != null)
                {
                    pbPakaian.Image = Properties.Resources.tshirt;
                }
                else
                {
                    // Jika tidak ada resource gambar yang ditemukan
                    throw new InvalidOperationException("Tidak ada resource gambar pakaian ditemukan (pakaian.png atau tshirt.jpg).");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat gambar statis: {ex.Message}");
                // Fallback manual: buat bitmap polos
                Bitmap bmp = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.LightGray, 0, 0, 100, 100);
                    g.DrawString("No Image", new Font("Arial", 10), Brushes.Black, 10, 40);
                }
                pbPakaian.Image = bmp;
            }
        }

        private async void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            SetLoadingState(true);

            try
            {
                // Buat objek UpdatePakaianDto dari input form
                UpdatePakaianDto updateDto = new UpdatePakaianDto
                {
                    Nama = tbNama.Text.Trim(),
                    Kategori = tbKategori.Text.Trim(),
                    Warna = tbWarna.Text.Trim(),
                    Ukuran = tbUkuran.Text.Trim(),
                    Harga = decimal.Parse(tbHarga.Text),
                    Stok = int.Parse(tbStok.Text),
                };

                // Panggil API untuk update
                PakaianDtos updatedPakaian = await KatalogService.UpdatePakaianAsync(_pakaianToEdit.Kode, updateDto); // Menggunakan PakaianDtos

                MessageBox.Show($"Pakaian '{updatedPakaian.Nama}' berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnPakaianUpdated?.Invoke(updatedPakaian); // Beri tahu parent bahwa data telah diperbarui
                // Setelah update, kembali ke panel KelolaPakaian
                OnNavigateToPanel?.Invoke(new KelolaPakaian());
            }
            catch (FormatException)
            {
                MessageBox.Show("Pastikan Harga dan Stok adalah angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan perubahan: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset form ke data awal _pakaianToEdit
            PanelEditPakaian_Load(sender, e);
            MessageBox.Show("Form telah direset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke panel KelolaPakaian
            OnNavigateToPanel?.Invoke(new KelolaPakaian());
        }

        // Fungsionalitas Foto DIHAPUS (btnUploadFoto_Click, btnHapusFoto_Click)
        // private void BtnUploadFoto_Click(object sender, EventArgs e) { /* Dihapus */ }
        // private void BtnHapusFoto_Click(object sender, EventArgs e) { /* Dihapus */ }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbKode.Text) || string.IsNullOrWhiteSpace(tbNama.Text) ||
                string.IsNullOrWhiteSpace(tbKategori.Text) || string.IsNullOrWhiteSpace(tbWarna.Text) ||
                string.IsNullOrWhiteSpace(tbUkuran.Text) || string.IsNullOrWhiteSpace(tbHarga.Text) ||
                string.IsNullOrWhiteSpace(tbStok.Text) || cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(tbHarga.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus angka positif yang valid!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbHarga.Focus();
                return false;
            }

            if (!int.TryParse(tbStok.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Stok harus angka non-negatif yang valid!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbStok.Focus();
                return false;
            }

            return true;
        }

        private void SetLoadingState(bool loading)
        {
            tbKode.Enabled = !loading;
            tbNama.Enabled = !loading;
            tbKategori.Enabled = !loading;
            tbWarna.Enabled = !loading;
            tbUkuran.Enabled = !loading;
            tbHarga.Enabled = !loading;
            tbStok.Enabled = !loading;
            cbStatus.Enabled = !loading;

            btnSave.Enabled = !loading;
            btnReset.Enabled = !loading;
            btnBack.Enabled = !loading;
            // Tombol foto DIHAPUS dari pengaktifan/penonaktifan
            // btnUploadFoto.Enabled = !loading;
            // btnHapusFoto.Enabled = !loading;

            this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}

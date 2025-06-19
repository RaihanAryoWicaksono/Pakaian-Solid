// PakaianForm/Views/Admin/Panel/editItemPanel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel; // Penting untuk UserControl
using System.Data;
using System.Drawing;
using System.Linq; // Untuk Enum.GetNames
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Penting untuk UserControl, Label, PictureBox, Button
using PakaianForm.Models; // Untuk PakaianDtos

namespace PakaianForm.Views.Admin.Panel
{
    public partial class editItemPanel : UserControl
    {
        private PakaianDto _currentPakaian; // Variabel untuk menyimpan data pakaian di panel ini

        // Event yang akan dipicu ketika tombol Edit diklik
        public event EventHandler<PakaianDto> OnEditClicked;
        // Event yang akan dipicu ketika tombol Hapus diklik
        public event EventHandler<PakaianDto> OnDeleteClicked;

        // Kontrol-kontrol yang dideklarasikan di Designer.cs. Akses langsung melalui namanya.
        // Pastikan nama-nama ini cocok dengan properti 'Name' di Designer.cs Anda.
        // Contoh: private System.Windows.Forms.Label labelNamaPakaian; (ini ada di Designer.cs)
        // private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1; (ini ada di Designer.cs)
        // private Guna.UI2.WinForms.Guna2GradientButton btnEditPakaian; (ini ada di Designer.cs)
        // private Guna.UI2.WinForms.Guna2GradientButton btnHapusPakaian; (ini ada di Designer.cs)


        public editItemPanel()
        {
            InitializeComponent(); // <-- Ini menginisialisasi SEMUA kontrol dari Designer.cs

            // Hubungkan event handlers ke tombol-tombol yang sudah ada dari desainer
            // Pastikan btnEditPakaian dan btnHapusPakaian dideklarasikan di Designer.cs
            if (btnEditPakaian != null) btnEditPakaian.Click += BtnEditPakaian_Click;
            if (btnHapusPakaian != null) btnHapusPakaian.Click += BtnHapusPakaian_Click;

            SetEmptyState(); // Tampilkan state kosong saat panel dibuat
        }

        // Properti 'Pakaian' yang akan digunakan oleh KelolaPakaian untuk mengatur data
        public PakaianDto Pakaian // <--- INI ADALAH DEFINISI PROPERTI 'Pakaian'
        {
            get { return _currentPakaian; }
            set
            {
                _currentPakaian = value;
                UpdateDisplay(); // Panggil metode untuk memperbarui tampilan setiap kali data diatur
            }
        }

        private void UpdateDisplay()
        {
            if (_currentPakaian == null)
            {
                SetEmptyState();
                return;
            }

            try
            {
                // Mengisi Label dengan data pakaian
                // Gunakan null-conditional operator (?) untuk keamanan
                if (labelNamaPakaian != null) labelNamaPakaian.Text = _currentPakaian.Nama ?? "Nama Pakaian";
                if (labelHarga != null) labelHarga.Text = $"Rp{_currentPakaian.Harga:N0}";
                if (labelStatusPakaian != null) labelStatusPakaian.Text = $"Status: {_currentPakaian.Status}";

                // Jika ada labelBanyakPesanan di Designer.cs (untuk stok)
                // if (labelBanyakPesanan != null) labelBanyakPesanan.Text = $"Stok: {_currentPakaian.Stok}";

                LoadPakaianImage();

                // Mengatur tombol Edit/Hapus berdasarkan data
                if (btnEditPakaian != null) btnEditPakaian.Tag = _currentPakaian.Kode;
                if (btnHapusPakaian != null) btnHapusPakaian.Tag = _currentPakaian.Kode;

                if (btnEditPakaian != null) btnEditPakaian.Enabled = true;
                if (btnHapusPakaian != null) btnHapusPakaian.Enabled = true;
            }
            catch (Exception ex)
            {
                // Tampilkan error jika ada masalah saat memperbarui tampilan
                if (labelNamaPakaian != null) labelNamaPakaian.Text = "Error!";
                if (labelHarga != null) labelHarga.Text = "-";
                if (labelStatusPakaian != null) labelStatusPakaian.Text = ex.Message;
                if (btnEditPakaian != null) btnEditPakaian.Enabled = false;
                if (btnHapusPakaian != null) btnHapusPakaian.Enabled = false;
            }
        }

        private void LoadPakaianImage()
        {
            if (guna2PictureBox1 == null) return; // Pastikan guna2PictureBox1 diinisialisasi

            try
            {
                if (Properties.Resources.tshirt != null) // Pastikan resource 'tshirt' ada
                {
                    guna2PictureBox1.Image = Properties.Resources.tshirt;
                }
                else
                {
                    throw new InvalidOperationException("Resource 'tshirt' tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat gambar untuk panel item: {ex.Message}");
                Bitmap bmp = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.LightGray, 0, 0, 100, 100);
                    g.DrawString("No Image", new Font("Arial", 10), Brushes.Black, 10, 40);
                }
                guna2PictureBox1.Image = bmp;
            }
        }

        private void SetEmptyState()
        {
            if (labelNamaPakaian != null) labelNamaPakaian.Text = "No Data";
            if (labelHarga != null) labelHarga.Text = "-";
            if (labelStatusPakaian != null) labelStatusPakaian.Text = "-";
            // if (labelBanyakPesanan != null) labelBanyakPesanan.Text = "-"; // Jika ada label stok
            if (guna2PictureBox1 != null) { guna2PictureBox1.Image = null; guna2PictureBox1.BackColor = Color.LightGray; }
            if (btnEditPakaian != null) btnEditPakaian.Enabled = false;
            if (btnHapusPakaian != null) btnHapusPakaian.Enabled = false;
        }

        // Event handler untuk tombol Edit (yang ada di desainer editItemPanel)
        private void BtnEditPakaian_Click(object sender, EventArgs e) // <--- INI ADALAH DEFINISI YANG DICARI
        {
            // Memicu event OnEditClicked untuk memberi tahu parent (KelolaPakaian)
            if (_currentPakaian != null)
            {
                OnEditClicked?.Invoke(this, _currentPakaian);
            }
        }

        // Event handler untuk tombol Hapus (yang ada di desainer editItemPanel)
        private void BtnHapusPakaian_Click(object sender, EventArgs e) // <--- INI ADALAH DEFINISI YANG DICARI
        {
            // Memicu event OnDeleteClicked untuk memberi tahu parent (KelolaPakaian)
            if (_currentPakaian != null)
            {
                OnDeleteClicked?.Invoke(this, _currentPakaian);
            }
        }

        // Contoh event handler lama (bisa dihapus jika tidak lagi direferensikan di Designer.cs)
        private void button1_Click(object sender, EventArgs e) { }
    }
}

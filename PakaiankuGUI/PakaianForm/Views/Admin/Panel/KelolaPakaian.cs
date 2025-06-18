// PakaianForm/Views/Admin/Panel/KelolaPakaian.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk PakaianDtos
using PakaianForm.Services; // Untuk ApiClient, KatalogService
using PakaianLib; // Penting untuk StatusPakaian, AksiPakaian
using Guna.UI2.WinForms; // Tambahkan ini untuk akses komponen Guna.UI2

namespace PakaianForm.Views.Admin.Panel
{
    public partial class KelolaPakaian : UserControl
    {
        public event Action<UserControl> OnNavigateToPanel;

        // Deklarasi Variabel
        private List<PakaianDtos> _allPakaian = new List<PakaianDtos>();
        private System.Windows.Forms.Timer _searchTimer;

        public KelolaPakaian()
        {
            InitializeComponent();

            // Konfigurasi FlowLayoutPanel
            flowLayoutPanelBarang.AutoScroll = true;
            flowLayoutPanelBarang.WrapContents = true;
            flowLayoutPanelBarang.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelBarang.AutoScrollMargin = new Size(0, 50);

            // Inisialisasi Timer Pencarian
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 500; // Delay 0.5 detik
            _searchTimer.Tick += SearchTimer_Tick; // Hubungkan event Tick

            // Hubungkan event handlers untuk pencarian
            // Pastikan tbSearchPakaian dan button1 dideklarasikan di Designer.cs
            if (tbSearchPakaian != null) tbSearchPakaian.TextChanged += tbSearchPakaian_TextChanged;
            if (button1 != null) button1.Click += button1_Click;

            this.Load += KelolaPakaian_Load;
        }

        private async void KelolaPakaian_Load(object sender, EventArgs e)
        {
            await LoadPakaianData(); // Muat semua data saat panel dimuat
        }

        public async Task LoadPakaianData()
        {
            flowLayoutPanelBarang.SuspendLayout();
            try
            {
                flowLayoutPanelBarang.Controls.Clear();

                _allPakaian = await KatalogService.GetAllPakaianAsync();

                DisplayPakaian(_allPakaian); // Tampilkan semua pakaian
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memuat data pakaian: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowLayoutPanelBarang.ResumeLayout(true);
                flowLayoutPanelBarang.Invalidate();
                flowLayoutPanelBarang.Update();
            }
        }

        private void DisplayPakaian(List<PakaianDtos> pakaianList)
        {
            flowLayoutPanelBarang.Controls.Clear();

            if (pakaianList == null || !pakaianList.Any())
            {
                Label noDataLabel = new Label();
                noDataLabel.Text = "Tidak ada pakaian ditemukan.";
                noDataLabel.AutoSize = true;
                noDataLabel.Padding = new Padding(10);
                flowLayoutPanelBarang.Controls.Add(noDataLabel);
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                System.Windows.Forms.Panel itemPanel = CreatePakaianItemPanel(pakaian);

                flowLayoutPanelBarang.Controls.Add(itemPanel);
            }
        }

        // Metode ini membuat panel item pakaian secara dinamis menggunakan komponen Guna.UI2
        private System.Windows.Forms.Panel CreatePakaianItemPanel(PakaianDtos pakaian)
        {
            System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new Size(265, 369); // Ukuran editItemPanel
            panel.Margin = new Padding(10);
            panel.Tag = pakaian;

            // Gambar
            Guna.UI2.WinForms.Guna2PictureBox pictureBox = new Guna.UI2.WinForms.Guna2PictureBox();
            try
            {
                if (Properties.Resources.tshirt != null) { pictureBox.Image = Properties.Resources.tshirt; }
                else { throw new InvalidOperationException("Resource 'tshirt' tidak ditemukan."); }
            }
            catch (Exception)
            {
                pictureBox.Image = null;
                pictureBox.BackColor = Color.LightGray;
            }
            pictureBox.ImageRotate = 0F;
            pictureBox.Location = new Point(35, 21);
            pictureBox.Name = "guna2PictureBox1";
            pictureBox.Size = new Size(194, 171);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            panel.Controls.Add(pictureBox);

            // Nama Pakaian
            Label lblNama = new Label();
            lblNama.Text = pakaian.Nama;
            lblNama.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblNama.AutoSize = false;
            lblNama.TextAlign = ContentAlignment.MiddleCenter;
            lblNama.Location = new Point(10, pictureBox.Bottom + 5);
            lblNama.Name = "labelNamaPakaian";
            lblNama.Size = new Size(panel.ClientSize.Width - 20, 28);
            lblNama.TabIndex = 6;
            panel.Controls.Add(lblNama);

            // Status Pakaian (Kecil, di atas Harga)
            Label lblStatus = new Label();
            lblStatus.Text = $"Status: {pakaian.Status}";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.AutoSize = false;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(lblNama.Left, lblNama.Bottom + 5);
            lblStatus.Name = "labelStatusPakaian";
            lblStatus.Size = new Size(panel.ClientSize.Width - 20, 20);
            lblStatus.TabIndex = 9;
            panel.Controls.Add(lblStatus);

            // Harga Pakaian (Di bawah Status)
            Label lblHarga = new Label();
            lblHarga.Text = $"Rp{pakaian.Harga:N0}";
            lblHarga.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblHarga.AutoSize = false;
            lblHarga.TextAlign = ContentAlignment.MiddleCenter;
            lblHarga.Location = new Point(lblNama.Left, lblStatus.Bottom + 2);
            lblHarga.Name = "labelHarga";
            lblHarga.Size = new Size(panel.ClientSize.Width - 20, 28);
            lblHarga.TabIndex = 7;
            panel.Controls.Add(lblHarga);

            // Tombol Edit
            Guna.UI2.WinForms.Guna2GradientButton btnEdit = new Guna.UI2.WinForms.Guna2GradientButton();
            btnEdit.Text = "Edit";
            btnEdit.Tag = pakaian.Kode;
            btnEdit.Animated = true;
            btnEdit.AutoRoundedCorners = true;
            btnEdit.BorderRadius = 21;
            btnEdit.FillColor = System.Drawing.Color.Lime;
            btnEdit.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(14, 301);
            btnEdit.Name = "btnEditPakaian";
            btnEdit.Size = new Size(112, 45);
            btnEdit.TabIndex = 12;
            btnEdit.Click += (s, e) => ItemPanel_OnEditClicked(s, pakaian);
            panel.Controls.Add(btnEdit);

            // Tombol Hapus
            Guna.UI2.WinForms.Guna2GradientButton btnHapus = new Guna.UI2.WinForms.Guna2GradientButton();
            btnHapus.Text = "Hapus";
            btnHapus.Tag = pakaian.Kode;
            btnHapus.Animated = true;
            btnHapus.AutoRoundedCorners = true;
            btnHapus.BorderRadius = 21;
            btnHapus.FillColor = System.Drawing.Color.Red;
            btnHapus.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            btnHapus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnHapus.ForeColor = Color.White;
            btnHapus.Location = new Point(132, 301);
            btnHapus.Name = "btnHapusPakaian";
            btnHapus.Size = new Size(112, 45);
            btnHapus.TabIndex = 13;
            btnHapus.Click += (s, e) => ItemPanel_OnDeleteClicked(s, pakaian);
            panel.Controls.Add(btnHapus);

            return panel;
        }

        private void ItemPanel_OnEditClicked(object sender, PakaianDtos pakaianToEdit)
        {
            panelEditPakaian editPanel = new panelEditPakaian(pakaianToEdit);
            editPanel.OnPakaianUpdated += async (updatedPakaian) => {
                MessageBox.Show($"Pakaian '{updatedPakaian.Nama}' berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPakaianData();
            };
            OnNavigateToPanel?.Invoke(editPanel);
        }

        private async void ItemPanel_OnDeleteClicked(object sender, PakaianDtos pakaianToDelete)
        {
            DialogResult confirmResult = MessageBox.Show($"Apakah Anda yakin ingin menghapus pakaian '{pakaianToDelete.Nama}' (Kode: {pakaianToDelete.Kode})?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await KatalogService.DeletePakaianAsync(pakaianToDelete.Kode);
                    MessageBox.Show($"Pakaian '{pakaianToDelete.Nama}' berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPakaianData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus pakaian: {ex.Message}", "Kesalahan Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Fungsionalitas Pencarian ---
        // Metode ini harus ada di sini karena event handler terhubung ke mereka di konstruktor
        private void tbSearchPakaian_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            await PerformSearch();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }

        private async Task PerformSearch()
        {
            if (tbSearchPakaian == null) return;

            flowLayoutPanelBarang.SuspendLayout();
            try
            {
                flowLayoutPanelBarang.Controls.Clear();

                string searchTerm = tbSearchPakaian.Text.Trim();
                List<PakaianDtos> searchResults;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchResults = new List<PakaianDtos>(_allPakaian);
                }
                else
                {
                    searchResults = await KatalogService.SearchPakaianAsync(searchTerm);
                }

                DisplayPakaian(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mencari pakaian: {ex.Message}", "Kesalahan Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowLayoutPanelBarang.ResumeLayout(true);
                flowLayoutPanelBarang.Invalidate();
                flowLayoutPanelBarang.Update();
            }
        }
        // --- Akhir Fungsionalitas Pencarian ---


        // Event handler yang direferensikan oleh desainer (jika ada, biarkan kosong)
        private void label1_Click(object sender, EventArgs e) { }
        private void panelItem1_Load(object sender, EventArgs e) { }
        private void editItemPanel_Load(object sender, EventArgs e) { }
        private void editItemPanel1_Load(object sender, EventArgs e) { }
        private void editItemPanel2_Load(object sender, EventArgs e) { }
        private void editItemPanel3_Load(object sender, EventArgs e) { }
        private void editItemPanel4_Load(object sender, EventArgs e) { }
        private void editItemPanel5_Load(object sender, EventArgs e) { }
        private void editItemPanel6_Load(object sender, EventArgs e) { }
    }
}

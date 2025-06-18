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

namespace PakaianForm.Views.Admin.Panel
{
    public partial class KelolaPakaian : UserControl
    {
        public event Action<UserControl> OnNavigateToPanel;

        public KelolaPakaian()
        {
            InitializeComponent();
            flowLayoutPanelBarang.AutoScroll = true;
            flowLayoutPanelBarang.WrapContents = true;

            this.Load += KelolaPakaian_Load;
        }

        private async void KelolaPakaian_Load(object sender, EventArgs e)
        {
            await LoadPakaianData();
        }

        public async Task LoadPakaianData()
        {
            try
            {
                flowLayoutPanelBarang.Controls.Clear();
                // Menggunakan List<PakaianDtos>
                List<PakaianDtos> pakaianList = await KatalogService.GetAllPakaianAsync(); // <--- Menggunakan PakaianDtos

                if (pakaianList != null && pakaianList.Any())
                {
                    foreach (var pakaian in pakaianList)
                    {
                        System.Windows.Forms.Panel itemPanel = CreatePakaianItemPanel(pakaian);
                        flowLayoutPanelBarang.Controls.Add(itemPanel);
                    }
                }
                else
                {
                    Label noDataLabel = new Label();
                    noDataLabel.Text = "Tidak ada pakaian ditemukan.";
                    noDataLabel.AutoSize = true;
                    noDataLabel.Padding = new Padding(10);
                    flowLayoutPanelBarang.Controls.Add(noDataLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memuat data pakaian: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Menggunakan PakaianDtos
        private System.Windows.Forms.Panel CreatePakaianItemPanel(PakaianDtos pakaian) // <--- Menggunakan PakaianDtos
        {
            System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new Size(200, 280);
            panel.Margin = new Padding(10);
            panel.Tag = pakaian;

            PictureBox pictureBox = new PictureBox();
            Image loadedImage = null;
            try
            {
                // Coba memuat resource 'pakaian'
                if (Properties.Resources.pakaian != null)
                {
                    loadedImage = Properties.Resources.pakaian;
                }
                // Jika 'pakaian' tidak ada, coba 'tshirt'
                else if (Properties.Resources.tshirt != null)
                {
                    loadedImage = Properties.Resources.tshirt;
                }
                else
                {
                    // Jika tidak ada resource gambar yang ditemukan
                    throw new InvalidOperationException("Tidak ada resource gambar pakaian ditemukan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat gambar: {ex.Message}");
                // Fallback manual: buat bitmap polos
                Bitmap bmp = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.LightGray, 0, 0, 100, 100);
                    g.DrawString("No Image", new Font("Arial", 10), Brushes.Black, 10, 40);
                }
                loadedImage = bmp;
            }

            pictureBox.Image = loadedImage;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(180, 120);
            pictureBox.Location = new Point(10, 10);
            panel.Controls.Add(pictureBox);

            Label lblNama = new Label();
            lblNama.Text = pakaian.Nama;
            lblNama.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNama.AutoSize = false;
            lblNama.TextAlign = ContentAlignment.MiddleCenter;
            lblNama.Size = new Size(180, 20);
            lblNama.Location = new Point(10, pictureBox.Bottom + 5);
            panel.Controls.Add(lblNama);

            Label lblHarga = new Label();
            lblHarga.Text = $"Rp{pakaian.Harga:N0}";
            lblHarga.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblHarga.AutoSize = false;
            lblHarga.TextAlign = ContentAlignment.MiddleCenter;
            lblHarga.Size = new Size(180, 20);
            lblHarga.Location = new Point(10, lblNama.Bottom + 5);
            panel.Controls.Add(lblHarga);

            Label lblStatus = new Label();
            lblStatus.Text = $"Status: {pakaian.Status}";
            lblStatus.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblStatus.AutoSize = false;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Size = new Size(180, 15);
            lblStatus.Location = new Point(10, lblHarga.Bottom + 5);
            panel.Controls.Add(lblStatus);

            Label lblStok = new Label();
            lblStok.Text = $"Stok: {pakaian.Stok}";
            lblStok.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblStok.AutoSize = false;
            lblStok.TextAlign = ContentAlignment.MiddleCenter;
            lblStok.Size = new Size(180, 15);
            lblStok.Location = new Point(10, lblStatus.Bottom + 2);
            panel.Controls.Add(lblStok);


            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Tag = pakaian.Kode;
            btnEdit.Size = new Size(80, 30);
            btnEdit.Location = new Point(10, lblStok.Bottom + 10);
            btnEdit.BackColor = Color.FromArgb(0, 192, 0);
            btnEdit.ForeColor = Color.White;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += EditButton_Click;
            panel.Controls.Add(btnEdit);

            Button btnHapus = new Button();
            btnHapus.Text = "Hapus";
            btnHapus.Tag = pakaian.Kode;
            btnHapus.Size = new Size(80, 30);
            btnHapus.Location = new Point(btnEdit.Right + 20, lblStok.Bottom + 10);
            btnHapus.BackColor = Color.FromArgb(192, 0, 0);
            btnHapus.ForeColor = Color.White;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderSize = 0;
            btnHapus.Click += DeleteButton_Click;
            panel.Controls.Add(btnHapus);

            return panel;
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            Button btnEdit = sender as Button;
            string kodePakaian = btnEdit.Tag.ToString();

            try
            {
                PakaianDtos pakaianToEdit = await KatalogService.GetPakaianByKodeAsync(kodePakaian); // <--- Menggunakan PakaianDtos

                if (pakaianToEdit != null)
                {
                    panelEditPakaian editPanel = new panelEditPakaian(pakaianToEdit); // <--- Menggunakan PakaianDtos

                    editPanel.OnPakaianUpdated += async (updatedPakaian) => { // <--- updatedPakaian akan PakaianDtos
                        MessageBox.Show($"Pakaian '{updatedPakaian.Nama}' berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadPakaianData();
                    };

                    OnNavigateToPanel?.Invoke(editPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mencoba mengedit pakaian: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btnHapus = sender as Button;
            string kodePakaian = btnHapus.Tag.ToString();

            DialogResult confirmResult = MessageBox.Show($"Apakah Anda yakin ingin menghapus pakaian dengan kode {kodePakaian}?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await KatalogService.DeletePakaianAsync(kodePakaian);
                    MessageBox.Show($"Pakaian dengan kode {kodePakaian} berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPakaianData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus pakaian: {ex.Message}", "Kesalahan Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Event handler yang direferensikan oleh desainer
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

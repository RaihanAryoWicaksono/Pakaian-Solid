// PakaianForm/Views/Customer/Panel/lihatSemuaPakaian.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk PakaianDtos, AddToCartDto, dll
using PakaianForm.Services; // Untuk KatalogService, KeranjangService
using PakaianLib; // Untuk StatusPakaian, AksiPakaian

namespace PakaianForm.Views.Customer.Panel
{
    public partial class lihatSemuaPakaian : UserControl
    {
        private List<PakaianDtos> allPakaian = new List<PakaianDtos>(); // <--- Menggunakan PakaianDtos
        private List<PakaianDtos> filteredPakaian = new List<PakaianDtos>(); // <--- Menggunakan PakaianDtos
        private System.Windows.Forms.Timer searchTimer;

        public lihatSemuaPakaian()
        {
            InitializeComponent();
            InitializeComponents();
            this.Load += LihatSemuaPakaian_Load;
        }

        private async void LihatSemuaPakaian_Load(object sender, EventArgs e)
        {
            await LoadPakaianData();
        }

        private void InitializeComponents()
        {
            flowLayoutPanelListPakaian.AutoScroll = true;
            flowLayoutPanelListPakaian.WrapContents = true;
            flowLayoutPanelListPakaian.FlowDirection = FlowDirection.LeftToRight;

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 500;
            searchTimer.Tick += SearchTimer_Tick;

            tbSearchPakaian.PlaceholderText = "Cari pakaian...";

            ClearStaticPanels();
        }

        private void ClearStaticPanels()
        {
            var panelsToRemove = flowLayoutPanelListPakaian.Controls
                .OfType<listPakaianPanel>() // <-- Pastikan listPakaianPanel ada
                .ToList();

            foreach (var panel in panelsToRemove)
            {
                flowLayoutPanelListPakaian.Controls.Remove(panel);
                panel.Dispose();
            }
        }

        private async Task LoadPakaianData()
        {
            try
            {
                SetLoadingState(true);
                flowLayoutPanelListPakaian.Controls.Clear();

                allPakaian = await KatalogService.GetAllPakaianAsync(); // <--- Menggunakan PakaianDtos
                filteredPakaian = new List<PakaianDtos>(allPakaian); // <--- Menggunakan PakaianDtos

                DisplayPakaian(filteredPakaian);

                // Pastikan labelListPakaian ada di designer
                // labelListPakaian.Text = $"List Pakaian ({filteredPakaian.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pakaian data:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowEmptyState("Gagal memuat data pakaian");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void DisplayPakaian(List<PakaianDtos> pakaianList) // <--- Menggunakan PakaianDtos
        {
            flowLayoutPanelListPakaian.Controls.Clear();

            if (pakaianList == null || pakaianList.Count == 0)
            {
                ShowEmptyState("Tidak ada pakaian yang ditemukan");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                var pakaianPanel = new listPakaianPanel(); // <-- Pastikan listPakaianPanel ada
                pakaianPanel.Pakaian = pakaian; // <-- Pastikan listPakaianPanel memiliki properti Pakaian (tipe PakaianDtos)

                pakaianPanel.OnAddToCartClicked += PakaianPanel_OnAddToCartClicked;
                pakaianPanel.OnDataChanged += PakaianPanel_OnDataChanged;

                flowLayoutPanelListPakaian.Controls.Add(pakaianPanel);
            }
        }

        private void PakaianPanel_OnAddToCartClicked(object sender, PakaianDtos pakaian) // <--- Menggunakan PakaianDtos
        {
            MessageBox.Show($"Item '{pakaian.Nama}' ditambahkan ke keranjang (simulasi).", "Info Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Untuk benar-benar menambahkannya ke keranjang via API:
            /*
            try {
                var addToCartRequest = new AddToCartDto { KodePakaian = pakaian.Kode };
                // Perlu ada using PakaianForm.Services.KeranjangService;
                // await KeranjangService.AddToKeranjangAsync(addToCartRequest);
                MessageBox.Show($"'{pakaian.Nama}' berhasil ditambahkan ke keranjang!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(); // Refresh untuk update stok
            } catch (Exception ex) {
                MessageBox.Show($"Gagal menambahkan ke keranjang:\n{ex.Message}", "Error Keranjang", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

        private void PakaianPanel_OnDataChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        // Metode ini mungkin tidak digunakan jika Anda menggunakan listPakaianPanel
        // Anda bisa menghapusnya jika memang tidak digunakan
        private Control CreatePakaianCard(PakaianDtos pakaian) // <--- Menggunakan PakaianDtos
        {
            var panel = new System.Windows.Forms.Panel();
            panel.Size = new Size(273, 369);
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Margin = new Padding(4, 5, 4, 5);

            var pictureBox = new PictureBox();
            pictureBox.Size = new Size(240, 200);
            pictureBox.Location = new Point(15, 15);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            try
            {
                pictureBox.Image = Properties.Resources.tshirt; // Pastikan Anda memiliki resource ini
            }
            catch
            {
                pictureBox.BackColor = Color.LightGray;
                var noImageLabel = new Label();
                noImageLabel.Text = "No Image";
                noImageLabel.Size = pictureBox.Size;
                noImageLabel.TextAlign = ContentAlignment.MiddleCenter;
                noImageLabel.BackColor = Color.LightGray;
                pictureBox.Controls.Add(noImageLabel);
            }

            var stokLabel = new Label();
            stokLabel.Text = pakaian.Stok.ToString();
            stokLabel.Size = new Size(40, 25);
            stokLabel.Location = new Point(215, 25);
            stokLabel.BackColor = pakaian.Stok > 0 ? Color.LimeGreen : Color.Red;
            stokLabel.ForeColor = Color.White;
            stokLabel.TextAlign = ContentAlignment.MiddleCenter;
            stokLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            var namaLabel = new Label();
            namaLabel.Text = pakaian.Nama;
            namaLabel.Size = new Size(240, 30);
            namaLabel.Location = new Point(15, 225);
            namaLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            namaLabel.ForeColor = Color.Black;
            namaLabel.TextAlign = ContentAlignment.MiddleLeft;

            var infoPanel = new System.Windows.Forms.Panel();
            infoPanel.Size = new Size(240, 25);
            infoPanel.Location = new Point(15, 260);
            infoPanel.BackColor = Color.Transparent;

            var statusLabel = new Label();
            statusLabel.Text = pakaian.Status.ToString();
            statusLabel.Size = new Size(100, 25);
            statusLabel.Location = new Point(0, 0);
            statusLabel.Font = new Font("Segoe UI", 9);
            statusLabel.ForeColor = Color.Gray;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            var hargaLabel = new Label();
            hargaLabel.Text = pakaian.Harga.ToString("C");
            hargaLabel.Size = new Size(140, 25);
            hargaLabel.Location = new Point(100, 0);
            hargaLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            hargaLabel.ForeColor = Color.DarkBlue;
            hargaLabel.TextAlign = ContentAlignment.MiddleRight;

            infoPanel.Controls.Add(statusLabel);
            infoPanel.Controls.Add(hargaLabel);

            var addToCartBtn = new Button();
            addToCartBtn.Text = "Tambah ke Keranjang";
            addToCartBtn.Size = new Size(240, 45);
            addToCartBtn.Location = new Point(15, 300);
            addToCartBtn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            addToCartBtn.ForeColor = Color.White;
            addToCartBtn.FlatStyle = FlatStyle.Flat;
            addToCartBtn.FlatAppearance.BorderSize = 0;

            if (pakaian.Stok <= 0 || pakaian.Status != PakaianLib.StatusPakaian.Tersedia)
            {
                addToCartBtn.Enabled = false;
                addToCartBtn.Text = pakaian.Stok <= 0 ? "Stok Habis" : "Tidak Tersedia";
                addToCartBtn.BackColor = Color.Gray;
            }
            else
            {
                addToCartBtn.BackColor = Color.LimeGreen;
            }

            addToCartBtn.Click += async (sender, e) => await AddToCart_Click(pakaian, addToCartBtn);

            panel.Controls.Add(pictureBox);
            panel.Controls.Add(stokLabel);
            panel.Controls.Add(namaLabel);
            panel.Controls.Add(infoPanel);
            panel.Controls.Add(addToCartBtn);

            return panel;
        }

        private async Task AddToCart_Click(PakaianDtos pakaian, Button button) // <--- Menggunakan PakaianDtos
        {
            try
            {
                button.Enabled = false;
                button.Text = "Adding...";

                var addToCartRequest = new AddToCartDto { KodePakaian = pakaian.Kode };
                await KeranjangService.AddToKeranjangAsync(addToCartRequest);

                MessageBox.Show($"'{pakaian.Nama}' berhasil ditambahkan ke keranjang!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan ke keranjang:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button.Enabled = true;
                button.Text = "Tambah ke Keranjang";
            }
        }

        private void ShowEmptyState(string message)
        {
            var emptyLabel = new Label();
            emptyLabel.Text = message;
            emptyLabel.Size = new Size(400, 50);
            emptyLabel.Location = new Point(200, 200);
            emptyLabel.Font = new Font("Segoe UI", 14);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;

            flowLayoutPanelListPakaian.Controls.Clear();
            flowLayoutPanelListPakaian.Controls.Add(emptyLabel);
        }

        private void SetLoadingState(bool loading)
        {
            if (loading)
            {
                flowLayoutPanelListPakaian.Controls.Clear();
                var loadingLabel = new Label();
                loadingLabel.Text = "Loading...";
                loadingLabel.Size = new Size(200, 50);
                loadingLabel.Location = new Point(300, 200);
                loadingLabel.Font = new Font("Segoe UI", 14);
                loadingLabel.ForeColor = Color.Blue;
                loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
                flowLayoutPanelListPakaian.Controls.Add(loadingLabel);
            }

            tbSearchPakaian.Enabled = !loading;
            button1.Enabled = !loading;
        }

        private void RefreshData()
        {
            LoadPakaianData();
        }

        // Search functionality
        private void tbSearchPakaian_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            await PerformSearch();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }

        private async Task PerformSearch()
        {
            try
            {
                SetLoadingState(true);

                string searchTerm = tbSearchPakaian.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    filteredPakaian = new List<PakaianDtos>(allPakaian); // <--- Menggunakan PakaianDtos
                }
                else
                {
                    filteredPakaian = await KatalogService.SearchPakaianAsync(searchTerm); // <--- Menggunakan PakaianDtos
                }

                DisplayPakaian(filteredPakaian);
                // Pastikan labelListPakaian ada di designer
                // labelListPakaian.Text = $"List Pakaian ({filteredPakaian.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching pakaian:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        // Designer generated event handlers
        private void flowLayoutPanelListPakaian_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void labelJudulListPakaian_Click(object sender, EventArgs e) { }
    }
}

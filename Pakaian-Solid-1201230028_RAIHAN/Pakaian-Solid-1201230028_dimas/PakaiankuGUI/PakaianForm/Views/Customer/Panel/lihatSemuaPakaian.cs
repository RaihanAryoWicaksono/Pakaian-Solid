using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models;
using PakaianForm.Services;

namespace PakaianForm.Views.Customer.Panel
{
    public partial class lihatSemuaPakaian : UserControl
    {
        private List<PakaianDto> allPakaian = new List<PakaianDto>();
        private List<PakaianDto> filteredPakaian = new List<PakaianDto>();
        private System.Windows.Forms.Timer searchTimer;

        public lihatSemuaPakaian()
        {
            InitializeComponent();
            InitializeComponents();
            LoadPakaianData();
        }

        private void InitializeComponents()
        {
            // Configure FlowLayoutPanel
            flowLayoutPanelListPakaian.AutoScroll = true;
            flowLayoutPanelListPakaian.WrapContents = true;
            flowLayoutPanelListPakaian.FlowDirection = FlowDirection.LeftToRight;

            // Setup search timer for real-time search
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 500; // 500ms delay
            searchTimer.Tick += SearchTimer_Tick;

            // Set placeholder text
            tbSearchPakaian.PlaceholderText = "Cari pakaian...";

            // Remove static panels
            ClearStaticPanels();
        }

        private void ClearStaticPanels()
        {
            // Remove all static listPakaianPanel controls
            var panelsToRemove = flowLayoutPanelListPakaian.Controls
                .OfType<listPakaianPanel>()
                .ToList();

            foreach (var panel in panelsToRemove)
            {
                flowLayoutPanelListPakaian.Controls.Remove(panel);
                panel.Dispose();
            }
        }

        private async void LoadPakaianData()
        {
            try
            {
                // Show loading state
                SetLoadingState(true);

                // Clear existing data
                flowLayoutPanelListPakaian.Controls.Clear();

                // Load data from API
                allPakaian = await KatalogService.GetAllPakaianAsync();
                filteredPakaian = new List<PakaianDto>(allPakaian);

                // Display data
                DisplayPakaian(filteredPakaian);

                // Update info
                labelListPakaian.Text = $"List Pakaian ({filteredPakaian.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pakaian data:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Show empty state
                ShowEmptyState("Gagal memuat data pakaian");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void DisplayPakaian(List<PakaianDto> pakaianList)
        {
            flowLayoutPanelListPakaian.Controls.Clear();

            if (pakaianList == null || pakaianList.Count == 0)
            {
                ShowEmptyState("Tidak ada pakaian yang ditemukan");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                var pakaianPanel = new listPakaianPanel();
                pakaianPanel.Pakaian = pakaian;

                // Subscribe to events
                pakaianPanel.OnAddToCartClicked += PakaianPanel_OnAddToCartClicked;
                pakaianPanel.OnDataChanged += PakaianPanel_OnDataChanged;

                flowLayoutPanelListPakaian.Controls.Add(pakaianPanel);
            }
        }

        private void PakaianPanel_OnAddToCartClicked(object sender, PakaianDto pakaian)
        {
            // Handle add to cart event from individual panels
            // Could show notification, update cart counter, etc.
        }

        private void PakaianPanel_OnDataChanged(object sender, EventArgs e)
        {
            // Handle data change event (e.g., stock updated after add to cart)
            // Could refresh parent display, update counters, etc.
        }

        private Control CreatePakaianCard(PakaianDto pakaian)
        {
            // Create main panel - using System.Windows.Forms.Panel explicitly
            var panel = new System.Windows.Forms.Panel();
            panel.Size = new Size(273, 369);
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Margin = new Padding(4, 5, 4, 5);

            // Create picture box for pakaian image
            var pictureBox = new PictureBox();
            pictureBox.Size = new Size(240, 200);
            pictureBox.Location = new Point(15, 15);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            // Load default image
            try
            {
                pictureBox.Image = Properties.Resources.tshirt; // Make sure you have this resource
            }
            catch
            {
                pictureBox.BackColor = Color.LightGray;
                // Create simple text if no image
                var noImageLabel = new Label();
                noImageLabel.Text = "No Image";
                noImageLabel.Size = pictureBox.Size;
                noImageLabel.TextAlign = ContentAlignment.MiddleCenter;
                noImageLabel.BackColor = Color.LightGray;
                pictureBox.Controls.Add(noImageLabel);
            }

            // Create stok label (top-right corner)
            var stokLabel = new Label();
            stokLabel.Text = pakaian.Stok.ToString();
            stokLabel.Size = new Size(40, 25);
            stokLabel.Location = new Point(215, 25);
            stokLabel.BackColor = pakaian.Stok > 0 ? Color.LimeGreen : Color.Red;
            stokLabel.ForeColor = Color.White;
            stokLabel.TextAlign = ContentAlignment.MiddleCenter;
            stokLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Create nama label
            var namaLabel = new Label();
            namaLabel.Text = pakaian.Nama;
            namaLabel.Size = new Size(240, 30);
            namaLabel.Location = new Point(15, 225);
            namaLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            namaLabel.ForeColor = Color.Black;
            namaLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Create info panel for status and harga
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

            // Create add to cart button - using regular Button for compatibility
            var addToCartBtn = new Button();
            addToCartBtn.Text = "Tambah ke Keranjang";
            addToCartBtn.Size = new Size(240, 45);
            addToCartBtn.Location = new Point(15, 300);
            addToCartBtn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            addToCartBtn.ForeColor = Color.White;
            addToCartBtn.FlatStyle = FlatStyle.Flat;
            addToCartBtn.FlatAppearance.BorderSize = 0;

            // Set button state based on stock and status
            if (pakaian.Stok <= 0 || pakaian.Status != StatusPakaian.Tersedia)
            {
                addToCartBtn.Enabled = false;
                addToCartBtn.Text = pakaian.Stok <= 0 ? "Stok Habis" : "Tidak Tersedia";
                addToCartBtn.BackColor = Color.Gray;
            }
            else
            {
                addToCartBtn.BackColor = Color.LimeGreen;
            }

            // Add click event for add to cart
            addToCartBtn.Click += async (sender, e) => await AddToCart_Click(pakaian, addToCartBtn);

            // Add all controls to panel
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(stokLabel);
            panel.Controls.Add(namaLabel);
            panel.Controls.Add(infoPanel);
            panel.Controls.Add(addToCartBtn);

            return panel;
        }

        private async Task AddToCart_Click(PakaianDto pakaian, Button button)
        {
            try
            {
                // Set loading state for button
                button.Enabled = false;
                button.Text = "Adding...";

                // TODO: Uncomment after creating KeranjangService.cs
                /*
                // Call API to add to cart
                var addToCartRequest = new AddToCartDto { KodePakaian = pakaian.Kode };
                await KeranjangService.AddToKeranjangAsync(addToCartRequest);
                */

                // Temporary: Just show success message
                MessageBox.Show($"'{pakaian.Nama}' berhasil ditambahkan ke keranjang!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh data to update stock
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan ke keranjang:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reset button state
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
            // Restart timer for delayed search
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
                    // Show all pakaian
                    filteredPakaian = new List<PakaianDto>(allPakaian);
                }
                else
                {
                    // Search from API
                    filteredPakaian = await KatalogService.SearchPakaianAsync(searchTerm);
                }

                DisplayPakaian(filteredPakaian);
                labelListPakaian.Text = $"List Pakaian ({filteredPakaian.Count})";
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
        private void flowLayoutPanelListPakaian_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty
        }

        private void labelJudulListPakaian_Click(object sender, EventArgs e)
        {
            // Leave empty
        }
    }
}
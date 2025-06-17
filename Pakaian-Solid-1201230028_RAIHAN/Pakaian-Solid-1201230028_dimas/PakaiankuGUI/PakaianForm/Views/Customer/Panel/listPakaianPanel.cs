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
    public partial class listPakaianPanel : UserControl
    {
        private PakaianDto _currentPakaian;
        public event EventHandler<PakaianDto> OnAddToCartClicked;
        public event EventHandler OnDataChanged;

        public listPakaianPanel()
        {
            InitializeComponent();
            InitializeComponent_Custom();
        }

        private void InitializeComponent_Custom()
        {
            // Set default state
            SetEmptyState();
        }

        // Property to set pakaian data
        public PakaianDto Pakaian
        {
            get { return _currentPakaian; }
            set
            {
                _currentPakaian = value;
                UpdateDisplay();
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
                // Update labels with pakaian data
                labelNamaPakaian.Text = _currentPakaian.Nama ?? "Unknown";
                labelHarga.Text = _currentPakaian.Harga.ToString("C");
                labelStatusPakaian.Text = _currentPakaian.Status.ToString();
                labelBanyakPesanan.Text = _currentPakaian.Stok.ToString();

                // Update button state based on stock and status
                UpdateButtonState();

                // Update visual indicators
                UpdateStockIndicator();

                // Load image if available
                LoadPakaianImage();
            }
            catch (Exception ex)
            {
                // Handle any display errors
                labelNamaPakaian.Text = "Error loading data";
                labelStatusPakaian.Text = ex.Message;
            }
        }

        private void UpdateButtonState()
        {
            if (_currentPakaian == null)
            {
                btnMasukkanKeranjang.Enabled = false;
                btnMasukkanKeranjang.Text = "No Data";
                return;
            }

            // Check if item can be added to cart
            bool canAddToCart = _currentPakaian.Stok > 0 &&
                               _currentPakaian.Status == StatusPakaian.Tersedia;

            btnMasukkanKeranjang.Enabled = canAddToCart;

            if (!canAddToCart)
            {
                if (_currentPakaian.Stok <= 0)
                {
                    btnMasukkanKeranjang.Text = "Stok Habis";
                    btnMasukkanKeranjang.FillColor = Color.Gray;
                    btnMasukkanKeranjang.FillColor2 = Color.DarkGray;
                }
                else
                {
                    btnMasukkanKeranjang.Text = "Tidak Tersedia";
                    btnMasukkanKeranjang.FillColor = Color.Orange;
                    btnMasukkanKeranjang.FillColor2 = Color.DarkOrange;
                }
            }
            else
            {
                btnMasukkanKeranjang.Text = "Tambah";
                btnMasukkanKeranjang.FillColor = Color.Lime;
                btnMasukkanKeranjang.FillColor2 = Color.FromArgb(128, 128, 255);
            }
        }

        private void UpdateStockIndicator()
        {
            if (_currentPakaian == null) return;

            // Update stok label color based on stock level
            if (_currentPakaian.Stok <= 0)
            {
                labelBanyakPesanan.ForeColor = Color.Red;
            }
            else if (_currentPakaian.Stok <= 5)
            {
                labelBanyakPesanan.ForeColor = Color.Orange;
            }
            else
            {
                labelBanyakPesanan.ForeColor = Color.FromArgb(128, 128, 255);
            }
        }

        private void LoadPakaianImage()
        {
            try
            {
                // For now, use default image
                // In future, you can load from URL or file
                guna2PictureBox1.Image = Properties.Resources.tshirt;
            }
            catch
            {
                // If default image fails, set background color
                guna2PictureBox1.Image = null;
                guna2PictureBox1.BackColor = Color.LightGray;
            }
        }

        private void SetEmptyState()
        {
            labelNamaPakaian.Text = "No Data";
            labelHarga.Text = "-";
            labelStatusPakaian.Text = "-";
            labelBanyakPesanan.Text = "0";

            btnMasukkanKeranjang.Enabled = false;
            btnMasukkanKeranjang.Text = "No Data";
            btnMasukkanKeranjang.FillColor = Color.Gray;
            btnMasukkanKeranjang.FillColor2 = Color.DarkGray;

            guna2PictureBox1.Image = null;
            guna2PictureBox1.BackColor = Color.LightGray;
        }

        private async void btnMasukkanKeranjang_Click(object sender, EventArgs e)
        {
            if (_currentPakaian == null) return;

            try
            {
                // Set loading state
                SetLoadingState(true);

                // Call API to add to cart
                var addToCartRequest = new AddToCartDto { KodePakaian = _currentPakaian.Kode };
                await KeranjangService.AddToKeranjangAsync(addToCartRequest);

                // Show success message
                MessageBox.Show($"'{_currentPakaian.Nama}' berhasil ditambahkan ke keranjang!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Fire event to notify parent (optional)
                OnAddToCartClicked?.Invoke(this, _currentPakaian);
                OnDataChanged?.Invoke(this, EventArgs.Empty);

                // Refresh the item (stock might have changed)
                await RefreshPakaianData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan ke keranjang:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading)
        {
            btnMasukkanKeranjang.Enabled = !loading;

            if (loading)
            {
                btnMasukkanKeranjang.Text = "Loading...";
            }
            else
            {
                UpdateButtonState(); // Restore proper button state
            }
        }

        private async Task RefreshPakaianData()
        {
            if (_currentPakaian == null) return;

            try
            {
                // Get updated data from API
                var updatedPakaian = await KatalogService.GetPakaianByKodeAsync(_currentPakaian.Kode);
                if (updatedPakaian != null)
                {
                    Pakaian = updatedPakaian; // This will trigger UpdateDisplay()
                }
            }
            catch (Exception ex)
            {
                // If refresh fails, just update button state with current data
                UpdateButtonState();
            }
        }

        // Public method to manually refresh this panel
        public async Task RefreshAsync()
        {
            await RefreshPakaianData();
        }

        // Public method to set data without triggering events
        public void SetPakaianData(PakaianDto pakaian)
        {
            _currentPakaian = pakaian;
            UpdateDisplay();
        }

        // Designer event handlers
        private void labelBanyakPesanan_Click(object sender, EventArgs e)
        {
            // Optional: Show detailed stock info or tooltip
            if (_currentPakaian != null)
            {
                string stockInfo = $"Stok tersedia: {_currentPakaian.Stok} unit\n" +
                                  $"Status: {_currentPakaian.Status}\n" +
                                  $"Kategori: {_currentPakaian.Kategori}\n" +
                                  $"Warna: {_currentPakaian.Warna}\n" +
                                  $"Ukuran: {_currentPakaian.Ukuran}";

                MessageBox.Show(stockInfo, "Detail Pakaian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Helper method to get current pakaian kode (for external use)
        public string GetPakaianKode()
        {
            return _currentPakaian?.Kode;
        }

        // Helper method to check if this panel has data
        public bool HasData()
        {
            return _currentPakaian != null;
        }
    }
}
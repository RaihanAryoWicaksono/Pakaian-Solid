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
        private bool _isLoading = false;

        // Events
        public event EventHandler<PakaianDto> OnAddToCartClicked;
        public event EventHandler OnDataChanged;
        public event EventHandler<string> OnError;
        public event EventHandler<PakaianDto> OnKeranjangNeedsRefresh; // New event khusus untuk refresh keranjang

        public listPakaianPanel()
        {
            InitializeComponent();
            InitializeComponent_Custom();
        }

        private void InitializeComponent_Custom()
        {
            // Set default state
            SetEmptyState();

            // Add hover effects
            AddHoverEffects();

            // Set up tooltips
            SetupTooltips();
        }

        private void AddHoverEffects()
        {
            // Add hover effect to the entire panel
            this.MouseEnter += (s, e) => {
                if (!_isLoading && _currentPakaian != null)
                {
                    this.BackColor = Color.FromArgb(248, 249, 250);
                    this.Cursor = Cursors.Hand;
                }
            };

            this.MouseLeave += (s, e) => {
                this.BackColor = Color.White;
                this.Cursor = Cursors.Default;
            };
        }

        private void SetupTooltips()
        {
            var tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;

            tooltip.SetToolTip(labelBanyakPesanan, "Klik untuk melihat detail stok");
            tooltip.SetToolTip(guna2PictureBox1, "Gambar produk");
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
                labelHarga.Text = FormatCurrency(_currentPakaian.Harga);
                labelStatusPakaian.Text = GetStatusText(_currentPakaian.Status);
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
                HandleDisplayError(ex);
            }
        }

        private string FormatCurrency(decimal amount)
        {
            return $"Rp {amount:N0}";
        }

        private string GetStatusText(StatusPakaian status)
        {
            switch (status)
            {
                case StatusPakaian.Tersedia:
                    return "Tersedia";
                case StatusPakaian.TidakTersedia:
                    return "Tidak Tersedia";
                case StatusPakaian.DalamKeranjang:
                    return "Dalam Keranjang";
                case StatusPakaian.Dipesan:
                    return "Dipesan";
                case StatusPakaian.Dibayar:
                    return "Dibayar";
                case StatusPakaian.DalamPengiriman:
                    return "Dalam Pengiriman";
                case StatusPakaian.Diterima:
                    return "Diterima";
                default:
                    return status.ToString();
            }
        }

        private void HandleDisplayError(Exception ex)
        {
            labelNamaPakaian.Text = "Error loading data";
            labelHarga.Text = "-";
            labelStatusPakaian.Text = "Error";
            labelBanyakPesanan.Text = "0";

            // Fire error event
            OnError?.Invoke(this, $"Error displaying pakaian data: {ex.Message}");
        }

        private void UpdateButtonState()
        {
            if (_currentPakaian == null)
            {
                SetButtonState(false, "No Data", Color.Gray, Color.DarkGray);
                return;
            }

            // Check if item can be added to cart
            bool canAddToCart = _currentPakaian.Stok > 0 &&
                               _currentPakaian.Status == StatusPakaian.Tersedia;

            if (!canAddToCart)
            {
                if (_currentPakaian.Stok <= 0)
                {
                    SetButtonState(false, "Stok Habis", Color.Gray, Color.DarkGray);
                }
                else if (_currentPakaian.Status == StatusPakaian.DalamKeranjang)
                {
                    SetButtonState(false, "Sudah di Keranjang", Color.Orange, Color.DarkOrange);
                }
                else if (_currentPakaian.Status == StatusPakaian.Dipesan)
                {
                    SetButtonState(false, "Sudah Dipesan", Color.Blue, Color.DarkBlue);
                }
                else
                {
                    SetButtonState(false, GetStatusText(_currentPakaian.Status), Color.Red, Color.DarkRed);
                }
            }
            else
            {
                SetButtonState(true, "Tambah", Color.Lime, Color.FromArgb(128, 128, 255));
            }
        }

        private void SetButtonState(bool enabled, string text, Color fillColor, Color fillColor2)
        {
            btnMasukkanKeranjang.Enabled = enabled && !_isLoading;
            btnMasukkanKeranjang.Text = text;
            btnMasukkanKeranjang.FillColor = fillColor;
            btnMasukkanKeranjang.FillColor2 = fillColor2;
        }

        private void UpdateStockIndicator()
        {
            if (_currentPakaian == null) return;

            // Update stok label color based on stock level
            Color stockColor;
            if (_currentPakaian.Stok <= 0)
            {
                stockColor = Color.Red;
            }
            else if (_currentPakaian.Stok <= 5)
            {
                stockColor = Color.Orange;
            }
            else if (_currentPakaian.Stok <= 10)
            {
                stockColor = Color.Gold;
            }
            else
            {
                stockColor = Color.FromArgb(128, 128, 255);
            }

            labelBanyakPesanan.ForeColor = stockColor;

            // Add blinking effect for very low stock
            if (_currentPakaian.Stok <= 2 && _currentPakaian.Stok > 0)
            {
                labelBanyakPesanan.Font = new Font(labelBanyakPesanan.Font, FontStyle.Bold);
            }
            else
            {
                labelBanyakPesanan.Font = new Font(labelBanyakPesanan.Font, FontStyle.Regular);
            }
        }

        private void LoadPakaianImage()
        {
            try
            {
                // Use default image for now
                guna2PictureBox1.Image = Properties.Resources.tshirt;
                guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                // If loading fails, set background color
                guna2PictureBox1.Image = null;
                guna2PictureBox1.BackColor = Color.LightGray;
                OnError?.Invoke(this, $"Error loading image: {ex.Message}");
            }
        }

        private void SetEmptyState()
        {
            labelNamaPakaian.Text = "No Data";
            labelHarga.Text = "-";
            labelStatusPakaian.Text = "-";
            labelBanyakPesanan.Text = "0";

            SetButtonState(false, "No Data", Color.Gray, Color.DarkGray);

            guna2PictureBox1.Image = null;
            guna2PictureBox1.BackColor = Color.LightGray;

            labelBanyakPesanan.ForeColor = Color.Gray;
        }

        private async void btnMasukkanKeranjang_Click(object sender, EventArgs e)
        {
            if (_currentPakaian == null || _isLoading) return;

            try
            {
                if (!ValidateAddToCart()) return;

                SetLoadingState(true);

                var addToCartRequest = new AddToCartDto
                {
                    KodePakaian = _currentPakaian.Kode,
                    Quantity = 1,
                };

                await KeranjangService.AddToKeranjangAsync(addToCartRequest);

                ShowSuccessMessage($"'{_currentPakaian.Nama}' berhasil ditambahkan ke keranjang!");

                // Update local status
                _currentPakaian.Status = StatusPakaian.DalamKeranjang;
                UpdateDisplay();

                // HANYA GUNAKAN SATU EVENT
                OnAddToCartClicked?.Invoke(this, _currentPakaian);

                // Refresh local data
                await RefreshPakaianData();
            }
            catch (Exception ex)
            {
                HandleAddToCartError(ex);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void HandleAddToCartError(Exception ex)
        {
            string errorMessage = ex.Message.ToLower();

            // Check for specific API errors dari service Anda
            if (errorMessage.Contains("sudah ada di keranjang"))
            {
                ShowErrorMessage($"'{_currentPakaian?.Nama}' sudah ada di keranjang Anda!");

                // Update local status to reflect current state
                if (_currentPakaian != null)
                {
                    _currentPakaian.Status = StatusPakaian.DalamKeranjang;
                    UpdateDisplay();
                }
            }
            else if (errorMessage.Contains("stok tidak mencukupi"))
            {
                ShowErrorMessage("Stok tidak mencukupi!");

                // Refresh data to get latest stock
                _ = RefreshPakaianData();
            }
            else if (errorMessage.Contains("produk tidak ditemukan"))
            {
                ShowErrorMessage("Produk tidak ditemukan!");
            }
            else if (errorMessage.Contains("sesi") && errorMessage.Contains("berakhir"))
            {
                ShowErrorMessage("Sesi Anda telah berakhir. Silakan login kembali.");
            }
            else if (errorMessage.Contains("tidak memiliki akses"))
            {
                ShowErrorMessage("Anda tidak memiliki akses untuk operasi ini.");
            }
            else if (errorMessage.Contains("server") || errorMessage.Contains("500"))
            {
                ShowErrorMessage("Terjadi kesalahan di server. Silakan coba lagi nanti.");
            }
            else if (errorMessage.Contains("network") || errorMessage.Contains("connection"))
            {
                ShowErrorMessage("Koneksi bermasalah. Silakan coba lagi.");
            }
            else
            {
                // Show original error untuk debugging
                ShowErrorMessage($"Gagal menambahkan ke keranjang:\n{ex.Message}");
            }

            // Fire error event for logging
            OnError?.Invoke(this, $"Add to cart failed: {ex.Message}");
        }

        private bool ValidateAddToCart()
        {
            if (_currentPakaian.Stok <= 0)
            {
                ShowErrorMessage("Stok tidak tersedia!");
                return false;
            }

            if (_currentPakaian.Status != StatusPakaian.Tersedia)
            {
                string statusMessage = GetStatusText(_currentPakaian.Status);

                if (_currentPakaian.Status == StatusPakaian.DalamKeranjang)
                {
                    ShowErrorMessage($"'{_currentPakaian.Nama}' sudah ada di keranjang Anda!");
                }
                else
                {
                    ShowErrorMessage($"Produk tidak dapat ditambahkan ke keranjang.\nStatus: {statusMessage}");
                }
                return false;
            }

            return true;
        }

        private void SetLoadingState(bool loading)
        {
            _isLoading = loading;
            btnMasukkanKeranjang.Enabled = !loading;

            if (loading)
            {
                btnMasukkanKeranjang.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                UpdateButtonState(); // Restore proper button state
                this.Cursor = Cursors.Default;
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
                OnError?.Invoke(this, $"Failed to refresh data: {ex.Message}");
            }
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Public methods
        public async Task RefreshAsync()
        {
            await RefreshPakaianData();
        }

        public void SetPakaianData(PakaianDto pakaian)
        {
            _currentPakaian = pakaian;
            UpdateDisplay();
        }

        public string GetPakaianKode()
        {
            return _currentPakaian?.Kode;
        }

        public bool HasData()
        {
            return _currentPakaian != null;
        }

        public bool IsLoading()
        {
            return _isLoading;
        }

        // Event handlers
        private void labelBanyakPesanan_Click(object sender, EventArgs e)
        {
            if (_currentPakaian != null)
            {
                ShowDetailInfo();
            }
        }

        private void ShowDetailInfo()
        {
            string stockInfo = $"=== Detail Pakaian ===\n\n" +
                              $"Nama: {_currentPakaian.Nama}\n" +
                              $"Kode: {_currentPakaian.Kode}\n" +
                              $"Harga: {FormatCurrency(_currentPakaian.Harga)}\n" +
                              $"Stok: {_currentPakaian.Stok} unit\n" +
                              $"Status: {GetStatusText(_currentPakaian.Status)}\n" +
                              $"Kategori: {_currentPakaian.Kategori ?? "N/A"}\n" +
                              $"Warna: {_currentPakaian.Warna ?? "N/A"}\n" +
                              $"Ukuran: {_currentPakaian.Ukuran ?? "N/A"}";

            MessageBox.Show(stockInfo, "Detail Pakaian",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Override for better disposal
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                // Clean up any custom resources
                _currentPakaian = null;
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
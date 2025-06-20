// PakaianForm/Views/Customer/Panel/listKeranjangPanel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk KeranjangItemDto, PakaianDtos
using PakaianForm.Services; // Untuk KeranjangService

namespace PakaianForm.Views.Customer.Panel
{
    public partial class listKeranjangPanel : UserControl
    {
        private KeranjangItemDto _currentItem;
        private int _itemIndex; // Ini akan tetap menjadi index posisi dalam daftar tampilan
        private bool _isLoading = false;

        // --- Event yang akan di-subscribe oleh parent (lihatKeranjangPanel) ---
        public event EventHandler<int> OnRemoveClicked;
        public event EventHandler OnDataChanged; // Event untuk memberi tahu perubahan data ke parent

        // Kontrol yang dideklarasikan di Designer.cs Anda. Akses langsung melalui namanya.
        // Contoh: private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        // private Guna.UI2.WinForms.Guna2Button btnHapusKeranjang;

        public listKeranjangPanel()
        {
            InitializeComponent(); // <-- Ini menginisialisasi SEMUA kontrol dari Designer.cs
            InitializeComponent_Custom();
        }

        private void InitializeComponent_Custom()
        {
            // Hubungkan event handler untuk tombol Hapus
            if (btnHapusKeranjang != null) btnHapusKeranjang.Click += BtnHapusKeranjang_Click;

            // Set default state
            SetEmptyState();

            // Add hover effects
            AddHoverEffects();

            // Setup tooltips
            SetupTooltips();
        }

        private void SetupTooltips()
        {
            var tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;

            if (guna2HtmlLabel5 != null)
                tooltip.SetToolTip(guna2HtmlLabel5, "Klik untuk melihat detail harga");
            if (guna2HtmlLabel2 != null)
                tooltip.SetToolTip(guna2HtmlLabel2, "Klik untuk melihat detail produk");
        }

        private void AddHoverEffects()
        {
            this.MouseEnter += (s, e) => {
                if (!_isLoading && _currentItem != null)
                {
                    this.BackColor = Color.FromArgb(108, 108, 235); // Slightly lighter
                }
            };

            this.MouseLeave += (s, e) => {
                this.BackColor = Color.FromArgb(128, 128, 255); // Original color
            };
        }

        // Properties for setting cart item data
        public KeranjangItemDto CartItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                UpdateDisplay();
            }
        }

        public int ItemIndex
        {
            get { return _itemIndex; }
            set { _itemIndex = value; }
        }

        private void UpdateDisplay()
        {
            Console.WriteLine("=== UPDATE DISPLAY START ===");

            if (_currentItem?.Pakaian == null) // Periksa _currentItem.Pakaian
            {
                Console.WriteLine("No item data, setting empty state");
                SetEmptyState();
                return;
            }

            try
            {
                var pakaian = _currentItem.Pakaian;
                Console.WriteLine($"Updating display for: {pakaian.Nama}");

                // Update labels with cart item data (Menggunakan nama label dari Designer.cs)
                if (guna2HtmlLabel1 != null)
                {
                    guna2HtmlLabel1.Text = pakaian.Nama ?? "Unknown Item"; // Nama Produk
                    guna2HtmlLabel1.Visible = true;
                    Console.WriteLine($"Label1 (Nama): {guna2HtmlLabel1.Text}");
                }

                if (guna2HtmlLabel2 != null)
                {
                    guna2HtmlLabel2.Text = pakaian.Warna ?? "-"; // Warna
                    guna2HtmlLabel2.Visible = true;
                    Console.WriteLine($"Label2 (Warna): {guna2HtmlLabel2.Text}");
                }

                if (guna2HtmlLabel3 != null)
                {
                    guna2HtmlLabel3.Text = pakaian.Ukuran ?? "-"; // Ukuran
                    guna2HtmlLabel3.Visible = true;
                    Console.WriteLine($"Label3 (Ukuran): {guna2HtmlLabel3.Text}");
                }

                if (guna2HtmlLabel4 != null)
                {
                    guna2HtmlLabel4.Text = $"{_currentItem.Quantity}x"; // Kuantitas
                    guna2HtmlLabel4.Visible = true;
                    Console.WriteLine($"Label4 (Quantity): {guna2HtmlLabel4.Text}");
                }

                if (guna2HtmlLabel5 != null)
                {
                    // PERBAIKAN: Multiple fallback untuk harga
                    decimal hargaTotal = GetTotalHargaFromItem();
                    string formattedHarga = FormatCurrency(hargaTotal);

                    guna2HtmlLabel5.Text = formattedHarga; // Total Harga Item
                    guna2HtmlLabel5.Visible = true;

                    Console.WriteLine($"=== HARGA DEBUG ===");
                    Console.WriteLine($"TotalHargaItem dari DTO: {_currentItem.TotalHargaItem}");
                    Console.WriteLine($"HargaSatuan dari DTO: {_currentItem.HargaSatuan}");
                    Console.WriteLine($"Harga dari Pakaian: {pakaian.Harga}");
                    Console.WriteLine($"Quantity: {_currentItem.Quantity}");
                    Console.WriteLine($"Calculated Total: {hargaTotal}");
                    Console.WriteLine($"Formatted: {formattedHarga}");
                    Console.WriteLine($"Label5 Text: '{guna2HtmlLabel5.Text}'");
                    Console.WriteLine($"Label5 Visible: {guna2HtmlLabel5.Visible}");
                }

                // Enable remove button
                if (btnHapusKeranjang != null)
                {
                    btnHapusKeranjang.Enabled = true;
                    btnHapusKeranjang.Text = "Hapus";
                    btnHapusKeranjang.Visible = true;
                    btnHapusKeranjang.FillColor = Color.Red;
                }

                Console.WriteLine($"✅ Display updated successfully for: {pakaian.Nama}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in UpdateDisplay: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                HandleDisplayError(ex);
            }
        }

        private decimal GetTotalHargaFromItem()
        {
            if (_currentItem == null)
            {
                Console.WriteLine("Current item is null, returning 0");
                return 0;
            }

            // 1. Coba dari TotalHargaItem jika ada dan valid
            if (_currentItem.TotalHargaItem > 0)
            {
                Console.WriteLine($"Using TotalHargaItem: {_currentItem.TotalHargaItem}");
                return _currentItem.TotalHargaItem;
            }

            // 2. Coba dari HargaSatuan * Quantity jika ada
            if (_currentItem.HargaSatuan > 0)
            {
                var total = _currentItem.HargaSatuan * _currentItem.Quantity;
                Console.WriteLine($"Using HargaSatuan * Quantity: {_currentItem.HargaSatuan} * {_currentItem.Quantity} = {total}");
                return total;
            }

            // 3. Fallback ke harga dari Pakaian * Quantity
            if (_currentItem.Pakaian?.Harga > 0)
            {
                var total = _currentItem.Pakaian.Harga * _currentItem.Quantity;
                Console.WriteLine($"Using Pakaian.Harga * Quantity: {_currentItem.Pakaian.Harga} * {_currentItem.Quantity} = {total}");
                return total;
            }

            Console.WriteLine("No valid price source found, returning 0");
            return 0;
        }

        private string FormatCurrency(decimal amount)
        {
            try
            {
                return $"Rp {amount:N0}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error formatting currency: {ex.Message}");
                return $"Rp {amount}";
            }
        }

        private void HandleDisplayError(Exception ex)
        {
            // Handle display errors
            if (guna2HtmlLabel1 != null)
            {
                guna2HtmlLabel1.Text = "Error loading item";
                guna2HtmlLabel1.Visible = true;
            }
            if (guna2HtmlLabel2 != null)
            {
                guna2HtmlLabel2.Text = "Error details";
                guna2HtmlLabel2.Visible = true;
            }
            if (guna2HtmlLabel3 != null)
            {
                guna2HtmlLabel3.Text = "-";
                guna2HtmlLabel3.Visible = true;
            }
            if (guna2HtmlLabel4 != null)
            {
                guna2HtmlLabel4.Text = "0x";
                guna2HtmlLabel4.Visible = true;
            }
            if (guna2HtmlLabel5 != null)
            {
                guna2HtmlLabel5.Text = "Rp 0";
                guna2HtmlLabel5.Visible = true;
            }
            if (btnHapusKeranjang != null)
            {
                btnHapusKeranjang.Enabled = false;
                btnHapusKeranjang.Visible = true;
            }

            Console.WriteLine($"Error updating display: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }

        private void SetEmptyState()
        {
            if (guna2HtmlLabel1 != null)
            {
                guna2HtmlLabel1.Text = "No Item";
                guna2HtmlLabel1.Visible = true;
            }
            if (guna2HtmlLabel2 != null)
            {
                guna2HtmlLabel2.Text = "-";
                guna2HtmlLabel2.Visible = true;
            }
            if (guna2HtmlLabel3 != null)
            {
                guna2HtmlLabel3.Text = "-";
                guna2HtmlLabel3.Visible = true;
            }
            if (guna2HtmlLabel4 != null)
            {
                guna2HtmlLabel4.Text = "0x";
                guna2HtmlLabel4.Visible = true;
            }
            if (guna2HtmlLabel5 != null)
            {
                guna2HtmlLabel5.Text = "Rp 0";
                guna2HtmlLabel5.Visible = true;
            }
            if (btnHapusKeranjang != null)
            {
                btnHapusKeranjang.Enabled = false;
                btnHapusKeranjang.Text = "Hapus";
                btnHapusKeranjang.Visible = true;
            }
        }

        private async void BtnHapusKeranjang_Click(object sender, EventArgs e)
        {
            if (_currentItem == null || _isLoading) return;

            var result = MessageBox.Show(
                $"Hapus '{_currentItem.Pakaian?.Nama}' dari keranjang?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SetLoadingState(true);

                    // Call API to remove from cart (menggunakan ID item keranjang dari DB)
                    KeranjangDto updatedCart = await KeranjangService.RemoveFromKeranjangAsync(_currentItem.Id);

                    MessageBox.Show("Item berhasil dihapus dari keranjang!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OnRemoveClicked?.Invoke(this, _currentItem.Id); // Mengembalikan ID
                    OnDataChanged?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error menghapus item:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SetLoadingState(false);
                }
            }
        }

        private void SetLoadingState(bool loading)
        {
            _isLoading = loading;
            if (btnHapusKeranjang != null) btnHapusKeranjang.Enabled = !loading;

            if (loading)
            {
                if (btnHapusKeranjang != null) btnHapusKeranjang.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                if (btnHapusKeranjang != null) btnHapusKeranjang.Text = "Hapus";
                this.Cursor = Cursors.Default;
            }
        }

        // Public method to set data without triggering events
        public void SetCartItemData(KeranjangItemDto item, int index) // Index visual tetap diperlukan untuk List
        {
            Console.WriteLine($"=== SETTING CART ITEM DATA ===");
            Console.WriteLine($"Setting item: {item?.Pakaian?.Nama} with quantity: {item?.Quantity}");
            Console.WriteLine($"TotalHargaItem: {item?.TotalHargaItem}");
            Console.WriteLine($"HargaSatuan: {item?.HargaSatuan}");

            _itemIndex = index; // Tetap simpan index visual
            _currentItem = item;
            UpdateDisplay();

            // Test apakah harga berhasil di-set
            TestPriceDisplay();
        }

        // Helper methods for external access
        public string GetItemKode() { return _currentItem?.Pakaian?.Kode; }
        public int GetQuantity() { return _currentItem?.Quantity ?? 0; }
        public decimal GetTotalHarga() { return GetTotalHargaFromItem(); }
        public bool HasData() { return _currentItem != null; }
        public bool IsLoading() { return _isLoading; }

        // Designer event handlers with improved functionality
        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            if (_currentItem?.Pakaian == null)
            {
                MessageBox.Show("Data item tidak tersedia!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var pakaian = _currentItem.Pakaian;
                decimal hargaSatuan = pakaian.Harga;
                int quantity = _currentItem.Quantity;
                decimal totalCalculated = hargaSatuan * quantity;
                decimal totalFromDto = GetTotalHargaFromItem();

                var breakdown = new StringBuilder();
                breakdown.AppendLine("=== DETAIL HARGA ===");
                breakdown.AppendLine();
                breakdown.AppendLine($"📝 Nama: {pakaian.Nama}");
                breakdown.AppendLine($"💰 Harga satuan: {FormatCurrency(hargaSatuan)}");
                breakdown.AppendLine($"📦 Quantity: {quantity}");
                breakdown.AppendLine($"🧮 Total (dihitung): {FormatCurrency(totalCalculated)}");

                if (totalFromDto != totalCalculated)
                {
                    breakdown.AppendLine($"📋 Total (dari DTO): {FormatCurrency(totalFromDto)}");
                    breakdown.AppendLine();
                    breakdown.AppendLine("⚠️ Ada perbedaan antara harga yang dihitung dan dari server");
                }

                // Debug info
                breakdown.AppendLine();
                breakdown.AppendLine("=== DEBUG INFO ===");
                breakdown.AppendLine($"TotalHargaItem: {_currentItem.TotalHargaItem}");
                breakdown.AppendLine($"HargaSatuan: {_currentItem.HargaSatuan}");

                MessageBox.Show(breakdown.ToString(), "Detail Harga", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error menampilkan detail harga:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in guna2HtmlLabel5_Click: {ex}");
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            if (_currentItem?.Pakaian == null)
            {
                MessageBox.Show("Data produk tidak tersedia!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var pakaian = _currentItem.Pakaian;

                var info = new StringBuilder();
                info.AppendLine("=== DETAIL PRODUK ===");
                info.AppendLine();
                info.AppendLine($"📝 Nama: {pakaian.Nama}");
                info.AppendLine($"🔖 Kode: {pakaian.Kode}");
                info.AppendLine($"🏷️ Kategori: {pakaian.Kategori ?? "-"}");
                info.AppendLine($"🎨 Warna: {pakaian.Warna ?? "-"}");
                info.AppendLine($"📏 Ukuran: {pakaian.Ukuran ?? "-"}");
                info.AppendLine($"💰 Harga: {FormatCurrency(pakaian.Harga)}");
                info.AppendLine($"📦 Stok: {pakaian.Stok}");

                MessageBox.Show(info.ToString(), "Detail Produk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error menampilkan detail produk:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in guna2HtmlLabel2_Click: {ex}");
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            // Event handler untuk ukuran
            if (_currentItem?.Pakaian != null)
            {
                MessageBox.Show($"Ukuran: {_currentItem.Pakaian.Ukuran ?? "Tidak diketahui"}",
                    "Info Ukuran", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Additional helper method for debugging
        public void DebugItem()
        {
            if (_currentItem != null)
            {
                Console.WriteLine($"=== DEBUG ITEM {_itemIndex} ===");
                Console.WriteLine($"Item ID: {_currentItem.Id}");
                Console.WriteLine($"Item Kode Pakaian: {_currentItem.KodePakaian}");
                Console.WriteLine($"Quantity: {_currentItem.Quantity}");
                Console.WriteLine($"HargaSatuan: {_currentItem.HargaSatuan}");
                Console.WriteLine($"TotalHargaItem: {_currentItem.TotalHargaItem}");

                if (_currentItem.Pakaian != null)
                {
                    Console.WriteLine($"Pakaian Nama: {_currentItem.Pakaian.Nama}");
                    Console.WriteLine($"Pakaian Kode: {_currentItem.Pakaian.Kode}");
                    Console.WriteLine($"Pakaian Harga: {_currentItem.Pakaian.Harga}");
                    Console.WriteLine($"Alternative Total: {_currentItem.Pakaian.Harga * _currentItem.Quantity}");
                }
                else
                {
                    Console.WriteLine("Pakaian is NULL");
                }
            }
            else
            {
                Console.WriteLine("Current item is NULL");
            }
        }

        // Method untuk test tampilan harga
        public void TestPriceDisplay()
        {
            Console.WriteLine("=== TESTING PRICE DISPLAY ===");
            if (guna2HtmlLabel5 != null)
            {
                Console.WriteLine($"Label5 Text: '{guna2HtmlLabel5.Text}'");
                Console.WriteLine($"Label5 Visible: {guna2HtmlLabel5.Visible}");
                Console.WriteLine($"Label5 Location: {guna2HtmlLabel5.Location}");
                Console.WriteLine($"Label5 Size: {guna2HtmlLabel5.Size}");
                Console.WriteLine($"Label5 ForeColor: {guna2HtmlLabel5.ForeColor}");
                Console.WriteLine($"Label5 BackColor: {guna2HtmlLabel5.BackColor}");
            }
            else
            {
                Console.WriteLine("❌ guna2HtmlLabel5 is NULL!");
            }

            if (_currentItem != null)
            {
                Console.WriteLine($"Current item exists: {_currentItem.Pakaian?.Nama}");
                Console.WriteLine($"Total from method: {GetTotalHargaFromItem()}");
            }
            else
            {
                Console.WriteLine("No current item");
            }
        }

        private void btnHapusKeranjang_Click_1(object sender, EventArgs e)
        {
            // Duplicate event handler - bisa dihapus
        }
    }
}
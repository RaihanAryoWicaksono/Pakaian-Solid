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
using PakaianLib; // Untuk StatusPakaian, AksiPakaian (penting)

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
            if (_currentItem?.Pakaian == null) // Periksa _currentItem.Pakaian
            {
                SetEmptyState();
                return;
            }

            try
            {
                var pakaian = _currentItem.Pakaian;

                // Update labels with cart item data (Menggunakan nama label dari Designer.cs)
                if (guna2HtmlLabel1 != null) guna2HtmlLabel1.Text = pakaian.Nama ?? "Unknown Item"; // Nama Produk
                if (guna2HtmlLabel2 != null) guna2HtmlLabel2.Text = pakaian.Warna ?? "-"; // Warna
                if (guna2HtmlLabel3 != null) guna2HtmlLabel3.Text = pakaian.Ukuran ?? "-"; // Ukuran
                if (guna2HtmlLabel4 != null) guna2HtmlLabel4.Text = $"{_currentItem.Quantity}x"; // Kuantitas
                if (guna2HtmlLabel5 != null) guna2HtmlLabel5.Text = FormatCurrency(_currentItem.TotalHargaItem); // Total Harga Item

                // Enable remove button
                if (btnHapusKeranjang != null)
                {
                    btnHapusKeranjang.Enabled = true;
                    btnHapusKeranjang.Text = "Hapus";
                    btnHapusKeranjang.Visible = true;
                    btnHapusKeranjang.FillColor = Color.Red;
                }

                // Debug output
                Console.WriteLine($"Updated display: {pakaian.Nama} - {pakaian.Warna} {pakaian.Ukuran} - {_currentItem.Quantity}x - {FormatCurrency(_currentItem.TotalHargaItem)}");
            }
            catch (Exception ex)
            {
                // Handle display errors
                if (guna2HtmlLabel1 != null) guna2HtmlLabel1.Text = "Error loading item";
                if (guna2HtmlLabel1 != null) guna2HtmlLabel1.Visible = true;
                if (guna2HtmlLabel2 != null) guna2HtmlLabel2.Text = "Error details";
                if (guna2HtmlLabel2 != null) guna2HtmlLabel2.Visible = true;
                if (guna2HtmlLabel3 != null) guna2HtmlLabel3.Text = ""; // Kosongkan
                if (guna2HtmlLabel3 != null) guna2HtmlLabel3.Visible = true;
                if (guna2HtmlLabel4 != null) guna2HtmlLabel4.Text = "0x";
                if (guna2HtmlLabel4 != null) guna2HtmlLabel4.Visible = true;
                if (guna2HtmlLabel5 != null) guna2HtmlLabel5.Text = "Rp 0";
                if (guna2HtmlLabel5 != null) guna2HtmlLabel5.Visible = true;
                if (btnHapusKeranjang != null) btnHapusKeranjang.Enabled = false;
                if (btnHapusKeranjang != null) btnHapusKeranjang.Visible = true;

                Console.WriteLine($"Error updating display: {ex.Message}");
            }
        }

        private string FormatCurrency(decimal amount)
        {
            return $"Rp {amount:N0}";
        }

        private void SetEmptyState()
        {
            if (guna2HtmlLabel1 != null) guna2HtmlLabel1.Text = "No Item";
            if (guna2HtmlLabel1 != null) guna2HtmlLabel1.Visible = true;
            if (guna2HtmlLabel2 != null) guna2HtmlLabel2.Text = "-";
            if (guna2HtmlLabel2 != null) guna2HtmlLabel2.Visible = true;
            if (guna2HtmlLabel3 != null) guna2HtmlLabel3.Text = ""; // Kosongkan
            if (guna2HtmlLabel3 != null) guna2HtmlLabel3.Visible = true;
            if (guna2HtmlLabel4 != null) guna2HtmlLabel4.Text = "0x";
            if (guna2HtmlLabel4 != null) guna2HtmlLabel4.Visible = true;
            if (guna2HtmlLabel5 != null) guna2HtmlLabel5.Text = "Rp 0";
            if (guna2HtmlLabel5 != null) guna2HtmlLabel5.Visible = true;
            if (btnHapusKeranjang != null) btnHapusKeranjang.Enabled = false;
            if (btnHapusKeranjang != null) btnHapusKeranjang.Text = "Hapus";
            if (btnHapusKeranjang != null) btnHapusKeranjang.Visible = true;
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
            _itemIndex = index; // Tetap simpan index visual
            _currentItem = item;
            UpdateDisplay();
        }

        // Helper methods for external access
        public string GetItemKode() { return _currentItem?.Pakaian?.Kode; }
        public int GetQuantity() { return _currentItem?.Quantity ?? 0; }
        public decimal GetTotalHarga() { return _currentItem?.TotalHargaItem ?? 0; }
        public bool HasData() { return _currentItem != null; }
        public bool IsLoading() { return _isLoading; }

        // Designer event handlers with improved functionality
        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            if (_currentItem?.Pakaian != null)
            {
                var breakdown = $"=== Detail Harga ===\n\n" +
                                $"Nama: {_currentItem.Pakaian.Nama}\n" +
                                $"Harga satuan: {FormatCurrency(_currentItem.Pakaian.Harga)}\n" +
                                $"Quantity: {_currentItem.Quantity}\n" +
                                $"Total: {FormatCurrency(_currentItem.Pakaian.Harga * _currentItem.Quantity)}"; // Hitung ulang total untuk akurasi

                MessageBox.Show(breakdown, "Detail Harga", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            if (_currentItem?.Pakaian != null)
            {
                string info = $"=== Detail Produk ===\n\n" +
                               $"Nama: {_currentItem.Pakaian.Nama}\n" +
                               $"Warna: {_currentItem.Pakaian.Warna}\n" +
                               $"Ukuran: {_currentItem.Pakaian.Ukuran}\n" +
                               $"Kategori: {_currentItem.Pakaian.Kategori}";

                MessageBox.Show(info, "Detail Produk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            // Label ini disembunyikan, jadi tidak perlu event handler
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
                Console.WriteLine($"HargaSatuan: {_currentItem.HargaSatuan}"); // Tambahkan ini
                Console.WriteLine($"TotalHargaItem (Calculated): {_currentItem.HargaSatuan * _currentItem.Quantity}"); // Hitung ulang

                if (_currentItem.Pakaian != null)
                {
                    Console.WriteLine($"Pakaian Nama: {_currentItem.Pakaian.Nama}");
                    Console.WriteLine($"Pakaian Kode: {_currentItem.Pakaian.Kode}");
                    Console.WriteLine($"Pakaian Harga: {_currentItem.Pakaian.Harga}");
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
    }
}

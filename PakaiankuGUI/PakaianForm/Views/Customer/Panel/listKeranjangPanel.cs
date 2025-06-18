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
    public partial class listKeranjangPanel : UserControl
    {
        private KeranjangItemDto _currentItem;
        private int _itemIndex;
        private bool _isLoading = false;

        public event EventHandler<int> OnRemoveClicked;
        public event EventHandler OnDataChanged;

        public listKeranjangPanel()
        {
            InitializeComponent();
            InitializeComponent_Custom();
        }

        private void InitializeComponent_Custom()
        {
            // Add event handler for remove button
            btnHapusKeranjang.Click += BtnHapusKeranjang_Click;

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
            if (_currentItem?.Pakaian == null)
            {
                SetEmptyState();
                return;
            }

            try
            {
                var pakaian = _currentItem.Pakaian;

                // Format nama produk di label pertama (kiri atas)
                string namaLengkap = $"{pakaian.Nama ?? "Unknown Item"}";
                guna2HtmlLabel1.Text = namaLengkap;
                guna2HtmlLabel1.Visible = true;

                // Format warna dan ukuran di label kedua (kiri bawah)  
                string warnaUkuran = $"{pakaian.Warna ?? "-"} {pakaian.Ukuran ?? "-"}";
                guna2HtmlLabel2.Text = warnaUkuran;
                guna2HtmlLabel2.Visible = true;

                // Tetap tampilkan label ketiga tapi kosongkan atau isi dengan info lain
                guna2HtmlLabel3.Text = ""; // Kosongkan saja
                guna2HtmlLabel3.Visible = true; // Tetap visible tapi kosong

                // Format quantity di label keempat (kanan)
                guna2HtmlLabel4.Text = $"{_currentItem.Quantity}x";
                guna2HtmlLabel4.Visible = true;

                // Format harga di label kelima (kanan)
                guna2HtmlLabel5.Text = FormatCurrency(_currentItem.TotalHarga);
                guna2HtmlLabel5.Visible = true;

                // Enable remove button
                btnHapusKeranjang.Enabled = true;
                btnHapusKeranjang.Text = "Hapus";
                btnHapusKeranjang.Visible = true;
                btnHapusKeranjang.FillColor = Color.Red;

                // Debug output
                Console.WriteLine($"Updated display: {namaLengkap} - {warnaUkuran} - {_currentItem.Quantity}x - {FormatCurrency(_currentItem.TotalHarga)}");
            }
            catch (Exception ex)
            {
                // Handle display errors
                guna2HtmlLabel1.Text = "Error loading item";
                guna2HtmlLabel1.Visible = true;
                guna2HtmlLabel2.Text = "Error details";
                guna2HtmlLabel2.Visible = true;
                guna2HtmlLabel3.Text = "";
                guna2HtmlLabel3.Visible = true;
                guna2HtmlLabel4.Text = "0x";
                guna2HtmlLabel4.Visible = true;
                guna2HtmlLabel5.Text = "Rp 0";
                guna2HtmlLabel5.Visible = true;
                btnHapusKeranjang.Enabled = false;
                btnHapusKeranjang.Visible = true;

                Console.WriteLine($"Error updating display: {ex.Message}");
            }
        }

        private string FormatCurrency(decimal amount)
        {
            return $"Rp {amount:N0}";
        }

        private void SetEmptyState()
        {
            guna2HtmlLabel1.Text = "No Item";
            guna2HtmlLabel1.Visible = true;
            guna2HtmlLabel2.Text = "-";
            guna2HtmlLabel2.Visible = true;
            guna2HtmlLabel3.Text = "";
            guna2HtmlLabel3.Visible = true;
            guna2HtmlLabel4.Text = "0x";
            guna2HtmlLabel4.Visible = true;
            guna2HtmlLabel5.Text = "Rp 0";
            guna2HtmlLabel5.Visible = true;
            btnHapusKeranjang.Enabled = false;
            btnHapusKeranjang.Text = "Hapus";
            btnHapusKeranjang.Visible = true;
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
                    // Set loading state
                    SetLoadingState(true);

                    // Call API to remove from cart
                    bool success = await KeranjangService.RemoveFromKeranjangAsync(_itemIndex);

                    if (success)
                    {
                        MessageBox.Show("Item berhasil dihapus dari keranjang!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Notify parent container
                        OnRemoveClicked?.Invoke(this, _itemIndex);
                        OnDataChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Gagal menghapus item dari keranjang.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error menghapus item:\n{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnHapusKeranjang.Enabled = !loading;

            if (loading)
            {
                btnHapusKeranjang.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnHapusKeranjang.Text = "Hapus";
                this.Cursor = Cursors.Default;
            }
        }

        // Public method to set data without triggering events
        public void SetCartItemData(KeranjangItemDto item, int index)
        {
            _itemIndex = index;
            _currentItem = item;

            // Debug log untuk troubleshooting
            if (item != null)
            {
                Console.WriteLine($"Setting cart item data: Index={index}, Item={item.Id}");
                if (item.Pakaian != null)
                {
                    Console.WriteLine($"Pakaian: {item.Pakaian.Nama}, Quantity: {item.Quantity}, Total: {item.TotalHarga}");
                }
                else
                {
                    Console.WriteLine("Warning: Pakaian is null!");
                }
            }
            else
            {
                Console.WriteLine("Warning: Item is null!");
            }

            UpdateDisplay();
        }

        // Helper methods for external access
        public string GetItemKode()
        {
            return _currentItem?.Pakaian?.Kode;
        }

        public int GetPakaianId()
        {
            return _currentItem?.PakaianId ?? 0;
        }

        public int GetUserId()
        {
            return _currentItem?.UserId ?? 0;
        }

        private string GetKodePakaianFromItem(KeranjangItemDto item)
        {
            try
            {
                // Sesuai dengan struktur KeranjangItemDto yang ada
                if (item.Pakaian?.Kode != null)
                    return item.Pakaian.Kode;

                // Fallback jika Pakaian null tapi ada PakaianId
                return item.PakaianId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting kode pakaian: {ex.Message}");
                return string.Empty;
            }
        }

        public int GetQuantity()
        {
            return _currentItem?.Quantity ?? 0;
        }

        public decimal GetTotalHarga()
        {
            return _currentItem?.TotalHarga ?? 0;
        }

        public bool HasData()
        {
            return _currentItem != null;
        }

        public bool IsLoading()
        {
            return _isLoading;
        }

        // Designer event handlers with improved functionality
        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            // Show price breakdown
            if (_currentItem?.Pakaian != null)
            {
                var breakdown = $"=== Detail Harga ===\n\n" +
                               $"Nama: {_currentItem.Pakaian.Nama}\n" +
                               $"Harga satuan: {FormatCurrency(_currentItem.Pakaian.Harga)}\n" +
                               $"Quantity: {_currentItem.Quantity}\n" +
                               $"Total: {FormatCurrency(_currentItem.TotalHarga)}";

                MessageBox.Show(breakdown, "Detail Harga",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            // Show warna dan ukuran info
            if (_currentItem?.Pakaian != null)
            {
                string info = $"=== Detail Produk ===\n\n" +
                             $"Nama: {_currentItem.Pakaian.Nama}\n" +
                             $"Warna: {_currentItem.Pakaian.Warna}\n" +
                             $"Ukuran: {_currentItem.Pakaian.Ukuran}\n" +
                             $"Kategori: {_currentItem.Pakaian.Kategori}";

                MessageBox.Show(info, "Detail Produk",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Console.WriteLine($"Item Type: {_currentItem.GetType().Name}");
                Console.WriteLine($"Quantity: {_currentItem.Quantity}");
                Console.WriteLine($"TotalHarga: {_currentItem.TotalHarga}");

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
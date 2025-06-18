
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
using PakaianForm.Models; // Untuk KeranjangItemDto, PakaianDto
using PakaianForm.Services; // Untuk KeranjangService
using PakaianLib; // <<<< PASTI ADA DAN BENAR UNTUK StatusPakaian, AksiPakaian (penting)

namespace PakaianForm.Views.Customer.Panel
{
    public partial class listKeranjangPanel : UserControl
    {
        private KeranjangItemDto _currentItem;
        private int _itemIndex;

        public event EventHandler<int> OnRemoveClicked;
        public event EventHandler OnDataChanged; // Event untuk memberi tahu perubahan data ke parent

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

                // Update labels with cart item data
                guna2HtmlLabel1.Text = pakaian.Nama ?? "Unknown Item";
                guna2HtmlLabel2.Text = pakaian.Warna ?? "-";
                guna2HtmlLabel3.Text = pakaian.Ukuran ?? "-";
                guna2HtmlLabel4.Text = $"{_currentItem.Quantity}x";
                guna2HtmlLabel5.Text = _currentItem.TotalHargaItem.ToString("C");

                // Enable remove button
                btnHapusKeranjang.Enabled = true;
            }
            catch (Exception ex)
            {
                // Handle display errors
                guna2HtmlLabel1.Text = "Error loading item";
                guna2HtmlLabel2.Text = ex.Message;
                btnHapusKeranjang.Enabled = false;
            }
        }

        private void SetEmptyState()
        {
            guna2HtmlLabel1.Text = "No Item";
            guna2HtmlLabel2.Text = "-";
            guna2HtmlLabel3.Text = "-";
            guna2HtmlLabel4.Text = "0x";
            guna2HtmlLabel5.Text = "Rp. 0";
            btnHapusKeranjang.Enabled = false;
        }

        private async void BtnHapusKeranjang_Click(object sender, EventArgs e)
        {
            if (_currentItem == null) return;

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
                    // KeranjangService.RemoveFromKeranjangAsync sekarang mengembalikan KeranjangDto
                    // Kita asumsikan sukses jika tidak ada exception.
                    KeranjangDto updatedCart = await KeranjangService.RemoveFromKeranjangAsync(_itemIndex);

                    MessageBox.Show("Item berhasil dihapus dari keranjang!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Notify parent container
                    OnRemoveClicked?.Invoke(this, _itemIndex);
                    OnDataChanged?.Invoke(this, EventArgs.Empty); // Beri tahu parent bahwa data keranjang berubah

                    // Setelah item dihapus, panel ini mungkin perlu menyembunyikan dirinya sendiri
                    // atau di-refresh oleh parent yang memuat ulang daftar keranjang.
                    // Jika parent menangani refresh penuh, Anda bisa menghapus kontrol ini dari tampilan.
                    // this.Parent?.Controls.Remove(this);
                    // this.Dispose(); // Lepaskan resource
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
            btnHapusKeranjang.Enabled = !loading;

            if (loading)
            {
                btnHapusKeranjang.Text = "Loading...";
            }
            else
            {
                btnHapusKeranjang.Text = "Hapus";
            }
        }

        // Public method to set data without triggering events
        public void SetCartItemData(KeranjangItemDto item, int index)
        {
            _itemIndex = index;
            _currentItem = item;
            UpdateDisplay();
        }

        // Helper methods for external access
        public string GetItemKode()
        {
            return _currentItem?.Pakaian?.Kode;
        }

        public int GetQuantity()
        {
            return _currentItem?.Quantity ?? 0;
        }

        public decimal GetTotalHarga()
        {
            return _currentItem?.TotalHargaItem ?? 0;
        }

        public bool HasData()
        {
            return _currentItem != null;
        }

        // Designer event handlers
        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            // Optional: Show price breakdown
            if (_currentItem?.Pakaian != null)
            {
                var breakdown = $"Harga satuan: {_currentItem.Pakaian.Harga:C}\n" +
                                $"Quantity: {_currentItem.Quantity}\n" +
                                $"Total: {_currentItem.TotalHargaItem:C}";

                MessageBox.Show(breakdown, "Detail Harga",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            // Optional: Show color info
            if (_currentItem?.Pakaian != null)
            {
                MessageBox.Show($"Warna: {_currentItem.Pakaian.Warna}", "Info Warna",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            // Optional: Show size info
            if (_currentItem?.Pakaian != null)
            {
                MessageBox.Show($"Ukuran: {_currentItem.Pakaian.Ukuran}", "Info Ukuran",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

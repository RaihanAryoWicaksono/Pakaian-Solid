﻿using System;
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
    public partial class lihatKeranjangPanel : UserControl
    {
       
        public lihatKeranjangPanel()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
           
            if (lblCheckoutTitle != null) lblCheckoutTitle.Text = "Checkout Sekarang!";

            
            if (flowLayoutPanel1 != null)
            {
                flowLayoutPanel1.AutoScroll = true;
                flowLayoutPanel1.WrapContents = false; 
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown; 
                flowLayoutPanel1.AutoScrollMargin = new Size(0, 20); 
            }

            
            if (btnCheckout != null) btnCheckout.Click += btnCheckout_Click_1;

            this.Load += lihatKeranjangPanel_Load;
        }

        private async void lihatKeranjangPanel_Load(object sender, EventArgs e)
        {
            await LoadKeranjangData();
        }

        public async Task LoadKeranjangData()
        {
            // SuspendLayout untuk performa saat memperbarui UI
            if (flowLayoutPanel1 != null) flowLayoutPanel1.SuspendLayout();
            try
            {
                if (flowLayoutPanel1 != null) flowLayoutPanel1.Controls.Clear();

                
                KeranjangDto keranjangData = await KeranjangService.GetKeranjangAsync();

                if (keranjangData != null && keranjangData.Items != null && keranjangData.Items.Any())
                {
                    int itemIndex = 0; // Digunakan sebagai ID unik sementara untuk in-memory keranjang
                    foreach (var item in keranjangData.Items)
                    {
                        // Membuat instance listKeranjangPanel
                        listKeranjangPanel itemPanel = new listKeranjangPanel();
                        itemPanel.SetCartItemData(item, itemIndex); // Mengisi data item keranjang

                        // Berlangganan event dari itemPanel
                        itemPanel.OnRemoveClicked += ItemPanel_OnRemoveClicked;
                        itemPanel.OnDataChanged += ItemPanel_OnDataChanged;

                        itemPanel.Margin = new Padding(5); // Margin antar item
                        if (flowLayoutPanel1 != null) flowLayoutPanel1.Controls.Add(itemPanel);
                        itemIndex++;
                    }
                }
                else
                {
                    ShowEmptyState("Keranjang Anda kosong.");
                }

                // Perbarui total harga dan jumlah item di bagian bawah panel
                if (lblTotalHarga != null) lblTotalHarga.Text = $"Total: Rp{keranjangData?.TotalHarga:N0}";
                if (lblJumlahItem != null) lblJumlahItem.Text = $"Jumlah Item: {keranjangData?.JumlahItem ?? 0}";

                // Atur status tombol Checkout
                if (btnCheckout != null) btnCheckout.Enabled = (keranjangData?.JumlahItem ?? 0) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memuat keranjang:\n{ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowEmptyState("Gagal memuat data keranjang.");
                if (lblTotalHarga != null) lblTotalHarga.Text = "Total: Rp0";
                if (lblJumlahItem != null) lblJumlahItem.Text = "Item: 0";
                if (btnCheckout != null) btnCheckout.Enabled = false;
            }
            finally
            {
                if (flowLayoutPanel1 != null) flowLayoutPanel1.ResumeLayout(true);
                if (flowLayoutPanel1 != null) flowLayoutPanel1.Invalidate();
                if (flowLayoutPanel1 != null) flowLayoutPanel1.Update();
            }
        }

        private void ShowEmptyState(string message)
        {
            if (flowLayoutPanel1 == null) return;
            flowLayoutPanel1.Controls.Clear();
            Label emptyLabel = new Label();
            emptyLabel.Text = message;
            emptyLabel.AutoSize = true;
            emptyLabel.Font = new Font("Segoe UI", 14);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.Location = new Point((flowLayoutPanel1.Width - emptyLabel.Width) / 2, (flowLayoutPanel1.Height - emptyLabel.Height) / 2);
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter; // Pastikan teks di tengah
            flowLayoutPanel1.Controls.Add(emptyLabel);
        }

        private async void ItemPanel_OnRemoveClicked(object sender, int removedId) // <--- Menerima ID item yang dihapus
        {
            await LoadKeranjangData();
            MessageBox.Show($"Item pada index {removedId} berhasil dihapus dari keranjang.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void ItemPanel_OnDataChanged(object sender, EventArgs e)
        {
            await LoadKeranjangData();
        }

        private async void btnCheckout_Click_1(object sender, EventArgs e) // <--- DEFINISI METHOD INI
        {
            
                if (Services.UserSession.IsLoggedIn && Services.UserSession.UserId > 0)
                {
                    try
                    {
                        if (btnCheckout != null)
                        {
                            btnCheckout.Enabled = false;
                            btnCheckout.Text = "Processing...";
                        }

                        CheckoutDto checkoutDto = new CheckoutDto
                        {
                            AlamatPengiriman = "Alamat Pengiriman Contoh",
                            MetodePembayaran = "Transfer Bank"
                        };

                        CheckoutResponseDto response = await KeranjangService.CheckoutAsync(checkoutDto);

                        MessageBox.Show($"Checkout berhasil! Order ID: {response.OrderId}\nTotal: Rp{response.TotalHarga:N0}\nStatus: {response.StatusPemesanan}", "Checkout Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await LoadKeranjangData(); // Muat ulang keranjang setelah checkout
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Checkout gagal:\n{ex.Message}", "Checkout Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {   
                        if (btnCheckout != null)
                        {
                            btnCheckout.Enabled = true;
                            btnCheckout.Text = "Checkout";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Anda harus login untuk melakukan checkout.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
        }
    }
}

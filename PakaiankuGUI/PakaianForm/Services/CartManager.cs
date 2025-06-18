//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using PakaianForm.Models;

//namespace PakaianForm.Views.Customer.Panel
//{
//    // =================== CART MANAGER ===================
//    /// <summary>
//    /// Static class untuk manage data keranjang antar panel
//    /// </summary>
//    public static class CartManager
//    {
//        private static KeranjangDto _cart = new KeranjangDto();
        
//        public static event EventHandler CartUpdated;

//        public static KeranjangDto GetCart()
//        {
//            return _cart;
//        }

//        public static void AddToCart(PakaianDto pakaian, int quantity = 1)
//        {
//            // Check if item already exists in cart
//            var existingItem = _cart.Items.FirstOrDefault(x => 
//                x.Pakaian.Kode == pakaian.Kode && 
//                x.Pakaian.Warna == pakaian.Warna && 
//                x.Pakaian.Ukuran == pakaian.Ukuran);
            
//            if (existingItem != null)
//            {
//                // Update existing item
//                existingItem.Quantity += quantity;
//                existingItem.TotalHarga = existingItem.Quantity * existingItem.Pakaian.Harga;
//            }
//            else
//            {
//                // Add new item
//                var newItem = new KeranjangItemDto
//                {
//                    Id = _cart.Items.Count + 1,
//                    UserId = 1, // Mock user ID
//                    PakaianId = int.Parse(pakaian.Kode.Replace("PKN", "")), // Extract numeric ID
//                    Pakaian = pakaian,
//                    Quantity = quantity,
//                    TotalHarga = quantity * pakaian.Harga,
//                    CreatedAt = DateTime.Now
//                };
//                _cart.Items.Add(newItem);
//            }

//            // Update cart totals
//            UpdateCartTotals();

//            // Notify all subscribers
//            CartUpdated?.Invoke(null, EventArgs.Empty);
//        }

//        public static bool RemoveFromCart(int itemId)
//        {
//            var item = _cart.Items.FirstOrDefault(x => x.Id == itemId);
//            if (item != null)
//            {
//                _cart.Items.Remove(item);
//                UpdateCartTotals();
//                CartUpdated?.Invoke(null, EventArgs.Empty);
//                return true;
//            }
//            return false;
//        }

//        public static bool RemoveFromCartByIndex(int index)
//        {
//            if (index >= 0 && index < _cart.Items.Count)
//            {
//                _cart.Items.RemoveAt(index);
//                UpdateCartTotals();
//                CartUpdated?.Invoke(null, EventArgs.Empty);
//                return true;
//            }
//            return false;
//        }

//        public static void ClearCart()
//        {
//            _cart.Items.Clear();
//            UpdateCartTotals();
//            CartUpdated?.Invoke(null, EventArgs.Empty);
//        }

//        private static void UpdateCartTotals()
//        {
//            _cart.TotalHarga = _cart.Items.Sum(x => x.TotalHarga);
//            _cart.JumlahItem = _cart.Items.Sum(x => x.Quantity);
//        }

//        public static void LoadDummyData()
//        {
//            _cart.Items.Clear();
            
//            // Add some dummy data
//            var dummyItems = new[]
//            {
//                new KeranjangItemDto
//                {
//                    Id = 1,
//                    UserId = 1,
//                    PakaianId = 1,
//                    Quantity = 2,
//                    TotalHarga = 200000,
//                    CreatedAt = DateTime.Now.AddMinutes(-30),
//                    Pakaian = new PakaianDto
//                    {
//                        Kode = "PKN001",
//                        Nama = "T-Shirt Premium",
//                        Warna = "Black",
//                        Ukuran = "XL",
//                        Harga = 100000,
//                        Kategori = "T-Shirt",
//                        Stok = 10,
//                        Status = StatusPakaian.Tersedia
//                    }
//                },
//                new KeranjangItemDto
//                {
//                    Id = 2,
//                    UserId = 1,
//                    PakaianId = 2,
//                    Quantity = 1,
//                    TotalHarga = 150000,
//                    CreatedAt = DateTime.Now.AddMinutes(-15),
//                    Pakaian = new PakaianDto
//                    {
//                        Kode = "PKN002",
//                        Nama = "Jaket Hoodie",
//                        Warna = "Navy",
//                        Ukuran = "L",
//                        Harga = 150000,
//                        Kategori = "Jaket",
//                        Stok = 5,
//                        Status = StatusPakaian.Tersedia
//                    }
//                }
//            };

//            foreach (var item in dummyItems)
//            {
//                _cart.Items.Add(item);
//            }

//            UpdateCartTotals();
//            CartUpdated?.Invoke(null, EventArgs.Empty);
//        }
//    }

//    // =================== MOCK SERVICES ===================
//    public static class KeranjangService
//    {
//        public static async Task<bool> AddToKeranjangAsync(AddToCartDto request)
//        {
//            await Task.Delay(500); // Simulate API delay
//            return true;
//        }

//        public static async Task<bool> RemoveFromKeranjangAsync(int itemIndex)
//        {
//            await Task.Delay(300);
//            return CartManager.RemoveFromCartByIndex(itemIndex);
//        }

//        public static async Task<List<KeranjangItemDto>> GetKeranjangAsync()
//        {
//            await Task.Delay(200);
//            return CartManager.GetCart().Items;
//        }

//        public static async Task<bool> ClearKeranjangAsync()
//        {
//            await Task.Delay(300);
//            CartManager.ClearCart();
//            return true;
//        }
//    }

//    public static class KatalogService
//    {
//        public static async Task<PakaianDto> GetPakaianByKodeAsync(string kode)
//        {
//            await Task.Delay(200);
//            // Return updated mock data (stock might decrease after adding to cart)
//            return new PakaianDto 
//            { 
//                Kode = kode, 
//                Nama = "Updated Item", 
//                Stok = 9, // Decrease stock
//                Status = StatusPakaian.Tersedia 
//            };
//        }

//        internal static async Task<List<PakaianDto>> GetAllPakaianAsync()
//        {
//            throw new NotImplementedException();
//        }

//        internal static async Task<List<PakaianDto>> SearchPakaianAsync(string searchTerm)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    public static class CheckoutService
//    {
//        public static async Task<CheckoutResponseDto> ProcessCheckoutAsync(CheckoutDto checkoutData)
//        {
//            await Task.Delay(2000); // Simulate checkout processing

//            var cart = CartManager.GetCart();
//            var response = new CheckoutResponseDto
//            {
//                OrderId = $"ORD{DateTime.Now.Ticks.ToString().Substring(10)}",
//                TanggalPemesanan = DateTime.Now,
//                Items = cart.Items,
//                TotalHarga = cart.TotalHarga,
//                StatusPemesanan = "Dipesan",
//                AlamatPengiriman = checkoutData.AlamatPengiriman,
//                MetodePembayaran = checkoutData.MetodePembayaran
//            };

//            // Clear cart after successful checkout
//            CartManager.ClearCart();

//            return response;
//        }
//    }

//    // =================== UPDATED LIHAT KERANJANG PANEL ===================
//    public partial class lihatKeranjangPanel : UserControl
//    {
//        private List<KeranjangItemDto> _keranjangItems;
//        private List<listKeranjangPanel> _itemPanels;

//        public event EventHandler OnCheckoutClicked;
//        public event EventHandler OnKeranjangUpdated;

        

//        private void InitializeCustomComponents()
//        {
//            _keranjangItems = new List<KeranjangItemDto>();
//            _itemPanels = new List<listKeranjangPanel>();

//            // Setup checkout button event
//            guna2GradientButton1.Click += Guna2GradientButton1_Click;

//            // Setup flowLayoutPanel
//            flowLayoutPanel1.AutoScroll = true;
//            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
//            flowLayoutPanel1.WrapContents = false;

//            // Remove existing static panels from designer
//            flowLayoutPanel1.Controls.Clear();

//            // Subscribe to cart updates
//            CartManager.CartUpdated += CartManager_CartUpdated;
//        }

//        private void CartManager_CartUpdated(object sender, EventArgs e)
//        {
//            // Update display when cart changes
//            this.Invoke(new Action(() => {
//                LoadKeranjangData();
//            }));
//        }

        

        

//        private void LoadKeranjangData()
//        {
//            try
//            {
//                // Get data from CartManager
//                _keranjangItems = CartManager.GetCart().Items;

//                // Update display
//                UpdateKeranjangDisplay();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading keranjang: {ex.Message}",
//                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void UpdateKeranjangDisplay()
//        {
//            // Clear existing panels
//            ClearCurrentPanels();

//            if (_keranjangItems == null || _keranjangItems.Count == 0)
//            {
//                ShowEmptyKeranjangState();
//                return;
//            }

//            // Create new panels for each item
//            for (int i = 0; i < _keranjangItems.Count; i++)
//            {
//                var itemPanel = CreateItemPanel(_keranjangItems[i], i);
//                _itemPanels.Add(itemPanel);
//                flowLayoutPanel1.Controls.Add(itemPanel);
//            }

//            // Update label
//            labelListPakaian.Text = $"Keranjang Belanja ({_keranjangItems.Count} item)";
            
//            // Update checkout button
//            UpdateCheckoutButton();
//        }

//        private listKeranjangPanel CreateItemPanel(KeranjangItemDto item, int index)
//        {
//            var panel = new listKeranjangPanel();

//            // Set data
//            panel.SetCartItemData(item, index);

//            // Subscribe to events
//            panel.OnRemoveClicked += ItemPanel_OnRemoveClicked;
//            panel.OnDataChanged += ItemPanel_OnDataChanged;

//            // Set size and margin
//            panel.Size = new Size(907, 91);
//            panel.Margin = new Padding(4, 5, 4, 5);

//            return panel;
//        }

//        private void ItemPanel_OnRemoveClicked(object sender, int itemIndex)
//        {
//            try
//            {
//                // Remove from CartManager
//                if (itemIndex >= 0 && itemIndex < _keranjangItems.Count)
//                {
//                    var removedItem = _keranjangItems[itemIndex];
//                    CartManager.RemoveFromCartByIndex(itemIndex);

//                    MessageBox.Show($"'{removedItem.Pakaian?.Nama}' berhasil dihapus dari keranjang!",
//                        "Item Dihapus", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    // Event will be handled by CartManager_CartUpdated
//                    OnKeranjangUpdated?.Invoke(this, EventArgs.Empty);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error menghapus item: {ex.Message}",
//                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void ItemPanel_OnDataChanged(object sender, EventArgs e)
//        {
//            UpdateCheckoutButton();
//            OnKeranjangUpdated?.Invoke(this, EventArgs.Empty);
//        }

//        private void ClearCurrentPanels()
//        {
//            foreach (var panel in _itemPanels)
//            {
//                panel.OnRemoveClicked -= ItemPanel_OnRemoveClicked;
//                panel.OnDataChanged -= ItemPanel_OnDataChanged;
//                panel.Dispose();
//            }

//            _itemPanels.Clear();
//            flowLayoutPanel1.Controls.Clear();
//        }

//        private void ShowEmptyKeranjangState()
//        {
//            labelListPakaian.Text = "Keranjang Kosong";

//            var emptyPanel = new System.Windows.Forms.Panel();
//            emptyPanel.Size = new Size(907, 200);
//            emptyPanel.BackColor = Color.White;

//            var emptyLabel = new System.Windows.Forms.Label();
//            emptyLabel.Text = "Keranjang belanja Anda kosong.\nSilakan tambahkan produk terlebih dahulu.";
//            emptyLabel.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
//            emptyLabel.ForeColor = Color.Gray;
//            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
//            emptyLabel.Dock = DockStyle.Fill;

//            emptyPanel.Controls.Add(emptyLabel);
//            flowLayoutPanel1.Controls.Add(emptyPanel);
//        }

//        private void UpdateCheckoutButton()
//        {
//            var cart = CartManager.GetCart();
            
//            if (cart.Items.Count == 0)
//            {
//                guna2GradientButton1.Text = "Checkout";
//                guna2GradientButton1.Enabled = false;
//                return;
//            }

//            guna2GradientButton1.Text = $"Checkout ({cart.JumlahItem} items) - {cart.TotalHarga:C}";
//            guna2GradientButton1.Enabled = true;
//        }

//        private async void Guna2GradientButton1_Click(object sender, EventArgs e)
//        {
//            var cart = CartManager.GetCart();
            
//            if (cart.Items.Count == 0)
//            {
//                MessageBox.Show("Keranjang belanja kosong!",
//                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            var confirmMessage = $"Konfirmasi checkout:\n\n" +
//                               $"Total item: {cart.JumlahItem}\n" +
//                               $"Total harga: {cart.TotalHarga:C}\n\n" +
//                               $"Lanjutkan ke pembayaran?";

//            var result = MessageBox.Show(confirmMessage, "Konfirmasi Checkout",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (result == DialogResult.Yes)
//            {
//                try
//                {
//                    SetLoadingState(true);

//                    // Process checkout
//                    var checkoutData = new CheckoutDto
//                    {
//                        AlamatPengiriman = "Alamat Default Customer",
//                        MetodePembayaran = "Transfer Bank"
//                    };

//                    var response = await CheckoutService.ProcessCheckoutAsync(checkoutData);

//                    MessageBox.Show($"Checkout berhasil!\n\n" +
//                                  $"Order ID: {response.OrderId}\n" +
//                                  $"Total: {response.TotalHarga:C}\n" +
//                                  $"Status: {response.StatusPemesanan}",
//                        "Checkout Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    // Cart already cleared by service
//                    OnCheckoutClicked?.Invoke(this, EventArgs.Empty);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Gagal melakukan checkout:\n{ex.Message}",
//                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//                finally
//                {
//                    SetLoadingState(false);
//                }
//            }
//        }

//        private void SetLoadingState(bool loading)
//        {
//            guna2GradientButton1.Enabled = !loading;
//            flowLayoutPanel1.Enabled = !loading;

//            if (loading)
//            {
//                guna2GradientButton1.Text = "Processing...";
//            }
//            else
//            {
//                UpdateCheckoutButton();
//            }
//        }

//        // =================== PUBLIC METHODS ===================

//        public void AddItemToKeranjang(PakaianDto pakaian, int quantity = 1)
//        {
//            CartManager.AddToCart(pakaian, quantity);
//            MessageBox.Show($"'{pakaian.Nama}' berhasil ditambahkan ke keranjang!",
//                "Item Ditambahkan", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        public void RefreshKeranjang()
//        {
//            LoadKeranjangData();
//        }

//        public void ClearKeranjang()
//        {
//            var result = MessageBox.Show("Hapus semua item dari keranjang?",
//                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (result == DialogResult.Yes)
//            {
//                CartManager.ClearCart();
//                MessageBox.Show("Keranjang berhasil dikosongkan!",
//                    "Keranjang Dikosongkan", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        public decimal GetTotalHarga()
//        {
//            return CartManager.GetCart().TotalHarga;
//        }

//        public int GetTotalItems()
//        {
//            return CartManager.GetCart().JumlahItem;
//        }

//        public bool IsKeranjangEmpty()
//        {
//            return CartManager.GetCart().Items.Count == 0;
//        }

//        public int GetItemCount()
//        {
//            return CartManager.GetCart().Items.Count;
//        }

//        public string GetKeranjangSummary()
//        {
//            var cart = CartManager.GetCart();
//            if (cart.Items.Count == 0)
//                return "Keranjang kosong";

//            return $"{cart.Items.Count} jenis item, {cart.JumlahItem} total barang, {cart.TotalHarga:C}";
//        }

        
//    }
//}
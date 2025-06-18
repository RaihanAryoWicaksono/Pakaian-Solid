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
    public partial class lihatKeranjangPanel : UserControl
    {
        private KeranjangDto currentKeranjang;
        private List<string> itemsInCart = new List<string>();
        private bool isLoading = false;

        // Event untuk memberitahu parent tentang perubahan keranjang
        public event Action OnKeranjangChanged;
        public event EventHandler<string> OnError;

        public lihatKeranjangPanel()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configure FlowLayoutPanel
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;

            // Setup event handler untuk checkout button
            guna2GradientButton1.Click += CheckoutButton_Click;

            // Clear static panels
            ClearStaticPanels();
        }

        private void ClearStaticPanels()
        {
            // Remove all static listKeranjangPanel controls
            var panelsToRemove = flowLayoutPanel1.Controls
                .OfType<listKeranjangPanel>()
                .ToList();

            foreach (var panel in panelsToRemove)
            {
                flowLayoutPanel1.Controls.Remove(panel);
                panel.Dispose();
            }
        }

        private async void lihatKeranjangPanel_Load(object sender, EventArgs e)
        {
            await LoadKeranjangData();
        }

        public async Task LoadKeranjangData()
        {
            try
            {
                // Show loading state
                SetLoadingState(true);

                // Load data from API
                currentKeranjang = await KeranjangService.GetKeranjangAsync();

                // Debug keranjang structure untuk troubleshooting
                DebugKeranjangStructure();

                // Update items in cart list untuk referensi
                UpdateItemsInCartList();

                // Display data
                DisplayKeranjangItems();

                // Update UI
                UpdateCheckoutButton();
                UpdateHeaderLabel();

                Console.WriteLine($"LoadKeranjangData completed. Items: {currentKeranjang?.Items?.Count ?? 0}");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error loading keranjang data: {ex.Message}";
                Console.WriteLine(errorMsg);
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnError?.Invoke(this, errorMsg);

                ShowEmptyState("Gagal memuat data keranjang");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void DebugKeranjangStructure()
        {
            try
            {
                Console.WriteLine("=== DEBUG KERANJANG STRUCTURE ===");

                if (currentKeranjang == null)
                {
                    Console.WriteLine("currentKeranjang is NULL");
                    return;
                }

                Console.WriteLine($"Keranjang Type: {currentKeranjang.GetType().Name}");

                var properties = currentKeranjang.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    try
                    {
                        var value = prop.GetValue(currentKeranjang);
                        Console.WriteLine($"  - {prop.Name}: {value} (Type: {prop.PropertyType.Name})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  - {prop.Name}: Error reading - {ex.Message}");
                    }
                }

                if (currentKeranjang.Items != null)
                {
                    Console.WriteLine($"\nItems Count: {currentKeranjang.Items.Count}");

                    for (int i = 0; i < Math.Min(currentKeranjang.Items.Count, 3); i++)
                    {
                        var item = currentKeranjang.Items[i];
                        Console.WriteLine($"\nItem {i} Type: {item.GetType().Name}");

                        var itemProperties = item.GetType().GetProperties();
                        foreach (var prop in itemProperties)
                        {
                            try
                            {
                                var value = prop.GetValue(item);
                                Console.WriteLine($"  - {prop.Name}: {value}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"  - {prop.Name}: Error reading - {ex.Message}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Items is NULL");
                }

                Console.WriteLine("=== END DEBUG ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug error: {ex.Message}");
            }
        }

        private void UpdateItemsInCartList()
        {
            itemsInCart.Clear();

            if (currentKeranjang?.Items != null)
            {
                foreach (var item in currentKeranjang.Items)
                {
                    string kodePakaian = GetKodePakaianFromItem(item);
                    if (!string.IsNullOrEmpty(kodePakaian))
                    {
                        itemsInCart.Add(kodePakaian);
                    }
                }
            }
        }

        private string GetKodePakaianFromItem(KeranjangItemDto item)
        {
            try
            {
                // Sesuai dengan struktur KeranjangItemDto yang Anda berikan
                if (item.Pakaian?.Kode != null)
                    return item.Pakaian.Kode;

                // Fallback menggunakan PakaianId
                return item.PakaianId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting kode pakaian: {ex.Message}");
                return string.Empty;
            }
        }

        // Method public untuk mengecek apakah item ada di keranjang
        public bool IsItemInCart(string kodePakaian)
        {
            return itemsInCart.Contains(kodePakaian);
        }

        // Method public untuk mendapatkan list item di keranjang
        public List<string> GetItemsInCart()
        {
            return new List<string>(itemsInCart);
        }

        // Method public untuk mendapatkan jumlah item di keranjang
        public int GetCartItemCount()
        {
            return currentKeranjang?.Items?.Count ?? 0;
        }

        // Method public untuk mendapatkan total harga
        public decimal GetCartTotal()
        {
            return CalculateTotal();
        }

        private void DisplayKeranjangItems()
        {
            Console.WriteLine("=== DisplayKeranjangItems START ===");

            flowLayoutPanel1.Controls.Clear();

            if (currentKeranjang?.Items == null || currentKeranjang.Items.Count == 0)
            {
                Console.WriteLine("No items to display - showing empty state");
                ShowEmptyState("Keranjang masih kosong");
                return;
            }

            Console.WriteLine($"Displaying {currentKeranjang.Items.Count} items in cart");

            for (int i = 0; i < currentKeranjang.Items.Count; i++)
            {
                var item = currentKeranjang.Items[i];

                // Debug log
                Console.WriteLine($"Creating panel for item {i}: {item?.Pakaian?.Nama ?? "Unknown"}");
                Console.WriteLine($"  - Quantity: {item?.Quantity ?? 0}");
                Console.WriteLine($"  - TotalHarga: {item?.TotalHarga ?? 0}");

                var keranjangPanel = new listKeranjangPanel();

                // Set data ke panel
                keranjangPanel.SetCartItemData(item, i);

                // Subscribe ke events
                keranjangPanel.OnRemoveClicked += KeranjangPanel_OnRemoveClicked;
                keranjangPanel.OnDataChanged += KeranjangPanel_OnDataChanged;

                // Make sure panel is visible
                keranjangPanel.Visible = true;
                keranjangPanel.Size = new Size(907, 91); // Sesuaikan dengan ukuran dari designer
                keranjangPanel.Dock = DockStyle.Top; // Atau sesuaikan layout
                keranjangPanel.Margin = new Padding(5);

                flowLayoutPanel1.Controls.Add(keranjangPanel);

                Console.WriteLine($"Panel {i} added to flowLayoutPanel1 - Controls count: {flowLayoutPanel1.Controls.Count}");
            }

            // Force refresh layout
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel1.Refresh();
            this.Refresh();

            Console.WriteLine($"=== DisplayKeranjangItems END - Total panels: {flowLayoutPanel1.Controls.Count} ===");
        }

        private async void KeranjangPanel_OnRemoveClicked(object sender, int itemIndex)
        {
            // Refresh keranjang setelah item dihapus
            await LoadKeranjangData();
            OnKeranjangChanged?.Invoke();
        }

        private void KeranjangPanel_OnDataChanged(object sender, EventArgs e)
        {
            // Handle perubahan data dari panel item
            OnKeranjangChanged?.Invoke();
        }

        private void ShowEmptyState(string message)
        {
            flowLayoutPanel1.Controls.Clear();

            var emptyPanel = new System.Windows.Forms.Panel();
            emptyPanel.Size = new Size(flowLayoutPanel1.Width - 20, 200);
            emptyPanel.BackColor = Color.White;

            var emptyLabel = new Label();
            emptyLabel.Text = message;
            emptyLabel.Size = new Size(400, 50);
            emptyLabel.Location = new Point((emptyPanel.Width - 400) / 2, 75);
            emptyLabel.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Add shopping cart icon if empty
            var iconLabel = new Label();
            iconLabel.Text = "🛒";
            iconLabel.Font = new Font("Segoe UI", 24);
            iconLabel.Size = new Size(50, 50);
            iconLabel.Location = new Point((emptyPanel.Width - 50) / 2, 25);
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;
            iconLabel.ForeColor = Color.LightGray;

            emptyPanel.Controls.Add(iconLabel);
            emptyPanel.Controls.Add(emptyLabel);
            flowLayoutPanel1.Controls.Add(emptyPanel);
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;

            if (loading)
            {
                flowLayoutPanel1.Controls.Clear();

                var loadingPanel = new System.Windows.Forms.Panel();
                loadingPanel.Size = new Size(flowLayoutPanel1.Width - 20, 100);
                loadingPanel.BackColor = Color.White;

                var loadingLabel = new Label();
                loadingLabel.Text = "Loading keranjang...";
                loadingLabel.Size = new Size(200, 50);
                loadingLabel.Location = new Point((loadingPanel.Width - 200) / 2, 25);
                loadingLabel.Font = new Font("Segoe UI", 12);
                loadingLabel.ForeColor = Color.Blue;
                loadingLabel.TextAlign = ContentAlignment.MiddleCenter;

                loadingPanel.Controls.Add(loadingLabel);
                flowLayoutPanel1.Controls.Add(loadingPanel);
            }

            guna2GradientButton1.Enabled = !loading;
        }

        private void UpdateCheckoutButton()
        {
            if (currentKeranjang?.Items != null && currentKeranjang.Items.Count > 0)
            {
                guna2GradientButton1.Enabled = true;

                try
                {
                    // Gunakan TotalHarga dari KeranjangDto
                    decimal total = currentKeranjang.TotalHarga;
                    guna2GradientButton1.Text = $"Checkout ({total:C})";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating checkout button: {ex.Message}");
                    guna2GradientButton1.Text = "Checkout";
                }
            }
            else
            {
                guna2GradientButton1.Enabled = false;
                guna2GradientButton1.Text = "Checkout";
            }
        }

        private decimal CalculateTotal()
        {
            if (currentKeranjang?.Items == null) return 0;

            // Gunakan TotalHarga dari KeranjangDto jika ada
            if (currentKeranjang.TotalHarga > 0)
            {
                return currentKeranjang.TotalHarga;
            }

            // Fallback: hitung manual dari items
            decimal total = 0;
            foreach (var item in currentKeranjang.Items)
            {
                try
                {
                    total += item.TotalHarga;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating item total: {ex.Message}");

                    // Fallback: calculate from harga * quantity
                    try
                    {
                        if (item.Pakaian != null)
                        {
                            total += item.Pakaian.Harga * item.Quantity;
                        }
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine($"Fallback calculation failed: {ex2.Message}");
                    }
                }
            }
            return total;
        }

        private void UpdateHeaderLabel()
        {
            if (currentKeranjang?.Items != null)
            {
                int itemCount = currentKeranjang.JumlahItem;
                if (itemCount > 0)
                {
                    decimal total = currentKeranjang.TotalHarga;
                    labelListPakaian.Text = $"Keranjang ({itemCount} items - {total:C})";
                }
                else
                {
                    labelListPakaian.Text = "Keranjang Kosong";
                }
            }
            else
            {
                labelListPakaian.Text = "Keranjang";
            }
        }

        private async void CheckoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentKeranjang?.Items == null || currentKeranjang.Items.Count == 0)
                {
                    MessageBox.Show("Keranjang masih kosong!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Konfirmasi checkout
                decimal total = currentKeranjang.TotalHarga;
                int itemCount = currentKeranjang.JumlahItem;

                var result = MessageBox.Show(
                    $"Checkout {itemCount} item dengan total {total:C}?",
                    "Konfirmasi Checkout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Set loading state
                guna2GradientButton1.Enabled = false;
                guna2GradientButton1.Text = "Processing...";

                // Create checkout request sesuai model yang ada
                var checkoutRequest = new CheckoutDto
                {
                    AlamatPengiriman = "Alamat Default", // Bisa diambil dari form input
                    MetodePembayaran = "Transfer Bank"    // Bisa diambil dari dropdown
                };

                // Call checkout API
                var checkoutResponse = await KeranjangService.CheckoutAsync(checkoutRequest);

                if (checkoutResponse != null)
                {
                    string successMsg = $"Checkout berhasil!\n" +
                                       $"Order ID: {checkoutResponse.OrderId}\n" +
                                       $"Total: {checkoutResponse.TotalHarga:C}\n" +
                                       $"Status: {checkoutResponse.StatusPemesanan}";

                    MessageBox.Show(successMsg, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh keranjang
                    await LoadKeranjangData();

                    // Trigger event untuk update UI lain
                    OnKeranjangChanged?.Invoke();
                }
                else
                {
                    MessageBox.Show("Checkout gagal. Silakan coba lagi.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error during checkout: {ex.Message}";
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnError?.Invoke(this, errorMsg);
            }
            finally
            {
                UpdateCheckoutButton();
            }
        }

        // Method untuk refresh dari external - IMPROVED
        public async void RefreshKeranjang()
        {
            if (!isLoading)
            {
                Console.WriteLine("RefreshKeranjang called - reloading data...");
                await LoadKeranjangData();
            }
            else
            {
                Console.WriteLine("RefreshKeranjang skipped - already loading");
            }
        }

        // Method untuk force refresh - untuk dipanggil setelah add to cart
        public async Task ForceRefreshKeranjang()
        {
            Console.WriteLine("ForceRefreshKeranjang called - forcing reload...");
            isLoading = false; // Reset loading state
            await LoadKeranjangData();
        }

        // Method untuk clear cart
        public async Task ClearCartAsync()
        {
            try
            {
                var result = MessageBox.Show("Kosongkan seluruh keranjang?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SetLoadingState(true);

                    await KeranjangService.ClearCartAsync();
                    await LoadKeranjangData();
                    OnKeranjangChanged?.Invoke();

                    MessageBox.Show("Keranjang berhasil dikosongkan.", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error clearing cart: {ex.Message}";
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnError?.Invoke(this, errorMsg);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        // Method untuk check apakah sedang loading
        public bool IsLoading()
        {
            return isLoading;
        }

        // Method public untuk debugging - akses ke currentKeranjang
        public KeranjangDto GetCurrentKeranjang()
        {
            return currentKeranjang;
        }

        // Method public untuk debugging - akses ke flowLayoutPanel1
        public FlowLayoutPanel GetFlowLayoutPanel()
        {
            return flowLayoutPanel1;
        }

        // Method untuk set dummy data (untuk testing)
        public void SetDummyData()
        {
            currentKeranjang = new KeranjangDto
            {
                TotalHarga = 50000,
                JumlahItem = 2,
                Items = new List<KeranjangItemDto>
                {
                    new KeranjangItemDto
                    {
                        Id = 1,
                        UserId = 1,
                        PakaianId = 1,
                        Quantity = 2,
                        TotalHarga = 30000,
                        CreatedAt = DateTime.Now,
                        Pakaian = new PakaianDto
                        {
                            Kode = "TSB001",
                            Nama = "T Shirt Black",
                            Kategori = "Kaos",
                            Warna = "Black",
                            Ukuran = "XXL",
                            Harga = 15000,
                            Stok = 10,
                            Status = StatusPakaian.DalamKeranjang
                        }
                    },
                    new KeranjangItemDto
                    {
                        Id = 2,
                        UserId = 1,
                        PakaianId = 2,
                        Quantity = 1,
                        TotalHarga = 20000,
                        CreatedAt = DateTime.Now,
                        Pakaian = new PakaianDto
                        {
                            Kode = "KFP001",
                            Nama = "Kemeja Formal Pria",
                            Kategori = "Kemeja",
                            Warna = "Blue",
                            Ukuran = "L",
                            Harga = 20000,
                            Stok = 5,
                            Status = StatusPakaian.DalamKeranjang
                        }
                    }
                }
            };

            DisplayKeranjangItems();
            UpdateCheckoutButton();
            UpdateHeaderLabel();

            Console.WriteLine("Dummy data set with 2 items");
        }

        // Tambahkan method debug ini di lihatKeranjangPanel.cs

        // Tambahkan method ini di dalam kelas lihatKeranjangPanel

        public async void TestKeranjangConnection()
        {
            try
            {
                Console.WriteLine("=== TESTING KERANJANG CONNECTION ===");

                // Test 1: Direct API call
                var apiData = await KeranjangService.GetKeranjangAsync();

                string apiResult = $"API Test Result:\n" +
                                  $"- Data is null: {apiData == null}\n" +
                                  $"- TotalHarga: {apiData?.TotalHarga ?? 0}\n" +
                                  $"- JumlahItem: {apiData?.JumlahItem ?? 0}\n" +
                                  $"- Items Count: {apiData?.Items?.Count ?? 0}";

                Console.WriteLine(apiResult);
                MessageBox.Show(apiResult, "API Test Result");

                // Test 2: Check if items exist
                if (apiData?.Items != null && apiData.Items.Count > 0)
                {
                    string itemsInfo = "Items found:\n";
                    for (int i = 0; i < Math.Min(apiData.Items.Count, 3); i++)
                    {
                        var item = apiData.Items[i];
                        itemsInfo += $"Item {i}: {item?.Pakaian?.Nama ?? "No Name"} - Qty: {item?.Quantity ?? 0}\n";
                    }

                    Console.WriteLine(itemsInfo);
                    MessageBox.Show(itemsInfo, "Items Test Result");

                    // Test 3: Try to display simple version
                    TestSimpleDisplay(apiData);
                }
                else
                {
                    MessageBox.Show("No items found in API response", "Items Test Result");
                }
            }
            catch (Exception ex)
            {
                string error = $"Test Error: {ex.Message}";
                Console.WriteLine(error);
                MessageBox.Show(error, "Test Error");
            }
        }

        private void TestSimpleDisplay(KeranjangDto keranjang)
        {
            try
            {
                Console.WriteLine("=== TESTING SIMPLE DISPLAY ===");

                // Clear flowLayoutPanel1
                flowLayoutPanel1.Controls.Clear();

                if (keranjang?.Items == null || keranjang.Items.Count == 0)
                {
                    MessageBox.Show("No items to display", "Display Test");
                    return;
                }

                // Add simple labels instead of complex panels
                for (int i = 0; i < keranjang.Items.Count; i++)
                {
                    var item = keranjang.Items[i];

                    // Create simple label
                    var itemLabel = new System.Windows.Forms.Label();
                    itemLabel.Text = $"Item {i + 1}: {item?.Pakaian?.Nama ?? "Unknown"} - Qty: {item?.Quantity ?? 0} - Total: Rp {item?.TotalHarga ?? 0:N0}";
                    itemLabel.Size = new Size(flowLayoutPanel1.Width - 20, 30);
                    itemLabel.BackColor = Color.LightGreen;
                    itemLabel.BorderStyle = BorderStyle.FixedSingle;
                    itemLabel.TextAlign = ContentAlignment.MiddleLeft;
                    itemLabel.Margin = new Padding(5);

                    flowLayoutPanel1.Controls.Add(itemLabel);

                    Console.WriteLine($"Added simple label for item {i}");
                }

                // Force refresh
                flowLayoutPanel1.Refresh();
                this.Refresh();

                MessageBox.Show($"Simple display completed. Added {keranjang.Items.Count} labels.", "Display Test Result");
            }
            catch (Exception ex)
            {
                string error = $"Display Test Error: {ex.Message}";
                Console.WriteLine(error);
                MessageBox.Show(error, "Display Test Error");
            }
        }

        // Tambahkan button test sementara
        private void AddTemporaryTestButton()
        {
            var testBtn = new Guna.UI2.WinForms.Guna2Button();
            testBtn.Text = "TEST API";
            testBtn.Size = new Size(100, 30);
            testBtn.Location = new Point(10, 10);
            testBtn.FillColor = Color.Orange;
            testBtn.Click += (s, e) => TestKeranjangConnection();

            this.Controls.Add(testBtn);
            testBtn.BringToFront();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
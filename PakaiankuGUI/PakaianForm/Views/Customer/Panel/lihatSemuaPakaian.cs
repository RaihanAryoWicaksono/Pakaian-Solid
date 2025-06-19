// PakaianForm/Views/Customer/Panel/lihatSemuaPakaian.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk PakaianDtos
using PakaianForm.Services; // Untuk ApiClient, KatalogService, KeranjangService, UserSession
using PakaianLib; // Penting untuk StatusPakaian, AksiPakaian
using Guna.UI2.WinForms; // Tambahkan ini untuk akses komponen Guna.UI2

namespace PakaianForm.Views.Customer.Panel
{
    public partial class lihatSemuaPakaian : UserControl
    {
        // Event untuk memberi tahu parent (CustomerDashboard) bahwa keranjang perlu di-refresh
        public event EventHandler OnCartDataChanged; // <--- EVENT BARU

        // Deklarasi Variabel
        private List<PakaianDtos> _allPakaian = new List<PakaianDtos>();
        private System.Windows.Forms.Timer _searchTimer;

        // Kontrol dari Designer.cs (jika ada, seperti tbSearchPakaian, button1, labelListPakaian)
        // private Guna.UI2.WinForms.Guna2TextBox tbSearchPakaian;
        // private System.Windows.Forms.Button button1;
        // private System.Windows.Forms.Label labelListPakaian;
        // private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelListPakaian;


        public lihatSemuaPakaian()
        {
            InitializeComponent();
            InitializeComponents();
            this.Load += LihatSemuaPakaian_Load;
        }

        private async void LihatSemuaPakaian_Load(object sender, EventArgs e)
        {
            await LoadPakaianData();
        }

        private void InitializeComponents()
        {
            // Konfigurasi FlowLayoutPanel
            if (flowLayoutPanelListPakaian != null)
            {
                flowLayoutPanelListPakaian.AutoScroll = true;
                flowLayoutPanelListPakaian.WrapContents = true;
                flowLayoutPanelListPakaian.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanelListPakaian.AutoScrollMargin = new Size(0, 50); // Margin bawah untuk scroll
            }

            // Inisialisasi Timer Pencarian
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 500;
            _searchTimer.Tick += SearchTimer_Tick;

            // Hubungkan event handlers untuk pencarian (pastikan kontrol ada di Designer.cs)
            if (tbSearchPakaian != null) tbSearchPakaian.PlaceholderText = "Cari pakaian...";
            if (tbSearchPakaian != null) tbSearchPakaian.TextChanged += tbSearchPakaian_TextChanged;
            if (button1 != null) button1.Click += button1_Click;
        }

        private void ClearStaticPanels()
        {
            if (flowLayoutPanelListPakaian == null) return;
            var panelsToRemove = flowLayoutPanelListPakaian.Controls
                .OfType<listPakaianPanel>()
                .ToList();

            foreach (var panel in panelsToRemove)
            {
                flowLayoutPanelListPakaian.Controls.Remove(panel);
                panel.Dispose();
            }
        }

        public async Task LoadPakaianData()
        {
            if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.SuspendLayout();
            try
            {
                SetLoadingState(true);
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Controls.Clear();

                _allPakaian = await KatalogService.GetAllPakaianAsync(); // Memanggil API Katalog
                DisplayPakaian(_allPakaian);

                // Pastikan labelListPakaian ada di desainer
                // if (labelListPakaian != null) labelListPakaian.Text = $"List Pakaian ({_allPakaian.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pakaian data:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowEmptyState("Gagal memuat data pakaian");
            }
            finally
            {
                SetLoadingState(false);
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.ResumeLayout(true);
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Invalidate();
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Update();
            }
        }

        private void DisplayPakaian(List<PakaianDtos> pakaianList)
        {
            if (flowLayoutPanelListPakaian == null) return;
            flowLayoutPanelListPakaian.Controls.Clear();

            if (pakaianList == null || !pakaianList.Any())
            {
                ShowEmptyState("Tidak ada pakaian yang ditemukan");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                listPakaianPanel itemPanel = new listPakaianPanel();
                itemPanel.Pakaian = pakaian;

                itemPanel.OnAddToCartClicked += ItemPanel_OnAddToCartClicked;
                itemPanel.OnDataChanged += ItemPanel_OnDataChanged; // Notifikasi jika data itemPanel berubah

                itemPanel.Margin = new Padding(10);
                flowLayoutPanelListPakaian.Controls.Add(itemPanel);
            }
        }

        private async void ItemPanel_OnAddToCartClicked(object sender, PakaianDtos pakaian)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Anda harus login untuk menambahkan item ke keranjang.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (UserSession.UserId == 0)
            {
                MessageBox.Show("User ID tidak tersedia. Harap login kembali.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guna.UI2.WinForms.Guna2GradientButton addToCartBtn = sender as Guna.UI2.WinForms.Guna2GradientButton;
            if (addToCartBtn == null) return;

            string originalText = addToCartBtn.Text;
            bool originalEnabled = addToCartBtn.Enabled;

            addToCartBtn.Enabled = false;
            addToCartBtn.Text = "Adding...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                AddToCartDto addToCartRequest = new AddToCartDto { KodePakaian = pakaian.Kode };
                await KeranjangService.AddToKeranjangAsync(addToCartRequest);

                MessageBox.Show($"'{pakaian.Nama}' berhasil ditambahkan ke keranjang!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- PENTING: PICU EVENT UNTUK MEMBERI TAHU DASHBOARD ---
                OnCartDataChanged?.Invoke(this, EventArgs.Empty); // <--- PICU EVENT INI
                // --- AKHIR PENTING ---

                await LoadPakaianData(); // Refresh data di list pakaian
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan ke keranjang:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                addToCartBtn.Enabled = originalEnabled;
                addToCartBtn.Text = originalText;
                this.Cursor = Cursors.Default;
            }
        }

        private async void ItemPanel_OnDataChanged(object sender, EventArgs e)
        {
            await LoadPakaianData();
        }

        private void ShowEmptyState(string message)
        {
            if (flowLayoutPanelListPakaian == null) return;
            flowLayoutPanelListPakaian.Controls.Clear();
            Label emptyLabel = new Label();
            emptyLabel.Text = message;
            emptyLabel.AutoSize = true;
            emptyLabel.Font = new Font("Segoe UI", 14);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.Location = new Point((flowLayoutPanelListPakaian.Width - emptyLabel.Width) / 2, (flowLayoutPanelListPakaian.Height - emptyLabel.Height) / 2);
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            flowLayoutPanelListPakaian.Controls.Add(emptyLabel);
        }

        private void SetLoadingState(bool loading)
        {
            if (flowLayoutPanelListPakaian == null) return;

            if (loading)
            {
                flowLayoutPanelListPakaian.Controls.Clear();
                var loadingLabel = new Label();
                loadingLabel.Text = "Loading...";
                loadingLabel.Size = new Size(200, 50);
                loadingLabel.Location = new Point((flowLayoutPanelListPakaian.Width - loadingLabel.Width) / 2, (flowLayoutPanelListPakaian.Height - loadingLabel.Height) / 2);
                loadingLabel.Font = new Font("Segoe UI", 14);
                loadingLabel.ForeColor = Color.Blue;
                loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
                flowLayoutPanelListPakaian.Controls.Add(loadingLabel);
            }

            if (tbSearchPakaian != null) tbSearchPakaian.Enabled = !loading;
            if (button1 != null) button1.Enabled = !loading;
            this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }

        private void RefreshData()
        {
            LoadPakaianData();
        }

        // Search functionality
        private void tbSearchPakaian_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            await PerformSearch();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }

        private async Task PerformSearch()
        {
            if (tbSearchPakaian == null) return;

            flowLayoutPanelListPakaian.SuspendLayout();
            try
            {
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Controls.Clear();

                string searchTerm = tbSearchPakaian.Text.Trim();
                List<PakaianDtos> searchResults;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchResults = new List<PakaianDtos>(_allPakaian);
                }
                else
                {
                    searchResults = await KatalogService.SearchPakaianAsync(searchTerm);
                }

                DisplayPakaian(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mencari pakaian: {ex.Message}", "Kesalahan Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.ResumeLayout(true);
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Invalidate();
                if (flowLayoutPanelListPakaian != null) flowLayoutPanelListPakaian.Update();
            }
        }

        // Designer generated event handlers (biarkan kosong jika tidak ada logika khusus)
        private void flowLayoutPanelListPakaian_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void labelJudulListPakaian_Click(object sender, EventArgs e) { }
    }
}

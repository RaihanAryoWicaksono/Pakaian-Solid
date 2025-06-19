// PakaianForm/Views/Admin/Panel/KelolaPakaian.cs
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
using PakaianForm.Services; // Untuk ApiClient, KatalogService
using PakaianLib; // Penting untuk StatusPakaian, AksiPakaian
using Guna.UI2.WinForms; // Tambahkan ini untuk akses komponen Guna.UI2

namespace PakaianForm.Views.Admin.Panel
{
    public partial class KelolaPakaian : UserControl
    {
        public event Action<UserControl> OnNavigateToPanel;

        private List<PakaianDtos> _allPakaian = new List<PakaianDtos>();
        private System.Windows.Forms.Timer _searchTimer;

        public KelolaPakaian()
        {
            InitializeComponent();

            flowLayoutPanelBarang.AutoScroll = true;
            flowLayoutPanelBarang.WrapContents = true;
            flowLayoutPanelBarang.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelBarang.AutoScrollMargin = new Size(0, 50);

            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 500;
            _searchTimer.Tick += SearchTimer_Tick;

            if (tbSearchPakaian != null) tbSearchPakaian.TextChanged += tbSearchPakaian_TextChanged;
            if (button1 != null) button1.Click += button1_Click;

            this.Load += KelolaPakaian_Load;
        }

        private async void KelolaPakaian_Load(object sender, EventArgs e)
        {
            await LoadPakaianData();
        }

        public async Task LoadPakaianData()
        {
            flowLayoutPanelBarang.SuspendLayout();
            try
            {
                SetLoadingState(true);
                flowLayoutPanelBarang.Controls.Clear();

                _allPakaian = await KatalogService.GetAllPakaianAsync();

                DisplayPakaian(_allPakaian);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memuat data pakaian: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowEmptyState("Gagal memuat data pakaian");
            }
            finally
            {
                SetLoadingState(false);
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.ResumeLayout(true);
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.Invalidate();
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.Update();
            }
        }

        private void DisplayPakaian(List<PakaianDtos> pakaianList)
        {
            if (flowLayoutPanelBarang == null) return;
            flowLayoutPanelBarang.Controls.Clear();

            if (pakaianList == null || !pakaianList.Any())
            {
                ShowEmptyState("Tidak ada pakaian ditemukan.");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                editItemPanel itemPanel = new editItemPanel();
                itemPanel.Pakaian = pakaian;

                itemPanel.OnEditClicked += ItemPanel_OnEditClicked;
                itemPanel.OnDeleteClicked += ItemPanel_OnDeleteClicked;

                itemPanel.Margin = new Padding(10);

                flowLayoutPanelBarang.Controls.Add(itemPanel);
            }
        }

        private void ItemPanel_OnEditClicked(object sender, PakaianDtos pakaianToEdit)
        {
            panelEditPakaian editPanel = new panelEditPakaian(pakaianToEdit);
            editPanel.OnPakaianUpdated += async (updatedPakaian) => {
                MessageBox.Show($"Pakaian '{updatedPakaian.Nama}' berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPakaianData();
            };
            OnNavigateToPanel?.Invoke(editPanel);
        }

        private async void ItemPanel_OnDeleteClicked(object sender, PakaianDtos pakaianToDelete)
        {
            DialogResult confirmResult = MessageBox.Show($"Apakah Anda yakin ingin menghapus pakaian '{pakaianToDelete.Nama}' (Kode: {pakaianToDelete.Kode})?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await KatalogService.DeletePakaianAsync(pakaianToDelete.Kode);
                    MessageBox.Show($"Pakaian '{pakaianToDelete.Nama}' berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPakaianData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus pakaian: {ex.Message}", "Kesalahan Hapus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowEmptyState(string message)
        {
            if (flowLayoutPanelBarang == null) return;
            flowLayoutPanelBarang.Controls.Clear();
            Label emptyLabel = new Label();
            emptyLabel.Text = message;
            emptyLabel.AutoSize = true;
            emptyLabel.Font = new Font("Segoe UI", 14);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.Location = new Point((flowLayoutPanelBarang.Width - emptyLabel.Width) / 2, (flowLayoutPanelBarang.Height - emptyLabel.Height) / 2);
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            flowLayoutPanelBarang.Controls.Add(emptyLabel);
        }

        private void SetLoadingState(bool loading)
        {
            if (flowLayoutPanelBarang == null) return;

            if (loading)
            {
                flowLayoutPanelBarang.Controls.Clear();
                var loadingLabel = new Label();
                loadingLabel.Text = "Loading...";
                loadingLabel.Size = new Size(200, 50);
                loadingLabel.Location = new Point((flowLayoutPanelBarang.Width - loadingLabel.Width) / 2, (flowLayoutPanelBarang.Height - loadingLabel.Height) / 2); // Corrected typo
                loadingLabel.Font = new Font("Segoe UI", 14);
                loadingLabel.ForeColor = Color.Blue;
                loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
                flowLayoutPanelBarang.Controls.Add(loadingLabel);
            }

            if (tbSearchPakaian != null) tbSearchPakaian.Enabled = !loading;
            if (button1 != null) button1.Enabled = !loading;
            this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }

        private void RefreshData()
        {
            LoadPakaianData();
        }

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

            flowLayoutPanelBarang.SuspendLayout();
            try
            {
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.Controls.Clear();

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
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.ResumeLayout(true);
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.Invalidate();
                if (flowLayoutPanelBarang != null) flowLayoutPanelBarang.Update();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void panelItem1_Load(object sender, EventArgs e) { }
        private void editItemPanel_Load(object sender, EventArgs e) { }
        private void editItemPanel1_Load(object sender, EventArgs e) { }
        private void editItemPanel2_Load(object sender, EventArgs e) { }
        private void editItemPanel3_Load(object sender, EventArgs e) { }
        private void editItemPanel4_Load(object sender, EventArgs e) { }
        private void editItemPanel5_Load(object sender, EventArgs e) { }
        private void editItemPanel6_Load(object sender, EventArgs e) { }
    }
}

using PakaianForm.Panel;
using PakaianForm.Form.Admin.Katalog;
using System;
using System.Windows.Forms;

namespace PakaianForm.Form.Admin
{
    public partial class KelolaKatalog : UserControl
    {
        public KelolaKatalog()
        {
            InitializeComponent();
            flowLayoutPanelBarang.AutoScroll = true;
            flowLayoutPanelBarang.WrapContents = true;
            // Jangan setup edit buttons di constructor karena parent belum ready
        }

        // Method untuk setup edit buttons setelah control di-load ke parent
        private void SetupEditButtons()
        {
            // Cari semall tombol Edit dalam form dan tambahkan event handler
            foreach (Control control in this.Controls)
            {
                if (control is Button && (control.Name.ToLower().Contains("edit") || control.Text.ToLower() == "edit"))
                {
                    Button editBtn = (Button)control;
                    editBtn.Click -= EditButton_Click; // Remove existing handler
                    editBtn.Click += EditButton_Click;
                }

                // Cari dalam nested controls
                FindAndSetupEditButtons(control);
            }
        }

        private void FindAndSetupEditButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button && (control.Name.ToLower().Contains("edit") || control.Text.ToLower() == "edit"))
                {
                    Button editBtn = (Button)control;
                    editBtn.Click -= EditButton_Click;
                    editBtn.Click += EditButton_Click;
                }

                if (control.HasChildren)
                {
                    FindAndSetupEditButtons(control);
                }
            }
        }

        // Override method Load untuk setup buttons setelah control dimuat
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupEditButtons();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Cek apakah parent ada dan bukan null
                Control panelKontainer = this.Parent;

                if (panelKontainer == null)
                {
                    MessageBox.Show("Parent container tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Clear container dan buat editPakaianPanel baru
                panelKontainer.Controls.Clear();

                editPakaianPanel editPakaian = new editPakaianPanel();
                editPakaian.Dock = DockStyle.Fill;

                panelKontainer.Controls.Add(editPakaian);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat berpindah ke halaman edit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Alternative method jika Anda ingin menggunakan event-based navigation
        public event Action<UserControl> OnNavigateToPanel;

        private void EditButton_Click_Alternative(object sender, EventArgs e)
        {
            try
            {
                editPakaianPanel editPanel = new editPakaianPanel();
                OnNavigateToPanel?.Invoke(editPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Existing event handlers
        private void panelItem2_Load(object sender, EventArgs e)
        {
        }

        private void flowLayoutPanelBarang_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelItem3_Load(object sender, EventArgs e)
        {
        }

        private void panelItem2_Load_1(object sender, EventArgs e)
        {
        }

        private void panelItemEdit1_Load(object sender, EventArgs e)
        {
            // Setup edit buttons ketika panel item di-load
            SetupEditButtons();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void flowLayoutPanelBarang_Paint_1(object sender, PaintEventArgs e)
        {
        }
    }
}
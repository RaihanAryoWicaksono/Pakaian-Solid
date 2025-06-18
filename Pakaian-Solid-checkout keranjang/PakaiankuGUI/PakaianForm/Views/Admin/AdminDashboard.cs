using PakaianForm.Views.Admin.Panel;
using PakaianForm.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PakaianForm.Views.Admin
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            InitializeAdminDashboard();
        }

        private void InitializeAdminDashboard()
        {
            // Update user info labels
            UpdateUserInfo();

            // Add event handlers yang belum ada
            btnLogout.Click += BtnLogout_Click;
            btnKembalikeLogin.Click += BtnKembalikeLogin_Click;

            // Set default panel (KelolaPakaian)
            ShowDefaultPanel();

            // Set active button style
            SetActiveButton(btnAdminLihatSemuaPakaian);
        }

        private void UpdateUserInfo()
        {
            // Update label dengan info user yang login
            labelTokoPakaian.Text = "Toko Pakaian";
            label1.Text = $"Admin - {UserSession.CurrentUser}";
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);
        }

        private void ShowDefaultPanel()
        {
            // Default tampilkan panel KelolaPakaian
            panelKontainer.Controls.Clear();
            KelolaPakaian kelolaPakaian = new KelolaPakaian();
            kelolaPakaian.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kelolaPakaian);
        }

        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            try
            {
                panelKontainer.Controls.Clear();
                KelolaPakaian kelolaPakaian = new KelolaPakaian();
                kelolaPakaian.Dock = DockStyle.Fill;
                panelKontainer.Controls.Add(kelolaPakaian);

                // Set button aktif
                SetActiveButton(btnAdminLihatSemuaPakaian);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading panel: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            try
            {
                panelKontainer.Controls.Clear();

                // Gunakan reflection untuk mencari panel edit yang tersedia
                Control editPanel = FindEditPanel();

                if (editPanel != null)
                {
                    editPanel.Dock = DockStyle.Fill;
                    panelKontainer.Controls.Add(editPanel);
                    MessageBox.Show("Panel Edit Pakaian berhasil dimuat!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Fallback: Gunakan KelolaPakaian 
                    KelolaPakaian kelolaPakaian = new KelolaPakaian();
                    kelolaPakaian.Dock = DockStyle.Fill;
                    panelKontainer.Controls.Add(kelolaPakaian);

                    MessageBox.Show("Panel Edit belum tersedia.\nSaat ini menampilkan panel Kelola Pakaian.\n\n" +
                                   "Panel edit akan dikembangkan di versi selanjutnya.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Set button aktif
                SetActiveButton(btnKelolaKatalogPakaian);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nMenampilkan KelolaPakaian sebagai gantinya.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                KelolaPakaian kelolaPakaian = new KelolaPakaian();
                kelolaPakaian.Dock = DockStyle.Fill;
                panelKontainer.Controls.Add(kelolaPakaian);

                SetActiveButton(btnKelolaKatalogPakaian);
            }
        }

        private Control FindEditPanel()
        {
            try
            {
                // Cari semua panel yang mungkin ada
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();

                // Daftar nama panel yang mungkin ada
                string[] possibleNames = {
                    "editPakaianPanel",
                    "EditPakaianPanel",
                    "editItemPanel",
                    "EditItemPanel",
                    "PakaianEditPanel",
                    "ManagePakaianPanel"
                };

                foreach (var type in types)
                {
                    // Cek apakah nama class cocok dengan kemungkinan nama panel
                    foreach (var name in possibleNames)
                    {
                        if (type.Name == name && type.IsSubclassOf(typeof(Control)))
                        {
                            return (Control)Activator.CreateInstance(type);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error finding edit panel: {ex.Message}");
            }

            return null; // Tidak ditemukan
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Konfirmasi logout
            var result = MessageBox.Show("Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session
                AuthService.Logout();

                // Tampilkan pesan logout
                MessageBox.Show("Logout berhasil!", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kembali ke login form
                ShowLoginForm();

                // Tutup form admin dashboard
                this.Close();
            }
        }

        private void BtnKembalikeLogin_Click(object sender, EventArgs e)
        {
            // Konfirmasi kembali ke login
            var result = MessageBox.Show("Kembali ke halaman login?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session
                AuthService.Logout();

                // Kembali ke login form
                ShowLoginForm();

                // Tutup form admin dashboard
                this.Close();
            }
        }

        private void ShowLoginForm()
        {
            try
            {
                // Cari LoginForm menggunakan reflection
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();

                Form loginForm = null;

                foreach (var type in types)
                {
                    if (type.Name == "LoginForm" && type.IsSubclassOf(typeof(Form)))
                    {
                        loginForm = (Form)Activator.CreateInstance(type);
                        break;
                    }
                }

                if (loginForm != null)
                {
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Tidak dapat membuka LoginForm. Aplikasi akan ditutup.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membuka LoginForm: {ex.Message}\nAplikasi akan ditutup.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            // Close button - konfirmasi keluar
            var result = MessageBox.Show("Tutup aplikasi?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session dan keluar aplikasi
                AuthService.Logout();
                Application.Exit();
            }
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2GradientButton activeButton)
        {
            // Reset semua button ke warna normal
            ResetAllButtons();

            // Set button aktif dengan warna berbeda
            activeButton.FillColor = Color.FromArgb(255, 159, 67);
            activeButton.FillColor2 = Color.FromArgb(255, 107, 107);
        }

        private void ResetAllButtons()
        {
            // Warna normal untuk semua button
            var normalColor1 = Color.FromArgb(128, 128, 255);
            var normalColor2 = Color.FromArgb(94, 148, 255);

            btnAdminLihatSemuaPakaian.FillColor = normalColor1;
            btnAdminLihatSemuaPakaian.FillColor2 = normalColor2;

            btnKelolaKatalogPakaian.FillColor = normalColor1;
            btnKelolaKatalogPakaian.FillColor2 = normalColor2;

            btnKembalikeLogin.FillColor = normalColor1;
            btnKembalikeLogin.FillColor2 = normalColor2;
        }

        // Method untuk menampilkan panel custom (bisa dipanggil dari panel lain)
        public void ShowPanel(Control panel)
        {
            try
            {
                panelKontainer.Controls.Clear();
                panel.Dock = DockStyle.Fill;
                panelKontainer.Controls.Add(panel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing panel: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle form load
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Tampilkan welcome message
            ShowWelcomeMessage();
        }

        private void ShowWelcomeMessage()
        {
            MessageBox.Show($"Selamat datang di Admin Dashboard, {UserSession.CurrentUser}!\n\n" +
                           "Fitur yang tersedia:\n" +
                           "• Lihat Semua Pakaian - Kelola inventory produk\n" +
                           "• Kelola Katalog Pakaian - Edit dan tambah produk\n" +
                           "• Logout - Keluar dari sistem",
                           "Welcome Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Handle form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Jika masih login dan form ditutup, konfirmasi
            if (UserSession.IsLoggedIn)
            {
                var result = MessageBox.Show("Tutup aplikasi akan logout otomatis. Lanjutkan?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // Clear session jika user konfirmasi keluar
                AuthService.Logout();
            }

            base.OnFormClosing(e);
        }

        // Method untuk refresh dashboard (jika diperlukan)
        public void RefreshDashboard()
        {
            UpdateUserInfo();
            ShowDefaultPanel();
        }

        private void btnKembalikeLogin_Click_1(object sender, EventArgs e)
        {

        }
    }
}
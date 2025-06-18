using PakaianForm.Views.Customer.Panel;
using PakaianForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Views.Shared;

namespace PakaianForm.Views.Customer
{
    public partial class CustomerDashboard : Form
    {
        private lihatSemuaPakaian currentLihatPakaianPanel;
        private lihatKeranjangPanel currentKeranjangPanel;

        public CustomerDashboard()
        {
            InitializeComponent();
        }

        private void SetWelcomeMessage()
        {
            if (UserSession.IsLoggedIn)
            {
                label1.Text = $"Customer - {UserSession.CurrentUser}";
            }
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainerCustomer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainerCustomer.Controls.Add(kontrol);
        }

        private void btnCustomerLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear container
                panelKontainerCustomer.Controls.Clear();

                // Create or reuse panel
                if (currentLihatPakaianPanel == null)
                {
                    currentLihatPakaianPanel = new lihatSemuaPakaian();
                }

                // Set dock and add to container
                currentLihatPakaianPanel.Dock = DockStyle.Fill;
                panelKontainerCustomer.Controls.Add(currentLihatPakaianPanel);

                // Update button states
                UpdateButtonStates("pakaian");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pakaian panel: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomerLihatKeranjang_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear container
                panelKontainerCustomer.Controls.Clear();

                // Create new panel untuk refresh data terbaru
                currentKeranjangPanel = new lihatKeranjangPanel();

                // Set dock and add to container
                currentKeranjangPanel.Dock = DockStyle.Fill;
                panelKontainerCustomer.Controls.Add(currentKeranjangPanel);

                // Update button states
                UpdateButtonStates("keranjang");

                Console.WriteLine("[DASHBOARD] Keranjang panel loaded");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading keranjang panel: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"[DASHBOARD] Error: {ex.Message}");
            }
        }

        private void UpdateButtonStates(string activePanel)
        {
            // Reset all button colors
            btnCustomerLihatSemuaPakaian.FillColor = System.Drawing.Color.FromArgb(128, 128, 255);
            btnCustomerLihatSemuaPakaian.FillColor2 = System.Drawing.Color.FromArgb(94, 148, 255);

            btnCustomerLihatKeranjang.FillColor = System.Drawing.Color.FromArgb(128, 128, 255);
            btnCustomerLihatKeranjang.FillColor2 = System.Drawing.Color.FromArgb(94, 148, 255);

            // Highlight active button
            switch (activePanel)
            {
                case "pakaian":
                    btnCustomerLihatSemuaPakaian.FillColor = System.Drawing.Color.FromArgb(94, 148, 255);
                    btnCustomerLihatSemuaPakaian.FillColor2 = System.Drawing.Color.FromArgb(128, 128, 255);
                    break;
                case "keranjang":
                    btnCustomerLihatKeranjang.FillColor = System.Drawing.Color.FromArgb(94, 148, 255);
                    btnCustomerLihatKeranjang.FillColor2 = System.Drawing.Color.FromArgb(128, 128, 255);
                    break;
            }
        }

        // Refresh method to be called from child panels
        public void RefreshKeranjang()
        {
            if (currentKeranjangPanel != null)
            {
                // Force recreation untuk refresh data
                currentKeranjangPanel = null;
                Console.WriteLine("[DASHBOARD] Keranjang refresh requested");
            }
        }

        // Method to show keranjang after adding item
        public void ShowKeranjangAfterAdd()
        {
            Console.WriteLine("[DASHBOARD] Showing keranjang after add item");
            btnCustomerLihatKeranjang_Click(null, null);
        }

        // PERBAIKAN: Tambahkan method ShowLoginForm
        private void ShowLoginForm()
        {
            try
            {
                // Hide current form
                this.Hide();

                // Create and show login form
                // Sesuaikan dengan nama class Login form Anda
                var loginForm = new LoginForm(); // Ganti dengan nama form login yang benar
                loginForm.Show();

                Console.WriteLine("[DASHBOARD] Returning to login form");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to login: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"[DASHBOARD] Error showing login: {ex.Message}");
            }
        }

        // ALTERNATIF: Jika menggunakan pattern yang berbeda
        private void ShowLoginFormAlternative()
        {
            try
            {
                // Method 1: Jika menggunakan Application.OpenForms
                foreach (Form form in Application.OpenForms)
                {
                    if (form is LoginForm) // Ganti dengan nama form login
                    {
                        form.Show();
                        form.BringToFront();
                        this.Hide();
                        return;
                    }
                }

                // Method 2: Jika form login belum ada, buat baru
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing login: {ex.Message}");
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty - designer generated
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty - designer generated
        }

        // Handle form load to ensure proper initialization
        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            SetWelcomeMessage();
            Console.WriteLine("[DASHBOARD] Customer dashboard loaded");
        }

        // Handle form closing
        private void CustomerDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user closes without logout, still clear session
            if (UserSession.IsLoggedIn)
            {
                var result = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel closing
                    return;
                }

                // Clear session and show login
                AuthService.Logout();
                Console.WriteLine("[DASHBOARD] User logged out on form close");
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            // Empty method - add functionality if needed
        }

        private void btnKembaliKeLogin_Click_1(object sender, EventArgs e)
        {
            // Konfirmasi kembali ke login
            var result = MessageBox.Show("Kembali ke halaman login?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session
                AuthService.Logout();
                Console.WriteLine("[DASHBOARD] User manually returned to login");

                var loginForm = new LoginForm(); // Ganti dengan nama form login yang benar
                loginForm.Show();

                // Tutup form customer dashboard
                this.Close();
            }
        }

        // TAMBAHAN: Method untuk refresh UI setelah ada perubahan di keranjang
        public async void RefreshKeranjangUI()
        {
            if (currentKeranjangPanel != null)
            {
                try
                {
                    // Jika sedang di halaman keranjang, refresh
                    if (panelKontainerCustomer.Controls.Contains(currentKeranjangPanel))
                    {
                        Console.WriteLine("[DASHBOARD] Refreshing keranjang UI");

                        // Recreate panel untuk data terbaru
                        panelKontainerCustomer.Controls.Clear();
                        currentKeranjangPanel = new lihatKeranjangPanel();
                        currentKeranjangPanel.Dock = DockStyle.Fill;
                        panelKontainerCustomer.Controls.Add(currentKeranjangPanel);

                        // Refresh UI
                        panelKontainerCustomer.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DASHBOARD] Error refreshing keranjang UI: {ex.Message}");
                }
            }
        }

        // Method untuk auto-refresh keranjang setiap beberapa detik (opsional)
        private Timer keranjangRefreshTimer;

        private void StartKeranjangAutoRefresh()
        {
            if (keranjangRefreshTimer == null)
            {
                keranjangRefreshTimer = new Timer();
                keranjangRefreshTimer.Interval = 5000; // 5 detik
                keranjangRefreshTimer.Tick += (s, e) =>
                {
                    if (currentKeranjangPanel != null &&
                        panelKontainerCustomer.Controls.Contains(currentKeranjangPanel))
                    {
                        RefreshKeranjangUI();
                    }
                };
            }
            keranjangRefreshTimer.Start();
        }

        private void StopKeranjangAutoRefresh()
        {
            keranjangRefreshTimer?.Stop();
        }

        
        
    }
}
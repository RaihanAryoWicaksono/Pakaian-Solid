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
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Add event handlers for missing buttons
            guna2GradientButton5.Click += BtnLogout_Click;
            btnKembaliKeLogin.Click += BtnKembaliKeLogin_Click;

            // Set welcome message
            SetWelcomeMessage();

            // Set default panel to show "Lihat Semua Pakaian"
            btnCustomerLihatSemuaPakaian_Click(null, null);
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

                // Create or reuse panel
                if (currentKeranjangPanel == null)
                {
                    currentKeranjangPanel = new lihatKeranjangPanel();
                }

                // Set dock and add to container
                currentKeranjangPanel.Dock = DockStyle.Fill;
                panelKontainerCustomer.Controls.Add(currentKeranjangPanel);

                // Update button states
                UpdateButtonStates("keranjang");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading keranjang panel: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear user session
                AuthService.Logout();

                // Show login form
                var loginForm = new Views.Shared.LoginForm();
                loginForm.Show();

                // Close this form
                this.Close();
            }
        }

        private void BtnKembaliKeLogin_Click(object sender, EventArgs e)
        {
            BtnLogout_Click(sender, e); // Same as logout
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
                // Refresh keranjang panel if it exists
                currentKeranjangPanel = null; // Force recreation on next click
            }
        }

        // Method to show keranjang after adding item
        public void ShowKeranjangAfterAdd()
        {
            btnCustomerLihatKeranjang_Click(null, null);
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
                var loginForm = new Views.Shared.LoginForm();
                loginForm.Show();
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {

        }

        private void btnKembaliKeLogin_Click_1(object sender, EventArgs e)
        {
            LoginForm formBaru = new LoginForm();
            formBaru.Show();

            this.Hide(); // Hide current form instead of closing
        }
    }
}
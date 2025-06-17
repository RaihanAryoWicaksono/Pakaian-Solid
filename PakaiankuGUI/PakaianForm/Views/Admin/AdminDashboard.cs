using PakaianForm.Services;
using PakaianForm.Views.Admin.Panel;
using PakaianForm.Views.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianForm.Views.Admin
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);
        }

        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            KelolaPakaian kelolaPakaian = new KelolaPakaian();
            kelolaPakaian.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kelolaPakaian);
        }
        

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            panelEditPakaian editPakaian = new panelEditPakaian();
            editPakaian.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(editPakaian);
        }

        private void AdminDashboard_FormClosing(object sender, FormClosingEventArgs e)
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

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            AdminDashboard_FormClosing(sender, new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        private void btnKembalikeLogin_Click(object sender, EventArgs e)
        {
            LoginForm formBaru = new LoginForm();
            formBaru.Show();

            this.Hide(); // Hide current form instead of closing
        }
    }
}

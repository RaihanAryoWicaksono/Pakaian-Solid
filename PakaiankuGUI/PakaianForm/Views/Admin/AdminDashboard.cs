// PakaianForm/Views/Admin/AdminDashboard.cs
using PakaianForm.Models;
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
            TampilkanKontrol(new KelolaPakaian());
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);

            if (kontrol is KelolaPakaian kelolaPakaianPanel)
            {
                kelolaPakaianPanel.OnNavigateToPanel -= KelolaPakaian_OnNavigateToPanel;
                kelolaPakaianPanel.OnNavigateToPanel += KelolaPakaian_OnNavigateToPanel;
            }
            else if (kontrol is panelEditPakaian editPakaianPanel)
            {
                editPakaianPanel.OnNavigateToPanel -= PanelEditPakaian_OnNavigateToPanel;
                editPakaianPanel.OnNavigateToPanel += PanelEditPakaian_OnNavigateToPanel;
            }
            else if (kontrol is panelAddPakaian addPakaianPanel)
            {
                addPakaianPanel.OnNavigateToPanel -= PanelAddPakaian_OnNavigateToPanel;
                addPakaianPanel.OnNavigateToPanel += PanelAddPakaian_OnNavigateToPanel;
                addPakaianPanel.OnPakaianAdded -= PanelAddPakaian_OnPakaianAdded;
                addPakaianPanel.OnPakaianAdded += PanelAddPakaian_OnPakaianAdded;
            }
        }

        private void KelolaPakaian_OnNavigateToPanel(UserControl targetPanel)
        {
            TampilkanKontrol(targetPanel);
        }

        private void PanelEditPakaian_OnNavigateToPanel(UserControl targetPanel)
        {
            TampilkanKontrol(targetPanel);
        }

        private void PanelAddPakaian_OnNavigateToPanel(UserControl targetPanel)
        {
            TampilkanKontrol(targetPanel);
        }

        private void PanelAddPakaian_OnPakaianAdded(PakaianDtos newPakaian)
        {
            MessageBox.Show($"Pakaian baru '{newPakaian.Nama}' ditambahkan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            TampilkanKontrol(new KelolaPakaian());
        }

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            TampilkanKontrol(new panelAddPakaian());
        }

        private void AdminDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserSession.IsLoggedIn)
            {
                var result = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

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
            AuthService.Logout(); // Logout user
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Sembunyikan dashboard;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AuthService.Logout(); // Logout user
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Sembunyikan dashboard
        }
    }
}

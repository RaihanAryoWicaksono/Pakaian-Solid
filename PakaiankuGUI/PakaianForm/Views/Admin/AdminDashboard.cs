// PakaianForm/Views/Admin/AdminDashboard.cs
using PakaianForm.Models;
using PakaianForm.Services;
using PakaianForm.Views.Admin.Panel; // Untuk KelolaPakaian, panelEditPakaian, panelAddPakaian
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
            // Saat AdminDashboard pertama kali dibuka, tampilkan KelolaPakaian
            TampilkanKontrol(new KelolaPakaian());
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);

            // Berlangganan event navigasi dari panel yang ditampilkan
            if (kontrol is KelolaPakaian kelolaPakaianPanel)
            {
                kelolaPakaianPanel.OnNavigateToPanel -= KelolaPakaian_OnNavigateToPanel; // Hapus langganan lama
                kelolaPakaianPanel.OnNavigateToPanel += KelolaPakaian_OnNavigateToPanel;
            }
            else if (kontrol is panelEditPakaian editPakaianPanel)
            {
                editPakaianPanel.OnNavigateToPanel -= PanelEditPakaian_OnNavigateToPanel; // Hapus langganan lama
                editPakaianPanel.OnNavigateToPanel += PanelEditPakaian_OnNavigateToPanel;
            }
            // Tambahkan penanganan untuk panelAddPakaian
            else if (kontrol is panelAddPakaian addPakaianPanel)
            {
                addPakaianPanel.OnNavigateToPanel -= PanelAddPakaian_OnNavigateToPanel; // Hapus langganan lama
                addPakaianPanel.OnNavigateToPanel += PanelAddPakaian_OnNavigateToPanel;
                addPakaianPanel.OnPakaianAdded -= PanelAddPakaian_OnPakaianAdded; // Berlangganan event pakaian ditambahkan
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
            // Opsional: Lakukan sesuatu setelah pakaian baru ditambahkan
            // Misalnya, refresh panel KelolaPakaian untuk menampilkan item baru
            // TampilkanKontrol(new KelolaPakaian()); // Ini sudah dilakukan di panelAddPakaian
        }


        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            // Tombol "Lihat Semua Pakaian" tetap menampilkan daftar
            TampilkanKontrol(new KelolaPakaian());
        }

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            // Mengubah tombol "Kelola Katalog Pakaian" menjadi "Tambah Pakaian"
            TampilkanKontrol(new panelAddPakaian()); // <--- PERUBAHAN UTAMA
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
            LoginForm formBaru = new LoginForm();
            formBaru.Show();

            this.Hide();
        }
    }
}

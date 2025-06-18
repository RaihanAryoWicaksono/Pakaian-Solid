// PakaianForm/Views/Admin/AdminDashboard.cs
using PakaianForm.Services;
using PakaianForm.Views.Admin.Panel; // Untuk KelolaPakaian, panelEditPakaian
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
            // Saat AdminDashboard pertama kali dibuka, mungkin langsung tampilkan KelolaPakaian
            // Ini akan memuat data otomatis.
            TampilkanKontrol(new KelolaPakaian());
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);

            // Jika kontrol yang ditampilkan adalah KelolaPakaian, berlangganan event navigasinya
            if (kontrol is KelolaPakaian kelolaPakaianPanel)
            {
                kelolaPakaianPanel.OnNavigateToPanel -= KelolaPakaian_OnNavigateToPanel; // Hapus langganan sebelumnya jika ada
                kelolaPakaianPanel.OnNavigateToPanel += KelolaPakaian_OnNavigateToPanel;
            }
            // Jika kontrol yang ditampilkan adalah panelEditPakaian, berlangganan event navigasinya
            else if (kontrol is panelEditPakaian editPakaianPanel)
            {
                editPakaianPanel.OnNavigateToPanel -= PanelEditPakaian_OnNavigateToPanel; // Hapus langganan sebelumnya
                editPakaianPanel.OnNavigateToPanel += PanelEditPakaian_OnNavigateToPanel;
            }
        }

        // Event handler untuk navigasi dari KelolaPakaian (misal: tombol Edit diklik)
        private void KelolaPakaian_OnNavigateToPanel(UserControl targetPanel)
        {
            TampilkanKontrol(targetPanel);
        }

        // Event handler untuk navigasi dari panelEditPakaian (misal: tombol Batal diklik)
        private void PanelEditPakaian_OnNavigateToPanel(UserControl targetPanel)
        {
            TampilkanKontrol(targetPanel);
        }


        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            // Ini sudah benar, menampilkan panel KelolaPakaian utama
            TampilkanKontrol(new KelolaPakaian());
        }

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            // Tombol "Kelola Katalog Pakaian" ini seharusnya juga menampilkan KelolaPakaian
            // agar pengguna bisa melihat daftar pakaian dan memilih untuk mengedit/menambah.
            // Jika Anda ingin tombol ini langsung ke form "Tambah Pakaian", Anda bisa membuat
            // panel terpisah untuk itu (misal: panelAddPakaian).
            TampilkanKontrol(new KelolaPakaian()); // <--- PERBAIKAN DI SINI
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

// PakaianForm/Views/Customer/CustomerDashboard.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Services; // Untuk UserSession, AuthService
using PakaianForm.Views.Customer.Panel; // Untuk lihatSemuaPakaian, lihatKeranjangPanel
using PakaianForm.Views.Shared; // Untuk LoginForm

namespace PakaianForm.Views.Customer
{
    public partial class CustomerDashboard : Form
    {
        // Instansiasi panel yang akan digunakan (bisa Singleton atau dibuat ulang)
        private lihatSemuaPakaian _lihatSemuaPakaianPanel;
        private lihatKeranjangPanel _lihatKeranjangPanel;

        public CustomerDashboard()
        {
            InitializeComponent();

            // Inisialisasi panel
            _lihatSemuaPakaianPanel = new lihatSemuaPakaian();
            _lihatKeranjangPanel = new lihatKeranjangPanel();

            // Set event handler untuk tombol-tombol navigasi di dashboard
            if (btnCustomerLihatSemuaPakaian != null) btnCustomerLihatSemuaPakaian.Click += BtnLihatSemuaPakaian_Click;
            if (btnCustomerLihatKeranjang != null) btnCustomerLihatKeranjang.Click += BtnLihatKeranjang_Click;
            if (btnKembaliKeLogin != null) btnKembaliKeLogin.Click += BtnKembaliKeLogin_Click;

            // Inisialisasi tampilan awal
            TampilkanKontrol(_lihatSemuaPakaianPanel);
        }

        // Metode untuk menampilkan kontrol UserControl di panel utama dashboard
        public void TampilkanKontrol(UserControl kontrol)
        {
            if (panelKontainerCustomer == null) return; // Keamanan
            panelKontainerCustomer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainerCustomer.Controls.Add(kontrol);
        }

        // Event handler untuk tombol "Lihat Semua Pakaian"
        private void BtnLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            TampilkanKontrol(_lihatSemuaPakaianPanel);
            // Muat ulang data daftar pakaian setiap kali ditampilkan (opsional, tergantung kebutuhan)
            _lihatSemuaPakaianPanel.LoadPakaianData(); // Panggil metode publik untuk memuat ulang
        }

        // Event handler untuk tombol "Lihat Keranjang"
        private void BtnLihatKeranjang_Click(object sender, EventArgs e)
        {
            TampilkanKontrol(_lihatKeranjangPanel);
            // PENTING: Muat ulang data keranjang setiap kali panel keranjang ditampilkan
            _lihatKeranjangPanel.LoadKeranjangData(); // Panggil metode publik untuk memuat ulang
        }

        // Event handler untuk tombol "Kembali Ke Login"
        private void BtnKembaliKeLogin_Click(object sender, EventArgs e)
        {
            AuthService.Logout(); // Logout user
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Sembunyikan dashboard
        }

        // Event handler untuk menutup form Dashboard
        private void CustomerDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Pastikan logout saat form ditutup jika masih login
            if (UserSession.IsLoggedIn)
            {
                var result = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Batalkan penutupan form
                    return;
                }
                AuthService.Logout(); // Logout saat keluar
            }
            Application.Exit(); // Tutup aplikasi sepenuhnya
        }

        private void guna2Panel_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty - designer generated
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Leave empty - designer generated
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

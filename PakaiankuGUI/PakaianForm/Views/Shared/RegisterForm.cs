// PakaianForm/Views/Shared/RegisterForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk RegisterRequest, UserRole
using PakaianForm.Services; // Untuk AuthService

namespace PakaianForm.Views.Shared
{
    public partial class RegisterForm : Form
    {
        // Kontrol yang dideklarasikan di Designer.cs. Akses langsung melalui namanya.
        // private Guna.UI2.WinForms.Guna2GradientButton btnRegister;
        // private System.Windows.Forms.Label label2; // Label "REGISTER"
        // private System.Windows.Forms.Label labelTokoPakaian; // Label "Toko Pakaian"
        // private Guna.UI2.WinForms.Guna2GradientButton btnBackToLogin; // Tombol "LOGIN" / Kembali ke Login
        // private Guna.UI2.WinForms.Guna2TextBox tbRegisterPassword;
        // private Guna.UI2.WinForms.Guna2TextBox tbRegisterUsername;
        // private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        // private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        // private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2; // Minimize
        // private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1; // Close


        public RegisterForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set password character untuk security
            if (tbRegisterPassword != null) tbRegisterPassword.UseSystemPasswordChar = true;
            // Jika ada tbConfirmPassword, set juga useSystemPasswordChar nya
            // if (tbConfirmPassword != null) tbConfirmPassword.UseSystemPasswordChar = true;

            // Set fokus ke username saat form load
            if (tbRegisterUsername != null) tbRegisterUsername.Focus();

            // Add enter key handlers untuk quick navigation
            if (tbRegisterUsername != null) tbRegisterUsername.KeyPress += TbRegisterUsername_KeyPress;
            if (tbRegisterPassword != null) tbRegisterPassword.KeyPress += TbRegisterPassword_KeyPress;
            // Jika ada tbConfirmPassword, tambahkan handler
            // if (tbConfirmPassword != null) tbConfirmPassword.KeyPress += TbConfirmPassword_KeyPress;

            // Hubungkan event handler untuk tombol
            if (btnRegister != null) btnRegister.Click += BtnRegister_Click;
            if (btnBackToLogin != null) btnBackToLogin.Click += BtnBackToLogin_Click;

            // Pastikan event Load juga dihubungkan di Designer.cs
            this.Load += RegisterForm_Load;
        }

        // Event handler untuk tombol Register
        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(tbRegisterUsername?.Text) ||
                string.IsNullOrWhiteSpace(tbRegisterPassword?.Text) /* || (tbConfirmPassword != null && string.IsNullOrWhiteSpace(tbConfirmPassword?.Text)) */ )
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Jika ada konfirmasi password
            // if (tbConfirmPassword != null && tbRegisterPassword.Text != tbConfirmPassword.Text)
            // {
            //     MessageBox.Show("Password dan Konfirmasi Password tidak cocok!", "Validation Error",
            //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     if (tbRegisterPassword != null) tbRegisterPassword.Clear();
            //     if (tbConfirmPassword != null) tbConfirmPassword.Clear();
            //     if (tbRegisterPassword != null) tbRegisterPassword.Focus();
            //     return;
            // }

            SetLoadingState(true);

            try
            {
                // Buat objek RegisterRequest
                var registerRequest = new RegisterRequest
                {
                    Username = tbRegisterUsername?.Text?.Trim(),
                    Password = tbRegisterPassword?.Text,
                    Role = UserRole.Customer // Default role adalah Customer
                };

                // Panggil API untuk mendaftar
                string resultMessage = await AuthService.RegisterAsync(registerRequest);

                if (resultMessage.Contains("berhasil"))
                {
                    MessageBox.Show(resultMessage, "Registrasi Berhasil",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Tutup form register dan kembali ke form login
                }
                else
                {
                    MessageBox.Show(resultMessage, "Registrasi Gagal",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (tbRegisterPassword != null) tbRegisterPassword.Clear();
                    // if (tbConfirmPassword != null) tbConfirmPassword.Clear();
                    if (tbRegisterPassword != null) tbRegisterPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat registrasi:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        // Event handler untuk tombol kembali ke Login
        private void BtnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close(); // Tutup form register
        }

        private void SetLoadingState(bool loading)
        {
            if (btnRegister != null) btnRegister.Enabled = !loading;
            if (btnBackToLogin != null) btnBackToLogin.Enabled = !loading;
            if (tbRegisterUsername != null) tbRegisterUsername.Enabled = !loading;
            if (tbRegisterPassword != null) tbRegisterPassword.Enabled = !loading;
            // if (tbConfirmPassword != null) tbConfirmPassword.Enabled = !loading;

            if (loading)
            {
                if (btnRegister != null) btnRegister.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                if (btnRegister != null) btnRegister.Text = "REGISTER";
                this.Cursor = Cursors.Default;
            }
        }

        // Event handler KeyPress untuk navigasi cepat dan kontrol input
        private void TbRegisterUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (tbRegisterPassword != null) tbRegisterPassword.Focus();
            }
        }

        private void TbRegisterPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                // Jika ada tbConfirmPassword, fokus ke sana
                // if (tbConfirmPassword != null) tbConfirmPassword.Focus();
                // Jika tidak ada, langsung panggil klik tombol register
                // else 
                BtnRegister_Click(sender, e);
            }
        }

        // Jika ada konfirmasi password
        private void TbConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnRegister_Click(sender, e);
            }
        }

        // Event handler RegisterForm_Load
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Logika saat form dimuat (misal: membersihkan field)
            ClearRegisterFormFields();
        }

        // Helper method untuk membersihkan field form
        private void ClearRegisterFormFields()
        {
            if (tbRegisterUsername != null) tbRegisterUsername.Clear();
            if (tbRegisterPassword != null) tbRegisterPassword.Clear();
            // if (tbConfirmPassword != null) tbConfirmPassword.Clear();
            if (tbRegisterUsername != null) tbRegisterUsername.Focus();
        }


        // Event handler yang direferensikan oleh desainer (dapat dihapus jika tidak ada logika khusus)
        private void label2_Click(object sender, EventArgs e) { }
        private void labelTokoPakaian_Click(object sender, EventArgs e) { }
        private void guna2GradientButton1_Click(object sender, EventArgs e) { /* Placeholder for btnRegister */ }
        private void guna2GradientButton2_Click(object sender, EventArgs e) { /* Placeholder for btnBackToLogin */ }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { /* Placeholder for tbRegisterPassword text changed */ }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { /* Placeholder for tbRegisterUsername text changed */ }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { /* Placeholder for image click */ }
    }
}

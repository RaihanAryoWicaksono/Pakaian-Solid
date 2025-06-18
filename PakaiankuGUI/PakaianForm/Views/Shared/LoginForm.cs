// PakaianForm/Views/Shared/LoginForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PakaianForm.Models;
using PakaianForm.Services;

namespace PakaianForm.Views.Shared
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            InitializeForm();
            this.Shown += LoginForm_Shown; // Pastikan event Shown ini diinisialisasi
            this.FormClosing += LoginForm_FormClosing; // Pastikan event FormClosing ini diinisialisasi
        }

        private void InitializeForm()
        {
            tbPassword.UseSystemPasswordChar = true;
            tbUsername.Focus();

            tbUsername.KeyPress += TbUsername_KeyPress;
            tbPassword.KeyPress += TbPassword_KeyPress;

            btnMoveToRegister.Click += BtnMoveToRegister_Click;
            btnLogin.Click += btnLogin_Click; // Pastikan tombol login juga punya event handler
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Focus();
                return;
            }

            SetLoadingState(true);

            try
            {
                var loginRequest = new User
                {
                    Username = tbUsername.Text.Trim(),
                    Password = tbPassword.Text
                };

                var response = await AuthService.LoginAsync(loginRequest);

                if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("berhasil"))
                {
                    MessageBox.Show($"Selamat datang, {tbUsername.Text}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (response.Role == UserRole.Admin)
                    {
                        var adminDashboard = new Views.Admin.AdminDashboard();
                        adminDashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        var customerDashboard = new Views.Customer.CustomerDashboard(); // Anda perlu membuat CustomerDashboard jika belum ada
                        customerDashboard.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Login gagal. Periksa username dan password Anda.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbPassword.Clear();
                    tbPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat login:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPassword.Clear();
                tbPassword.Focus();
            }
            finally { SetLoadingState(false); }
        }

        private void BtnMoveToRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog(this);
        }

        private void SetLoadingState(bool loading)
        {
            btnLogin.Enabled = !loading;
            btnMoveToRegister.Enabled = !loading;
            tbUsername.Enabled = !loading;
            tbPassword.Enabled = !loading;

            if (loading) { btnLogin.Text = "Loading..."; this.Cursor = Cursors.WaitCursor; }
            else { btnLogin.Text = "LOGIN"; this.Cursor = Cursors.Default; }
        }

        private void TbUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; tbPassword.Focus(); }
        }

        private void TbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; btnLogin_Click(sender, e); }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            tbUsername.Clear();
            tbPassword.Clear();
            tbUsername.Focus();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UserSession.IsLoggedIn) { Application.Exit(); }
        }
    }
}

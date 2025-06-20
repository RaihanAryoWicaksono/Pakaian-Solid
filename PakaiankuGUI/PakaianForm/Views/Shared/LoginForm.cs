using PakaianForm.Models;
using PakaianForm.Services;
using System;
using System.Windows.Forms;

namespace PakaianForm.Views.Shared
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set password character untuk security
            tbPassword.UseSystemPasswordChar = true;

            // Set focus ke username saat form load
            tbUsername.Focus();

            // Add enter key handlers untuk quick navigation
            tbUsername.KeyPress += TbUsername_KeyPress;
            tbPassword.KeyPress += TbPassword_KeyPress;

            // Add register button event
            btnMoveToRegister.Click += BtnMoveToRegister_Click;

            // Fix register button color
            btnMoveToRegister.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
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

        private async void BtnMoveToRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Open Register Form
                var registerForm = new RegisterForm();
                registerForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membuka form register: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetLoadingState(bool loading)
        {
            // Disable/enable controls during loading
            btnLogin.Enabled = !loading;
            btnMoveToRegister.Enabled = !loading;
            tbUsername.Enabled = !loading;
            tbPassword.Enabled = !loading;

            // Change button text and cursor
            if (loading)
            {
                btnLogin.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnLogin.Text = "LOGIN";
                this.Cursor = Cursors.Default;
            }
        }

        // Enter key press handlers for quick navigation
        private void TbUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                tbPassword.Focus();
            }
        }

        private void TbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnLogin_Click(sender, e);
            }
        }

        // Optional: Clear fields when form is shown
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            tbUsername.Clear();
            tbPassword.Clear();
            tbUsername.Focus();
        }

        // Handle form closing
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Use Services.UserSession since that's where your UserSession is defined
            if (!Services.UserSession.IsLoggedIn)
            {
                Application.Exit();
            }
        }
    }
}
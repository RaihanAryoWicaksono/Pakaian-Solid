using System;
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
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Focus();
                return;
            }

            // Show loading state
            SetLoadingState(true);

            try
            {
                // Create login request
                var loginRequest = new User
                {
                    Username = tbUsername.Text.Trim(),
                    Password = tbPassword.Text
                };

                // Call API
                var response = await AuthService.LoginAsync(loginRequest);

                // Check response
                if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("berhasil"))
                {
                    // Store user session
                    UserSession.CurrentUser = tbUsername.Text.Trim();
                    UserSession.Role = response.Role;

                    // Login successful
                    MessageBox.Show($"Selamat datang, {tbUsername.Text}!", "Login Berhasil",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Navigate based on role
                    if (response.Role == UserRole.Admin)
                    {
                        // Open Admin Dashboard
                        var adminDashboard = new Views.Admin.AdminDashboard();
                        adminDashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Open Customer Dashboard
                        var customerDashboard = new Views.Customer.CustomerDashboard();
                        customerDashboard.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // Login failed
                    MessageBox.Show(response.Message ?? "Login gagal. Periksa username dan password Anda.",
                        "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clear password field
                    tbPassword.Clear();
                    tbPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                // Handle network or other errors
                MessageBox.Show($"Terjadi kesalahan saat login:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password field
                tbPassword.Clear();
                tbPassword.Focus();
            }
            finally
            {
                // Reset loading state
                SetLoadingState(false);
            }
        }

        private void BtnMoveToRegister_Click(object sender, EventArgs e)
        {
            // Open Register Form
            var registerForm = new RegisterForm();
            registerForm.ShowDialog(this);
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
            // Make sure to show main menu if this was not a successful login
            if (!UserSession.IsLoggedIn)
            {
                // You can add logic here to show main menu or exit application
                Application.Exit();
            }
        }
    }
}
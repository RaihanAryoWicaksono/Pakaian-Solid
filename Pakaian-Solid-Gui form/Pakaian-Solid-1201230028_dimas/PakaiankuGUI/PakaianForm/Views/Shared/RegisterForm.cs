using PakaianForm.Models;
using PakaianForm.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianForm.Views.Shared
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set password character untuk security
            tbRegisterPassword.UseSystemPasswordChar = true;

            // Fix button color issue
            btnMoveToLogin.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);

            // Set focus ke username saat form load
            tbRegisterUsername.Focus();

            // Add enter key handlers untuk quick navigation
            tbRegisterUsername.KeyPress += TbRegisterUsername_KeyPress;
            tbRegisterPassword.KeyPress += TbRegisterPassword_KeyPress;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Empty - untuk label REGISTER
        }

        private void labelTokoPakaian_Click(object sender, EventArgs e)
        {
            // Empty - untuk label Toko Pakaian
        }

        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // REGISTER BUTTON CLICK
            await HandleRegister();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // LOGIN BUTTON CLICK - Kembali ke login form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            // Password textbox changed - empty
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Username textbox changed - empty
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            // Picture box click - empty
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Form load - set focus to username
            tbRegisterUsername.Focus();
        }

        // Main registration logic
        private async Task HandleRegister()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(tbRegisterUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRegisterUsername.Focus();
                return;
            }

            if (tbRegisterUsername.Text.Length < 3)
            {
                MessageBox.Show("Username minimal 3 karakter!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRegisterUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbRegisterPassword.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRegisterPassword.Focus();
                return;
            }

            if (tbRegisterPassword.Text.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRegisterPassword.Focus();
                return;
            }

            // Show loading state
            SetLoadingState(true);

            try
            {
                // Create user object for registration
                var newUser = new User
                {
                    Username = tbRegisterUsername.Text.Trim(),
                    Password = tbRegisterPassword.Text,
                    Email = $"{tbRegisterUsername.Text.Trim()}@example.com", // Default email
                    FullName = tbRegisterUsername.Text.Trim(),
                    Role = UserRole.Customer // Default role
                };

                // Call register service
                var response = await AuthService.RegisterAsync(newUser);

                // Check response
                if (!string.IsNullOrEmpty(response) && response.Contains("berhasil"))
                {
                    // Registration successful
                    MessageBox.Show($"Registrasi berhasil!\nUsername: {newUser.Username}\n" +
                                   "Silakan login dengan akun yang baru dibuat.",
                                   "Registrasi Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Set dialog result to OK and close
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Registration failed
                    MessageBox.Show(response ?? "Registrasi gagal. Username mungkin sudah digunakan.",
                                   "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clear password field
                    tbRegisterPassword.Clear();
                    tbRegisterPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                // Handle network or other errors
                MessageBox.Show($"Terjadi kesalahan saat registrasi:\n{ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password field
                tbRegisterPassword.Clear();
                tbRegisterPassword.Focus();
            }
            finally
            {
                // Reset loading state
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading)
        {
            // Disable/enable controls during loading
            btnRegister.Enabled = !loading;
            btnMoveToLogin.Enabled = !loading;
            tbRegisterUsername.Enabled = !loading;
            tbRegisterPassword.Enabled = !loading;

            // Change button text and cursor
            if (loading)
            {
                btnRegister.Text = "Loading...";
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnRegister.Text = "REGISTER";
                this.Cursor = Cursors.Default;
            }
        }

        // Enter key press handlers for quick navigation
        private void TbRegisterUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                tbRegisterPassword.Focus();
            }
        }

        private void TbRegisterPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                _ = HandleRegister(); // Fire and forget async call
            }
        }

        // Override form closing to handle proper dialog result
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // If no dialog result is set, assume cancel
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }
    }
}
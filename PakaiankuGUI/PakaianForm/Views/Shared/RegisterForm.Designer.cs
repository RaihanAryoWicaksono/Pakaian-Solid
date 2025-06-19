// PakaianForm/Views/Shared/RegisterForm.Designer.cs
namespace PakaianForm.Views.Shared
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.btnRegister = new Guna.UI2.WinForms.Guna2GradientButton(); // Tombol Register
            this.label2 = new System.Windows.Forms.Label(); // Label REGISTER
            this.labelTokoPakaian = new System.Windows.Forms.Label(); // Label Toko Pakaian
            this.btnBackToLogin = new Guna.UI2.WinForms.Guna2GradientButton(); // Tombol LOGIN / Kembali ke Login
            this.tbRegisterPassword = new Guna.UI2.WinForms.Guna2TextBox(); // Password TextBox
            this.tbRegisterUsername = new Guna.UI2.WinForms.Guna2TextBox(); // Username TextBox
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox(); // Gambar
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox(); // Minimize Box
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox(); // Close Box
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel(); // Panel Top controls
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // btnRegister
            // 
            this.btnRegister.BorderRadius = 8;
            this.btnRegister.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRegister.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRegister.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRegister.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRegister.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(553, 357); // Lokasi
            this.btnRegister.Name = "btnRegister"; // Pastikan namanya ini
            this.btnRegister.Size = new System.Drawing.Size(389, 66);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click); // Hubungkan ke metode yang benar
            // 
            // label2 (Label REGISTER)
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(546, 126); // Lokasi
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 38);
            this.label2.TabIndex = 9;
            this.label2.Text = "REGISTER";
            // 
            // labelTokoPakaian
            // 
            this.labelTokoPakaian.AutoSize = true;
            this.labelTokoPakaian.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTokoPakaian.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelTokoPakaian.Location = new System.Drawing.Point(639, 65); // Lokasi
            this.labelTokoPakaian.Name = "labelTokoPakaian";
            this.labelTokoPakaian.Size = new System.Drawing.Size(223, 45);
            this.labelTokoPakaian.TabIndex = 8;
            this.labelTokoPakaian.Text = "Toko Pakaian";
            // 
            // btnBackToLogin (Tombol LOGIN / Kembali ke Login)
            // 
            this.btnBackToLogin.Animated = true;
            this.btnBackToLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnBackToLogin.BorderRadius = 8;
            this.btnBackToLogin.BorderThickness = 1;
            this.btnBackToLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBackToLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBackToLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBackToLogin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBackToLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBackToLogin.FillColor = System.Drawing.Color.Transparent;
            this.btnBackToLogin.FillColor2 = System.Drawing.Color.Transparent;
            this.btnBackToLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackToLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))); // Warna teks
            this.btnBackToLogin.Image = global::PakaianForm.Properties.Resources.bx_right_arrow_alt; // Pastikan resource ini ada
            this.btnBackToLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnBackToLogin.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnBackToLogin.ImageSize = new System.Drawing.Size(30, 30);
            this.btnBackToLogin.Location = new System.Drawing.Point(553, 478); // Lokasi
            this.btnBackToLogin.Name = "btnBackToLogin"; // Pastikan namanya ini
            this.btnBackToLogin.Size = new System.Drawing.Size(389, 66);
            this.btnBackToLogin.TabIndex = 13;
            this.btnBackToLogin.Text = "LOGIN";
            this.btnBackToLogin.Click += new System.EventHandler(this.BtnBackToLogin_Click); // Hubungkan ke metode yang benar
            // 
            // tbRegisterPassword (Password TextBox)
            // 
            this.tbRegisterPassword.Animated = true;
            this.tbRegisterPassword.BorderRadius = 8;
            this.tbRegisterPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbRegisterPassword.DefaultText = "";
            this.tbRegisterPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbRegisterPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbRegisterPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbRegisterPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbRegisterPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbRegisterPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbRegisterPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbRegisterPassword.IconLeft = global::PakaianForm.Properties.Resources.bx_show1; // Pastikan resource ini ada
            this.tbRegisterPassword.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.tbRegisterPassword.IconLeftSize = new System.Drawing.Size(30, 30);
            this.tbRegisterPassword.Location = new System.Drawing.Point(553, 259); // Lokasi
            this.tbRegisterPassword.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbRegisterPassword.Name = "tbRegisterPassword"; // Pastikan namanya ini
            this.tbRegisterPassword.PlaceholderText = "Password";
            this.tbRegisterPassword.SelectedText = "";
            this.tbRegisterPassword.Size = new System.Drawing.Size(389, 67);
            this.tbRegisterPassword.TabIndex = 11;
            this.tbRegisterPassword.UseSystemPasswordChar = true; // Penting untuk password
            // 
            // tbRegisterUsername (Username TextBox)
            // 
            this.tbRegisterUsername.Animated = true;
            this.tbRegisterUsername.BorderRadius = 8;
            this.tbRegisterUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbRegisterUsername.DefaultText = "";
            this.tbRegisterUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbRegisterUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbRegisterUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbRegisterUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbRegisterUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbRegisterUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbRegisterUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbRegisterUsername.IconLeft = global::PakaianForm.Properties.Resources.bx_user1; // Pastikan resource ini ada
            this.tbRegisterUsername.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.tbRegisterUsername.IconLeftSize = new System.Drawing.Size(30, 30);
            this.tbRegisterUsername.Location = new System.Drawing.Point(553, 180); // Lokasi
            this.tbRegisterUsername.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbRegisterUsername.Name = "tbRegisterUsername"; // Pastikan namanya ini
            this.tbRegisterUsername.PlaceholderText = "Username";
            this.tbRegisterUsername.SelectedText = "";
            this.tbRegisterUsername.Size = new System.Drawing.Size(389, 67);
            this.tbRegisterUsername.TabIndex = 10;
            // 
            // guna2PictureBox1 (Gambar)
            // 
            this.guna2PictureBox1.Image = global::PakaianForm.Properties.Resources.pakaian; // Pastikan resource ini ada
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(-6, -5); // Lokasi
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(497, 606);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 7;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2ControlBox2 (Minimize Box)
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox2.ForeColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2ControlBox2.Location = new System.Drawing.Point(872, 12);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(55, 37);
            this.guna2ControlBox2.TabIndex = 1;
            // 
            // guna2ControlBox1 (Close Box)
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(933, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(55, 37);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // guna2Panel1 (Panel Top controls)
            // 
            this.guna2Panel1.Controls.Add(this.guna2ControlBox2);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1000, 62);
            this.guna2Panel1.TabIndex = 14;
            // 
            // RegisterForm (Form itu sendiri)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnBackToLogin); // Tombol LOGIN
            this.Controls.Add(this.btnRegister); // Tombol REGISTER
            this.Controls.Add(this.tbRegisterPassword);
            this.Controls.Add(this.tbRegisterUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTokoPakaian);
            this.Controls.Add(this.guna2PictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientButton btnBackToLogin; // Tombol LOGIN / Kembali ke Login
        private Guna.UI2.WinForms.Guna2GradientButton btnRegister; // Tombol REGISTER
        private Guna.UI2.WinForms.Guna2TextBox tbRegisterPassword;
        private Guna.UI2.WinForms.Guna2TextBox tbRegisterUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTokoPakaian;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}

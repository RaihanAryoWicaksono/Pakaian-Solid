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
            this.btnRegister = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTokoPakaian = new System.Windows.Forms.Label();
            this.btnMoveToLogin = new Guna.UI2.WinForms.Guna2GradientButton();
            this.tbRegisterPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbRegisterUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
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
            this.btnRegister.Location = new System.Drawing.Point(553, 357);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(389, 66);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.Click += new System.EventHandler(this.guna2GradientButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(546, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 38);
            this.label2.TabIndex = 9;
            this.label2.Text = "REGISTER";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // labelTokoPakaian
            // 
            this.labelTokoPakaian.AutoSize = true;
            this.labelTokoPakaian.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTokoPakaian.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelTokoPakaian.Location = new System.Drawing.Point(639, 65);
            this.labelTokoPakaian.Name = "labelTokoPakaian";
            this.labelTokoPakaian.Size = new System.Drawing.Size(223, 45);
            this.labelTokoPakaian.TabIndex = 8;
            this.labelTokoPakaian.Text = "Toko Pakaian";
            this.labelTokoPakaian.Click += new System.EventHandler(this.labelTokoPakaian_Click);
            // 
            // btnMoveToLogin
            // 
            this.btnMoveToLogin.Animated = true;
            this.btnMoveToLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnMoveToLogin.BorderRadius = 8;
            this.btnMoveToLogin.BorderThickness = 1;
            this.btnMoveToLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMoveToLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMoveToLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMoveToLogin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMoveToLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMoveToLogin.FillColor = System.Drawing.Color.Transparent;
            this.btnMoveToLogin.FillColor2 = System.Drawing.Color.Transparent;
            this.btnMoveToLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnMoveToLogin.ForeColor = System.Drawing.Color.Transparent;
            this.btnMoveToLogin.Image = global::PakaianForm.Properties.Resources.bx_right_arrow_alt;
            this.btnMoveToLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnMoveToLogin.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnMoveToLogin.ImageSize = new System.Drawing.Size(30, 30);
            this.btnMoveToLogin.Location = new System.Drawing.Point(553, 478);
            this.btnMoveToLogin.Name = "btnMoveToLogin";
            this.btnMoveToLogin.Size = new System.Drawing.Size(389, 66);
            this.btnMoveToLogin.TabIndex = 13;
            this.btnMoveToLogin.Text = "LOGIN";
            this.btnMoveToLogin.Click += new System.EventHandler(this.guna2GradientButton2_Click);
            // 
            // tbRegisterPassword
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
            this.tbRegisterPassword.IconLeft = global::PakaianForm.Properties.Resources.bx_show1;
            this.tbRegisterPassword.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.tbRegisterPassword.IconLeftSize = new System.Drawing.Size(30, 30);
            this.tbRegisterPassword.Location = new System.Drawing.Point(553, 259);
            this.tbRegisterPassword.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbRegisterPassword.Name = "tbRegisterPassword";
            this.tbRegisterPassword.PlaceholderText = "Password";
            this.tbRegisterPassword.SelectedText = "";
            this.tbRegisterPassword.Size = new System.Drawing.Size(389, 67);
            this.tbRegisterPassword.TabIndex = 11;
            this.tbRegisterPassword.TextChanged += new System.EventHandler(this.guna2TextBox2_TextChanged);
            // 
            // tbRegisterUsername
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
            this.tbRegisterUsername.IconLeft = global::PakaianForm.Properties.Resources.bx_user1;
            this.tbRegisterUsername.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.tbRegisterUsername.IconLeftSize = new System.Drawing.Size(30, 30);
            this.tbRegisterUsername.Location = new System.Drawing.Point(553, 180);
            this.tbRegisterUsername.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbRegisterUsername.Name = "tbRegisterUsername";
            this.tbRegisterUsername.PlaceholderText = "Username";
            this.tbRegisterUsername.SelectedText = "";
            this.tbRegisterUsername.Size = new System.Drawing.Size(389, 67);
            this.tbRegisterUsername.TabIndex = 10;
            this.tbRegisterUsername.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::PakaianForm.Properties.Resources.pakaian;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(-6, -5);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(497, 606);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 7;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // guna2ControlBox2
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
            // guna2ControlBox1
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
            // guna2Panel1
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
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnMoveToLogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.tbRegisterPassword);
            this.Controls.Add(this.tbRegisterUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTokoPakaian);
            this.Controls.Add(this.guna2PictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private Guna.UI2.WinForms.Guna2GradientButton btnMoveToLogin;
        private Guna.UI2.WinForms.Guna2GradientButton btnRegister;
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
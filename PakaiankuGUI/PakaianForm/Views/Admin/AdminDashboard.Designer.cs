namespace PakaianForm.Views.Admin
{
    partial class AdminDashboard
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
            this.guna2Elipse1AdminDashboard = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTokoPakaian = new System.Windows.Forms.Label();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnKembalikeLogin = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnAdminLihatSemuaPakaian = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnKelolaKatalogPakaian = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panelKontainer = new System.Windows.Forms.Panel();
            this.kelolaPakaian1 = new PakaianForm.Views.Admin.Panel.KelolaPakaian();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.panelKontainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1AdminDashboard
            // 
            this.guna2Elipse1AdminDashboard.BorderRadius = 20;
            this.guna2Elipse1AdminDashboard.TargetControl = this;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.labelTokoPakaian);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox2);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1200, 59);
            this.guna2Panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(77, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 37);
            this.label1.TabIndex = 10;
            this.label1.Text = "Admin";
            // 
            // labelTokoPakaian
            // 
            this.labelTokoPakaian.AutoSize = true;
            this.labelTokoPakaian.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTokoPakaian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelTokoPakaian.Location = new System.Drawing.Point(485, 6);
            this.labelTokoPakaian.Name = "labelTokoPakaian";
            this.labelTokoPakaian.Size = new System.Drawing.Size(187, 37);
            this.labelTokoPakaian.TabIndex = 9;
            this.labelTokoPakaian.Text = "Toko Pakaian";
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox2.ForeColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2ControlBox2.Location = new System.Drawing.Point(1069, 12);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(55, 37);
            this.guna2ControlBox2.TabIndex = 3;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(1130, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(55, 37);
            this.guna2ControlBox1.TabIndex = 2;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.White;
            this.guna2Panel2.Controls.Add(this.btnLogout);
            this.guna2Panel2.Controls.Add(this.btnKembalikeLogin);
            this.guna2Panel2.Controls.Add(this.btnAdminLihatSemuaPakaian);
            this.guna2Panel2.Controls.Add(this.btnKelolaKatalogPakaian);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 59);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(280, 641);
            this.guna2Panel2.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Animated = true;
            this.btnLogout.BorderRadius = 8;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLogout.FillColor2 = System.Drawing.Color.Red;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(25, 567);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(236, 56);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnKembalikeLogin
            // 
            this.btnKembalikeLogin.Animated = true;
            this.btnKembalikeLogin.BorderRadius = 8;
            this.btnKembalikeLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKembalikeLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKembalikeLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKembalikeLogin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKembalikeLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKembalikeLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnKembalikeLogin.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnKembalikeLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKembalikeLogin.ForeColor = System.Drawing.Color.White;
            this.btnKembalikeLogin.Location = new System.Drawing.Point(25, 161);
            this.btnKembalikeLogin.Name = "btnKembalikeLogin";
            this.btnKembalikeLogin.Size = new System.Drawing.Size(236, 56);
            this.btnKembalikeLogin.TabIndex = 2;
            this.btnKembalikeLogin.Text = "Kembali Ke Login";
            this.btnKembalikeLogin.Click += new System.EventHandler(this.btnKembalikeLogin_Click);
            // 
            // btnAdminLihatSemuaPakaian
            // 
            this.btnAdminLihatSemuaPakaian.Animated = true;
            this.btnAdminLihatSemuaPakaian.BorderRadius = 8;
            this.btnAdminLihatSemuaPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdminLihatSemuaPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdminLihatSemuaPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdminLihatSemuaPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdminLihatSemuaPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdminLihatSemuaPakaian.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAdminLihatSemuaPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnAdminLihatSemuaPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdminLihatSemuaPakaian.ForeColor = System.Drawing.Color.White;
            this.btnAdminLihatSemuaPakaian.Location = new System.Drawing.Point(25, 37);
            this.btnAdminLihatSemuaPakaian.Name = "btnAdminLihatSemuaPakaian";
            this.btnAdminLihatSemuaPakaian.Size = new System.Drawing.Size(236, 56);
            this.btnAdminLihatSemuaPakaian.TabIndex = 0;
            this.btnAdminLihatSemuaPakaian.Text = "Lihat Semua Pakaian";
            this.btnAdminLihatSemuaPakaian.Click += new System.EventHandler(this.btnAdminLihatSemuaPakaian_Click);
            // 
            // btnKelolaKatalogPakaian
            // 
            this.btnKelolaKatalogPakaian.Animated = true;
            this.btnKelolaKatalogPakaian.BorderRadius = 8;
            this.btnKelolaKatalogPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKelolaKatalogPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKelolaKatalogPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKelolaKatalogPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKelolaKatalogPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKelolaKatalogPakaian.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnKelolaKatalogPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnKelolaKatalogPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKelolaKatalogPakaian.ForeColor = System.Drawing.Color.White;
            this.btnKelolaKatalogPakaian.Location = new System.Drawing.Point(25, 99);
            this.btnKelolaKatalogPakaian.Name = "btnKelolaKatalogPakaian";
            this.btnKelolaKatalogPakaian.Size = new System.Drawing.Size(236, 56);
            this.btnKelolaKatalogPakaian.TabIndex = 1;
            this.btnKelolaKatalogPakaian.Text = "Tambah Pakaian";
            this.btnKelolaKatalogPakaian.Click += new System.EventHandler(this.btnKelolaKatalogPakaian_Click);
            // 
            // panelKontainer
            // 
            this.panelKontainer.Controls.Add(this.kelolaPakaian1);
            this.panelKontainer.Location = new System.Drawing.Point(278, 59);
            this.panelKontainer.Name = "panelKontainer";
            this.panelKontainer.Size = new System.Drawing.Size(922, 623);
            this.panelKontainer.TabIndex = 2;
            // 
            // kelolaPakaian1
            // 
            this.kelolaPakaian1.BackColor = System.Drawing.Color.White;
            this.kelolaPakaian1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.kelolaPakaian1.Location = new System.Drawing.Point(0, -1);
            this.kelolaPakaian1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.kelolaPakaian1.Name = "kelolaPakaian1";
            this.kelolaPakaian1.Size = new System.Drawing.Size(1068, 617);
            this.kelolaPakaian1.TabIndex = 0;
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.panelKontainer);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminDashboard";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.panelKontainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1AdminDashboard;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2GradientButton btnLogout;
        private Guna.UI2.WinForms.Guna2GradientButton btnKembalikeLogin;
        private Guna.UI2.WinForms.Guna2GradientButton btnKelolaKatalogPakaian;
        private Guna.UI2.WinForms.Guna2GradientButton btnAdminLihatSemuaPakaian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTokoPakaian;
        private System.Windows.Forms.Panel panelKontainer;
        private Panel.KelolaPakaian kelolaPakaian1;
    }
}
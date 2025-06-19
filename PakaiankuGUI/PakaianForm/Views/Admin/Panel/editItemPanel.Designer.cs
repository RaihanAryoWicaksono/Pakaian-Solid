// PakaianForm/Views/Admin/Panel/editItemPanel.Designer.cs
namespace PakaianForm.Views.Admin.Panel
{
    partial class editItemPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // --- DEKLARASI DAN INISIALISASI KONTROL ---
            this.labelNamaPakaian = new System.Windows.Forms.Label(); // Pastikan namanya sama
            this.labelStatusPakaian = new System.Windows.Forms.Label(); // Pastikan namanya sama
            this.labelHarga = new System.Windows.Forms.Label(); // Pastikan namanya sama
            // this.labelBanyakPesanan = new System.Windows.Forms.Label(); // Jika ada label stok
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox(); // Pastikan namanya sama
            this.btnEditPakaian = new Guna.UI2.WinForms.Guna2GradientButton(); // Pastikan namanya sama
            this.btnHapusPakaian = new Guna.UI2.WinForms.Guna2GradientButton(); // Pastikan namanya sama
            this.guna2Elipse1editItemPanel = new Guna.UI2.WinForms.Guna2Elipse(this.components);

            // SuspendLayout untuk performa
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();

            // --- DEFINISI PROPERTI KONTROL ---
            // labelNamaPakaian
            this.labelNamaPakaian.AutoSize = true; // Atau false, sesuaikan desain Anda
            this.labelNamaPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.labelNamaPakaian.Location = new System.Drawing.Point(59, 204); // Sesuaikan lokasi
            this.labelNamaPakaian.Name = "labelNamaPakaian"; // Kritis: Harus sama
            this.labelNamaPakaian.Size = new System.Drawing.Size(140, 28); // Sesuaikan ukuran
            this.labelNamaPakaian.TabIndex = 6;
            this.labelNamaPakaian.Text = ""; // Mengosongkan teks default

            // labelStatusPakaian
            this.labelStatusPakaian.AutoSize = true;
            this.labelStatusPakaian.Location = new System.Drawing.Point(30, 252); // Sesuaikan lokasi
            this.labelStatusPakaian.Name = "labelStatusPakaian"; // Kritis: Harus sama
            this.labelStatusPakaian.Size = new System.Drawing.Size(65, 28); // Sesuaikan ukuran
            this.labelStatusPakaian.TabIndex = 9;
            this.labelStatusPakaian.Text = ""; // Mengosongkan teks default

            // labelHarga
            this.labelHarga.AutoSize = true;
            this.labelHarga.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.labelHarga.Location = new System.Drawing.Point(152, 252); // Sesuaikan lokasi
            this.labelHarga.Name = "labelHarga"; // Kritis: Harus sama
            this.labelHarga.Size = new System.Drawing.Size(77, 28); // Sesuaikan ukuran
            this.labelHarga.TabIndex = 7;
            this.labelHarga.Text = ""; // Mengosongkan teks default

            // guna2PictureBox1
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent; // Sesuaikan
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent; // Sesuaikan
            this.guna2PictureBox1.Image = global::PakaianForm.Properties.Resources.tshirt; // Pastikan resource ini ada
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(35, 21); // Sesuaikan lokasi
            this.guna2PictureBox1.Name = "guna2PictureBox1"; // Kritis: Harus sama
            this.guna2PictureBox1.Size = new System.Drawing.Size(194, 171); // Sesuaikan ukuran
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 11;
            this.guna2PictureBox1.TabStop = false;

            // btnEditPakaian
            this.btnEditPakaian.Animated = true; this.btnEditPakaian.AutoRoundedCorners = true; this.btnEditPakaian.BorderRadius = 21; this.btnEditPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray; this.btnEditPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray; this.btnEditPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169))))); this.btnEditPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169))))); this.btnEditPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141))))); this.btnEditPakaian.FillColor = System.Drawing.Color.Lime; this.btnEditPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))); this.btnEditPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold); this.btnEditPakaian.ForeColor = System.Drawing.Color.White;
            this.btnEditPakaian.Location = new System.Drawing.Point(14, 301); // Sesuaikan lokasi
            this.btnEditPakaian.Name = "btnEditPakaian"; // Kritis: Harus sama
            this.btnEditPakaian.Size = new System.Drawing.Size(112, 45); // Sesuaikan ukuran
            this.btnEditPakaian.TabIndex = 12;
            this.btnEditPakaian.Text = "Edit";

            // btnHapusPakaian
            this.btnHapusPakaian.Animated = true; this.btnHapusPakaian.AutoRoundedCorners = true; this.btnHapusPakaian.BorderRadius = 21; this.btnHapusPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray; this.btnHapusPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray; this.btnHapusPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169))))); this.btnHapusPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169))))); this.btnHapusPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141))))); this.btnHapusPakaian.FillColor = System.Drawing.Color.Red; this.btnHapusPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))); this.btnHapusPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold); this.btnHapusPakaian.ForeColor = System.Drawing.Color.White;
            this.btnHapusPakaian.Location = new System.Drawing.Point(132, 301); // Sesuaikan lokasi
            this.btnHapusPakaian.Name = "btnHapusPakaian"; // Kritis: Harus sama
            this.btnHapusPakaian.Size = new System.Drawing.Size(112, 45); // Sesuaikan ukuran
            this.btnHapusPakaian.TabIndex = 13;
            this.btnHapusPakaian.Text = "Hapus";

            // guna2Elipse1editItemPanel
            this.guna2Elipse1editItemPanel.BorderRadius = 20;
            // TargetControl tidak perlu diinisialisasi di sini karena ini UserControl itu sendiri
            // this.guna2Elipse1editItemPanel.TargetControl = this; // Biasanya tidak perlu di Designer.cs

            // --- PENAMBAHAN KONTROL KE PARENT ---
            this.Controls.Add(this.btnHapusPakaian);
            this.Controls.Add(this.btnEditPakaian);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.labelStatusPakaian);
            this.Controls.Add(this.labelHarga);
            this.Controls.Add(this.labelNamaPakaian);

            // Properti UserControl induk
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set Font
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7); // Set Margin
            this.Name = "editItemPanel";
            this.Size = new System.Drawing.Size(265, 369); // Ukuran total panel item

            // ResumeLayout
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // --- DEKLARASI VARIABEL KONTROL (HARUS ADA DI SINI, SEBAGAI 'private') ---
        private System.Windows.Forms.Label labelStatusPakaian;
        private System.Windows.Forms.Label labelHarga;
        private System.Windows.Forms.Label labelNamaPakaian;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1editItemPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2GradientButton btnHapusPakaian;
        private Guna.UI2.WinForms.Guna2GradientButton btnEditPakaian;
    }
}

// PakaianForm/Views/Admin/Panel/panelEditPakaian.Designer.cs
namespace PakaianForm.Views.Admin.Panel
{
    partial class panelEditPakaian
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2ComboBox cbStatus;

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
            // Inisialisasi kontrol tanpa argumen 'this.components'
            this.cbStatus = new Guna.UI2.WinForms.Guna2ComboBox(); // KOREKSI
            this.labelJudulEdit = new System.Windows.Forms.Label();
            this.guna2Elipse1EditPakaianPanel = new Guna.UI2.WinForms.Guna2Elipse(this.components); // Ini mungkin masih pakai components, biarkan jika desainer membuatnya
            this.btnSavePakaian = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();

            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox6 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox7 = new Guna.UI2.WinForms.Guna2TextBox();

            this.btnSaveEditPakaian = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnResetPakaian = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnBackPakaian = new Guna.UI2.WinForms.Guna2GradientButton();

            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip(); // KOREKSI


            // SuspendLayout untuk performa
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // --- Definisi properti cbStatus ---
            this.cbStatus.Animated = true;
            this.cbStatus.AutoRoundedCorners = true;
            this.cbStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cbStatus.BorderThickness = 2;
            this.cbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbStatus.ItemHeight = 20;
            this.cbStatus.Location = new System.Drawing.Point(16, 232); // Sesuaikan lokasi
            this.cbStatus.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(334, 25);
            this.cbStatus.TabIndex = 48;
            this.panel1.Controls.Add(this.cbStatus); // Pastikan ditambahkan ke panel yang benar


            // --- Definisi Kontrol Lainnya (seperti guna2TextBox1, btnSaveEditPakaian, dll.) ---
            // labelJudulEdit
            this.labelJudulEdit.AutoSize = true;
            this.labelJudulEdit.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJudulEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelJudulEdit.Location = new System.Drawing.Point(23, 18);
            this.labelJudulEdit.Name = "labelJudulEdit";
            this.labelJudulEdit.Size = new System.Drawing.Size(174, 37);
            this.labelJudulEdit.TabIndex = 7;
            this.labelJudulEdit.Text = "Edit Pakaian";

            // guna2Elipse1EditPakaianPanel
            this.guna2Elipse1EditPakaianPanel.BorderRadius = 20;
            this.guna2Elipse1EditPakaianPanel.TargetControl = this;

            // btnSavePakaian (tombol Save besar di luar panel1)
            this.btnSavePakaian.Animated = true;
            this.btnSavePakaian.AutoRoundedCorners = true;
            this.btnSavePakaian.BorderRadius = 18;
            this.btnSavePakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSavePakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSavePakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSavePakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSavePakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSavePakaian.FillColor = System.Drawing.Color.Lime;
            this.btnSavePakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSavePakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnSavePakaian.ForeColor = System.Drawing.Color.White;
            this.btnSavePakaian.Location = new System.Drawing.Point(724, 578);
            this.btnSavePakaian.Margin = new System.Windows.Forms.Padding(2);
            this.btnSavePakaian.Name = "btnSavePakaian";
            this.btnSavePakaian.Size = new System.Drawing.Size(135, 37);
            this.btnSavePakaian.TabIndex = 34;
            this.btnSavePakaian.Text = "Save";

            // guna2PictureBox1
            this.guna2PictureBox1.Image = global::PakaianForm.Properties.Resources.tshirt;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(374, 18);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(156, 102);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 31;
            this.guna2PictureBox1.TabStop = false;

            // panel1 (Panel utama untuk menampung TextBox dan tombol)
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnSaveEditPakaian);
            // Hapus baris lama yang menambahkan guna2TextBox8: this.panel1.Controls.Add(this.guna2TextBox8);
            this.panel1.Controls.Add(this.guna2TextBox7);
            this.panel1.Controls.Add(this.guna2TextBox6);
            this.panel1.Controls.Add(this.guna2TextBox5);
            this.panel1.Controls.Add(this.guna2TextBox4);
            this.panel1.Controls.Add(this.guna2TextBox3);
            this.panel1.Controls.Add(this.guna2TextBox2);
            this.panel1.Controls.Add(this.guna2TextBox1);
            this.panel1.Controls.Add(this.btnBackPakaian);
            this.panel1.Controls.Add(this.btnResetPakaian);
            this.panel1.Location = new System.Drawing.Point(29, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 273);
            this.panel1.TabIndex = 27;

            // btnSaveEditPakaian (Tombol Save di dalam panel1)
            this.btnSaveEditPakaian.Animated = true;
            this.btnSaveEditPakaian.BorderRadius = 8;
            this.btnSaveEditPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveEditPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveEditPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSaveEditPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSaveEditPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaveEditPakaian.FillColor = System.Drawing.Color.Lime;
            this.btnSaveEditPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSaveEditPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnSaveEditPakaian.ForeColor = System.Drawing.Color.White;
            this.btnSaveEditPakaian.Location = new System.Drawing.Point(381, 21);
            this.btnSaveEditPakaian.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveEditPakaian.Name = "btnSaveEditPakaian";
            this.btnSaveEditPakaian.Size = new System.Drawing.Size(94, 33);
            this.btnSaveEditPakaian.TabIndex = 49;
            this.btnSaveEditPakaian.Text = "Save";

            // guna2TextBox1 (Kode)
            this.guna2TextBox1.Animated = true;
            this.guna2TextBox1.AutoRoundedCorners = true;
            this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.BorderThickness = 2;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(16, 11);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "Kode";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox1.TabIndex = 35;

            // guna2TextBox2 (Nama)
            this.guna2TextBox2.Animated = true;
            this.guna2TextBox2.AutoRoundedCorners = true;
            this.guna2TextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.BorderThickness = 2;
            this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox2.DefaultText = "";
            this.guna2TextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Location = new System.Drawing.Point(16, 43);
            this.guna2TextBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PlaceholderText = "Nama";
            this.guna2TextBox2.SelectedText = "";
            this.guna2TextBox2.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox2.TabIndex = 42;

            // guna2TextBox3 (Kategori)
            this.guna2TextBox3.Animated = true;
            this.guna2TextBox3.AutoRoundedCorners = true;
            this.guna2TextBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.BorderThickness = 2;
            this.guna2TextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox3.DefaultText = "";
            this.guna2TextBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Location = new System.Drawing.Point(16, 75);
            this.guna2TextBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PlaceholderText = "Kategori";
            this.guna2TextBox3.SelectedText = "";
            this.guna2TextBox3.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox3.TabIndex = 43;

            // guna2TextBox4 (Warna)
            this.guna2TextBox4.Animated = true;
            this.guna2TextBox4.AutoRoundedCorners = true;
            this.guna2TextBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.BorderThickness = 2;
            this.guna2TextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox4.DefaultText = "";
            this.guna2TextBox4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Location = new System.Drawing.Point(16, 106);
            this.guna2TextBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox4.Name = "guna2TextBox4";
            this.guna2TextBox4.PlaceholderText = "Warna";
            this.guna2TextBox4.SelectedText = "";
            this.guna2TextBox4.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox4.TabIndex = 44;

            // guna2TextBox5 (Ukuran)
            this.guna2TextBox5.Animated = true;
            this.guna2TextBox5.AutoRoundedCorners = true;
            this.guna2TextBox5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox5.BorderThickness = 2;
            this.guna2TextBox5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox5.DefaultText = "";
            this.guna2TextBox5.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox5.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox5.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox5.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox5.Location = new System.Drawing.Point(16, 138);
            this.guna2TextBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox5.Name = "guna2TextBox5";
            this.guna2TextBox5.PlaceholderText = "Ukuran";
            this.guna2TextBox5.SelectedText = "";
            this.guna2TextBox5.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox5.TabIndex = 45;

            // guna2TextBox6 (Harga)
            this.guna2TextBox6.Animated = true;
            this.guna2TextBox6.AutoRoundedCorners = true;
            this.guna2TextBox6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox6.BorderThickness = 2;
            this.guna2TextBox6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox6.DefaultText = "";
            this.guna2TextBox6.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox6.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox6.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox6.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox6.Location = new System.Drawing.Point(16, 169);
            this.guna2TextBox6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox6.Name = "guna2TextBox6";
            this.guna2TextBox6.PlaceholderText = "Harga";
            this.guna2TextBox6.SelectedText = "";
            this.guna2TextBox6.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox6.TabIndex = 46;

            // guna2TextBox7 (Stok)
            this.guna2TextBox7.Animated = true;
            this.guna2TextBox7.AutoRoundedCorners = true;
            this.guna2TextBox7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2TextBox7.BorderThickness = 2;
            this.guna2TextBox7.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox7.DefaultText = "";
            this.guna2TextBox7.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox7.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox7.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox7.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox7.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox7.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox7.Location = new System.Drawing.Point(16, 201);
            this.guna2TextBox7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox7.Name = "guna2TextBox7";
            this.guna2TextBox7.PlaceholderText = "Stok";
            this.guna2TextBox7.SelectedText = "";
            this.guna2TextBox7.Size = new System.Drawing.Size(334, 25);
            this.guna2TextBox7.TabIndex = 47;

            // btnBackPakaian
            this.btnBackPakaian.Animated = true;
            this.btnBackPakaian.BorderRadius = 8;
            this.btnBackPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBackPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBackPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBackPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBackPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBackPakaian.FillColor = System.Drawing.Color.Gray;
            this.btnBackPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBackPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackPakaian.ForeColor = System.Drawing.Color.White;
            this.btnBackPakaian.Location = new System.Drawing.Point(381, 97);
            this.btnBackPakaian.Margin = new System.Windows.Forms.Padding(2);
            this.btnBackPakaian.Name = "btnBackPakaian";
            this.btnBackPakaian.Size = new System.Drawing.Size(94, 33);
            this.btnBackPakaian.TabIndex = 32;
            this.btnBackPakaian.Text = "Back";

            // btnResetPakaian
            this.btnResetPakaian.Animated = true;
            this.btnResetPakaian.BorderRadius = 8;
            this.btnResetPakaian.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnResetPakaian.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnResetPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnResetPakaian.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnResetPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnResetPakaian.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnResetPakaian.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnResetPakaian.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnResetPakaian.ForeColor = System.Drawing.Color.White;
            this.btnResetPakaian.Location = new System.Drawing.Point(381, 59);
            this.btnResetPakaian.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetPakaian.Name = "btnResetPakaian";
            this.btnResetPakaian.Size = new System.Drawing.Size(94, 33);
            this.btnResetPakaian.TabIndex = 33;
            this.btnResetPakaian.Text = "Reset";

            // guna2ContextMenuStrip1
            this.guna2ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(61, 4);

            // panelEditPakaian (UserControl itu sendiri)
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSavePakaian);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJudulEdit);
            this.Name = "panelEditPakaian";
            this.Size = new System.Drawing.Size(572, 428);

            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // --- DEKLARASI VARIABEL KONTROL ---
        private System.Windows.Forms.Label labelJudulEdit;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1EditPakaianPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2GradientButton btnSavePakaian; // Ini tombol Save di luar panel1
        private System.Windows.Forms.Panel panel1;

        // TextBox yang ada di dalam panel1
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1; // Kode
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2; // Nama
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox3; // Kategori
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox4; // Warna
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox5; // Ukuran
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox6; // Harga
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox7; // Stok


        // Tombol yang ada di dalam panel1
        private Guna.UI2.WinForms.Guna2GradientButton btnSaveEditPakaian; // Tombol Save di dalam panel1
        private Guna.UI2.WinForms.Guna2GradientButton btnBackPakaian;
        private Guna.UI2.WinForms.Guna2GradientButton btnResetPakaian;

        // Jika Anda menggunakan ContextMenuStrip (guna2ContextMenuStrip1)
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
    }
}

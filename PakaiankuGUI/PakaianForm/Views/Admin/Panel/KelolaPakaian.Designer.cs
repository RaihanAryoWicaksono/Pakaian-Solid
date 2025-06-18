// PakaianForm/Views/Admin/Panel/KelolaPakaian.Designer.cs
namespace PakaianForm.Views.Admin.Panel
{
    partial class KelolaPakaian : System.Windows.Forms.UserControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.flowLayoutPanelBarang = new System.Windows.Forms.FlowLayoutPanel();
            // --- HAPUS SEMUA BARIS editItemPanelX DI SINI DARI DESIGNER.CS ---
            // this.editItemPanel1 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // this.editItemPanel2 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // this.editItemPanel3 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // this.editItemPanel4 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // this.editItemPanel5 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // this.editItemPanel6 = new PakaianForm.Views.Admin.Panel.editItemPanel();
            // --- AKHIR HAPUS ---
            this.tbSearchPakaian = new Guna.UI2.WinForms.Guna2TextBox();
            this.flowLayoutPanelBarang.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(496, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 27);
            this.button1.TabIndex = 8;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kelola Pakaian";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // flowLayoutPanelBarang
            // 
            this.flowLayoutPanelBarang.AutoScroll = true;
            // --- HAPUS SEMUA BARIS YANG MENGADD editItemPanelX KE flowLayoutPanelBarang ---
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel1);
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel2);
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel3);
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel4);
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel5);
            // this.flowLayoutPanelBarang.Controls.Add(this.editItemPanel6);
            // --- AKHIR HAPUS ---
            this.flowLayoutPanelBarang.Location = new System.Drawing.Point(12, 67);
            this.flowLayoutPanelBarang.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelBarang.Name = "flowLayoutPanelBarang";
            this.flowLayoutPanelBarang.Size = new System.Drawing.Size(639, 354);
            this.flowLayoutPanelBarang.TabIndex = 9;
            // 
            // tbSearchPakaian
            // 
            this.tbSearchPakaian.Animated = true;
            this.tbSearchPakaian.BorderRadius = 8;
            this.tbSearchPakaian.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearchPakaian.DefaultText = "";
            this.tbSearchPakaian.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbSearchPakaian.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbSearchPakaian.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearchPakaian.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearchPakaian.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearchPakaian.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbSearchPakaian.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearchPakaian.IconLeft = global::PakaianForm.Properties.Resources.bx_search;
            this.tbSearchPakaian.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.tbSearchPakaian.Location = new System.Drawing.Point(338, 28);
            this.tbSearchPakaian.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearchPakaian.Name = "tbSearchPakaian";
            this.tbSearchPakaian.PlaceholderText = "";
            this.tbSearchPakaian.SelectedText = "";
            this.tbSearchPakaian.Size = new System.Drawing.Size(154, 27);
            this.tbSearchPakaian.TabIndex = 10;
            // 
            // KelolaPakaian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbSearchPakaian);
            this.Controls.Add(this.flowLayoutPanelBarang);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "KelolaPakaian";
            this.Size = new System.Drawing.Size(663, 435);
            this.flowLayoutPanelBarang.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBarang;
        // --- HAPUS SEMUA DEKLARASI VARIABEL editItemPanelX DI SINI ---
        // private editItemPanel editItemPanel1;
        // private editItemPanel editItemPanel2;
        // private editItemPanel editItemPanel3;
        // private editItemPanel editItemPanel4;
        // private editItemPanel editItemPanel5;
        // private editItemPanel editItemPanel6;
        // --- AKHIR HAPUS ---
        private Guna.UI2.WinForms.Guna2TextBox tbSearchPakaian;
    }
}

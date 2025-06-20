namespace PakaianForm.Views.Customer.Panel
{
    partial class lihatSemuaPakaian
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.labelListPakaian = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tbSearchPakaian = new Guna.UI2.WinForms.Guna2TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanelListPakaian = new System.Windows.Forms.FlowLayoutPanel();
            this.listPakaianPanel1 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.listPakaianPanel2 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.listPakaianPanel3 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.listPakaianPanel4 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.listPakaianPanel5 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.listPakaianPanel6 = new PakaianForm.Views.Customer.Panel.listPakaianPanel();
            this.guna2Panel1.SuspendLayout();
            this.flowLayoutPanelListPakaian.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.labelListPakaian);
            this.guna2Panel1.Controls.Add(this.tbSearchPakaian);
            this.guna2Panel1.Controls.Add(this.button1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(903, 79);
            this.guna2Panel1.TabIndex = 1;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // labelListPakaian
            // 
            this.labelListPakaian.BackColor = System.Drawing.Color.Transparent;
            this.labelListPakaian.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelListPakaian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelListPakaian.Location = new System.Drawing.Point(41, 20);
            this.labelListPakaian.Name = "labelListPakaian";
            this.labelListPakaian.Size = new System.Drawing.Size(227, 56);
            this.labelListPakaian.TabIndex = 13;
            this.labelListPakaian.Text = "List Pakaian";
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
            this.tbSearchPakaian.Location = new System.Drawing.Point(434, 34);
            this.tbSearchPakaian.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbSearchPakaian.Name = "tbSearchPakaian";
            this.tbSearchPakaian.PlaceholderText = "";
            this.tbSearchPakaian.SelectedText = "";
            this.tbSearchPakaian.Size = new System.Drawing.Size(344, 33);
            this.tbSearchPakaian.TabIndex = 12;
            this.tbSearchPakaian.TextChanged += new System.EventHandler(this.tbSearchPakaian_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(786, 34);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanelListPakaian
            // 
            this.flowLayoutPanelListPakaian.AutoScroll = true;
            this.flowLayoutPanelListPakaian.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel1);
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel2);
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel3);
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel4);
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel5);
            this.flowLayoutPanelListPakaian.Controls.Add(this.listPakaianPanel6);
            this.flowLayoutPanelListPakaian.Location = new System.Drawing.Point(17, 85);
            this.flowLayoutPanelListPakaian.Name = "flowLayoutPanelListPakaian";
            this.flowLayoutPanelListPakaian.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanelListPakaian.Size = new System.Drawing.Size(861, 531);
            this.flowLayoutPanelListPakaian.TabIndex = 2;
            this.flowLayoutPanelListPakaian.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelListPakaian_Paint);
            // 
            // listPakaianPanel1
            // 
            this.listPakaianPanel1.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel1.Location = new System.Drawing.Point(4, 5);
            this.listPakaianPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel1.Name = "listPakaianPanel1";
            this.listPakaianPanel1.Pakaian = null;
            this.listPakaianPanel1.Size = new System.Drawing.Size(273, 369);
            this.listPakaianPanel1.TabIndex = 3;
            this.listPakaianPanel1.Load += new System.EventHandler(this.listPakaianPanel1_Load);
            // 
            // listPakaianPanel2
            // 
            this.listPakaianPanel2.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel2.Location = new System.Drawing.Point(285, 5);
            this.listPakaianPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel2.Name = "listPakaianPanel2";
            this.listPakaianPanel2.Pakaian = null;
            this.listPakaianPanel2.Size = new System.Drawing.Size(273, 369);
            this.listPakaianPanel2.TabIndex = 4;
            // 
            // listPakaianPanel3
            // 
            this.listPakaianPanel3.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel3.Location = new System.Drawing.Point(566, 5);
            this.listPakaianPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel3.Name = "listPakaianPanel3";
            this.listPakaianPanel3.Pakaian = null;
            this.listPakaianPanel3.Size = new System.Drawing.Size(270, 369);
            this.listPakaianPanel3.TabIndex = 5;
            // 
            // listPakaianPanel4
            // 
            this.listPakaianPanel4.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel4.Location = new System.Drawing.Point(4, 384);
            this.listPakaianPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel4.Name = "listPakaianPanel4";
            this.listPakaianPanel4.Pakaian = null;
            this.listPakaianPanel4.Size = new System.Drawing.Size(272, 369);
            this.listPakaianPanel4.TabIndex = 6;
            // 
            // listPakaianPanel5
            // 
            this.listPakaianPanel5.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel5.Location = new System.Drawing.Point(284, 384);
            this.listPakaianPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel5.Name = "listPakaianPanel5";
            this.listPakaianPanel5.Pakaian = null;
            this.listPakaianPanel5.Size = new System.Drawing.Size(272, 369);
            this.listPakaianPanel5.TabIndex = 7;
            // 
            // listPakaianPanel6
            // 
            this.listPakaianPanel6.BackColor = System.Drawing.Color.White;
            this.listPakaianPanel6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPakaianPanel6.Location = new System.Drawing.Point(564, 384);
            this.listPakaianPanel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPakaianPanel6.Name = "listPakaianPanel6";
            this.listPakaianPanel6.Pakaian = null;
            this.listPakaianPanel6.Size = new System.Drawing.Size(272, 369);
            this.listPakaianPanel6.TabIndex = 8;
            // 
            // lihatSemuaPakaian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flowLayoutPanelListPakaian);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "lihatSemuaPakaian";
            this.Size = new System.Drawing.Size(903, 641);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.flowLayoutPanelListPakaian.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox tbSearchPakaian;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelListPakaian;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelListPakaian;
        private listPakaianPanel listPakaianPanel1;
        private listPakaianPanel listPakaianPanel2;
        private listPakaianPanel listPakaianPanel3;
        private listPakaianPanel listPakaianPanel4;
        private listPakaianPanel listPakaianPanel5;
        private listPakaianPanel listPakaianPanel6;
    }
}

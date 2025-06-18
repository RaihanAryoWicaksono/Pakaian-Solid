using System;

namespace PakaianForm.Views.Customer.Panel
{
    partial class lihatKeranjangPanel
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.listKeranjangPanel1 = new PakaianForm.Views.Customer.Panel.listKeranjangPanel();
            this.listKeranjangPanel2 = new PakaianForm.Views.Customer.Panel.listKeranjangPanel();
            this.listKeranjangPanel3 = new PakaianForm.Views.Customer.Panel.listKeranjangPanel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientButton1 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2Panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
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
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(918, 140);
            this.guna2Panel1.TabIndex = 0;
            // 
            // labelListPakaian
            // 
            this.labelListPakaian.BackColor = System.Drawing.Color.Transparent;
            this.labelListPakaian.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelListPakaian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelListPakaian.Location = new System.Drawing.Point(301, 50);
            this.labelListPakaian.Name = "labelListPakaian";
            this.labelListPakaian.Size = new System.Drawing.Size(378, 56);
            this.labelListPakaian.TabIndex = 14;
            this.labelListPakaian.Text = "Checkout Sekarang!";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.listKeranjangPanel1);
            this.flowLayoutPanel1.Controls.Add(this.listKeranjangPanel2);
            this.flowLayoutPanel1.Controls.Add(this.listKeranjangPanel3);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 146);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(918, 406);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // listKeranjangPanel1
            // 
            this.listKeranjangPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.listKeranjangPanel1.CartItem = null;
            this.listKeranjangPanel1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listKeranjangPanel1.ItemIndex = 0;
            this.listKeranjangPanel1.Location = new System.Drawing.Point(4, 5);
            this.listKeranjangPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listKeranjangPanel1.Name = "listKeranjangPanel1";
            this.listKeranjangPanel1.Size = new System.Drawing.Size(907, 91);
            this.listKeranjangPanel1.TabIndex = 0;
            this.listKeranjangPanel1.Load += new System.EventHandler(this.listKeranjangPanel1_Load);
            // 
            // listKeranjangPanel2
            // 
            this.listKeranjangPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.listKeranjangPanel2.CartItem = null;
            this.listKeranjangPanel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listKeranjangPanel2.ItemIndex = 0;
            this.listKeranjangPanel2.Location = new System.Drawing.Point(4, 106);
            this.listKeranjangPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listKeranjangPanel2.Name = "listKeranjangPanel2";
            this.listKeranjangPanel2.Size = new System.Drawing.Size(907, 91);
            this.listKeranjangPanel2.TabIndex = 1;
            // 
            // listKeranjangPanel3
            // 
            this.listKeranjangPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.listKeranjangPanel3.CartItem = null;
            this.listKeranjangPanel3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listKeranjangPanel3.ItemIndex = 0;
            this.listKeranjangPanel3.Location = new System.Drawing.Point(4, 207);
            this.listKeranjangPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listKeranjangPanel3.Name = "listKeranjangPanel3";
            this.listKeranjangPanel3.Size = new System.Drawing.Size(907, 91);
            this.listKeranjangPanel3.TabIndex = 2;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.guna2GradientButton1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 550);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(918, 100);
            this.guna2Panel2.TabIndex = 2;
            // 
            // guna2GradientButton1
            // 
            this.guna2GradientButton1.Animated = true;
            this.guna2GradientButton1.AutoRoundedCorners = true;
            this.guna2GradientButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton1.FillColor = System.Drawing.Color.ForestGreen;
            this.guna2GradientButton1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.guna2GradientButton1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GradientButton1.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton1.Location = new System.Drawing.Point(705, 26);
            this.guna2GradientButton1.Name = "guna2GradientButton1";
            this.guna2GradientButton1.Size = new System.Drawing.Size(180, 45);
            this.guna2GradientButton1.TabIndex = 0;
            this.guna2GradientButton1.Text = "Checkout";
            // 
            // lihatKeranjangPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "lihatKeranjangPanel";
            this.Size = new System.Drawing.Size(918, 650);
            this.Load += new System.EventHandler(this.lihatKeranjangPanel_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void listKeranjangPanel1_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelListPakaian;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton1;
        private listKeranjangPanel listKeranjangPanel1;
        private listKeranjangPanel listKeranjangPanel2;
        private listKeranjangPanel listKeranjangPanel3;
    }
}

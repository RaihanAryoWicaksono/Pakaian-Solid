﻿// PakaianForm/Views/Customer/Panel/lihatKeranjangPanel.Designer.cs
namespace PakaianForm.Views.Customer.Panel
{
    partial class lihatKeranjangPanel
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
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelTopHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCheckoutTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBottomCheckout = new Guna.UI2.WinForms.Guna2Panel();
            this.lblJumlahItem = new System.Windows.Forms.Label();
            this.lblTotalHarga = new System.Windows.Forms.Label();
            this.btnCheckout = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panelTopHeader.SuspendLayout();
            this.panelBottomCheckout.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // panelTopHeader
            // 
            this.panelTopHeader.BackColor = System.Drawing.Color.White;
            this.panelTopHeader.Controls.Add(this.lblCheckoutTitle);
            this.panelTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopHeader.Location = new System.Drawing.Point(0, 0);
            this.panelTopHeader.Name = "panelTopHeader";
            this.panelTopHeader.Size = new System.Drawing.Size(918, 140);
            this.panelTopHeader.TabIndex = 0;
            // 
            // lblCheckoutTitle
            // 
            this.lblCheckoutTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckoutTitle.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckoutTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblCheckoutTitle.Location = new System.Drawing.Point(301, 50);
            this.lblCheckoutTitle.Name = "lblCheckoutTitle";
            this.lblCheckoutTitle.Size = new System.Drawing.Size(378, 56);
            this.lblCheckoutTitle.TabIndex = 14;
            this.lblCheckoutTitle.Text = "Checkout Sekarang!";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 146);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(918, 406);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panelBottomCheckout
            // 
            this.panelBottomCheckout.Controls.Add(this.lblJumlahItem);
            this.panelBottomCheckout.Controls.Add(this.lblTotalHarga);
            this.panelBottomCheckout.Controls.Add(this.btnCheckout);
            this.panelBottomCheckout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomCheckout.Location = new System.Drawing.Point(0, 550);
            this.panelBottomCheckout.Name = "panelBottomCheckout";
            this.panelBottomCheckout.Size = new System.Drawing.Size(918, 100);
            this.panelBottomCheckout.TabIndex = 2;
            // 
            // lblJumlahItem
            // 
            this.lblJumlahItem.AutoSize = true;
            this.lblJumlahItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJumlahItem.Location = new System.Drawing.Point(50, 50);
            this.lblJumlahItem.Name = "lblJumlahItem";
            this.lblJumlahItem.Size = new System.Drawing.Size(71, 28);
            this.lblJumlahItem.TabIndex = 2;
            this.lblJumlahItem.Text = "Item: 0";
            // 
            // lblTotalHarga
            // 
            this.lblTotalHarga.AutoSize = true;
            this.lblTotalHarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHarga.Location = new System.Drawing.Point(50, 20);
            this.lblTotalHarga.Name = "lblTotalHarga";
            this.lblTotalHarga.Size = new System.Drawing.Size(129, 32);
            this.lblTotalHarga.TabIndex = 1;
            this.lblTotalHarga.Text = "Total: Rp0";
            // 
            // btnCheckout
            // 
            this.btnCheckout.Animated = true;
            this.btnCheckout.AutoRoundedCorners = true;
            this.btnCheckout.BorderRadius = 21;
            this.btnCheckout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckout.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckout.FillColor = System.Drawing.Color.ForestGreen;
            this.btnCheckout.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(705, 26);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(180, 45);
            this.btnCheckout.TabIndex = 0;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click_1);
            // 
            // lihatKeranjangPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelBottomCheckout);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelTopHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "lihatKeranjangPanel";
            this.Size = new System.Drawing.Size(918, 650);
            this.Load += new System.EventHandler(this.lihatKeranjangPanel_Load);
            this.panelTopHeader.ResumeLayout(false);
            this.panelTopHeader.PerformLayout();
            this.panelBottomCheckout.ResumeLayout(false);
            this.panelBottomCheckout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // --- DEKLARASI VARIABEL KONTROL ---
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel panelTopHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCheckoutTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel panelBottomCheckout;
        private Guna.UI2.WinForms.Guna2GradientButton btnCheckout;
        private System.Windows.Forms.Label lblTotalHarga;
        private System.Windows.Forms.Label lblJumlahItem;
    }
}

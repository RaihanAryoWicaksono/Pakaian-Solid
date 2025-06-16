using PakaianForm.Views.Customer.Panel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianForm.Views.Customer
{
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainerCustomer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainerCustomer.Controls.Add(kontrol);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomerLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            panelKontainerCustomer.Controls.Clear();

            lihatSemuaPakaian lihatPakaian = new lihatSemuaPakaian();
            lihatPakaian.Dock = DockStyle.Fill;
            panelKontainerCustomer.Controls.Add(lihatPakaian);
        }

        private void btnCustomerLihatKeranjang_Click(object sender, EventArgs e)
        {

        }
    }
}

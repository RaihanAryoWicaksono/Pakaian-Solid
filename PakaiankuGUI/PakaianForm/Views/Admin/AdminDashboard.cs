using PakaianForm.Panel;
using PakaianForm.Views.Admin.Panel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianForm.Views.Admin
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        public void TampilkanKontrol(Control kontrol)
        {
            panelKontainer.Controls.Clear();
            kontrol.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kontrol);
        }

        private void btnAdminLihatSemuaPakaian_Click(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            KelolaPakaian kelolaPakaian = new KelolaPakaian();
            kelolaPakaian.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(kelolaPakaian);
        }
        

        private void btnKelolaKatalogPakaian_Click(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            panelEditPakaian editPakaian = new panelEditPakaian();
            editPakaian.Dock = DockStyle.Fill;
            panelKontainer.Controls.Add(editPakaian);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

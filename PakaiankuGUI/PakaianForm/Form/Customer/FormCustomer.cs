using PakaianForm.Form.Admin;
using PakaianForm.Panel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianApp
{
    public partial class FormCustomer: Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void button1_sClick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            panelCariPakaian panelScroll = new panelCariPakaian();
            panelScroll.Dock = DockStyle.Fill; // atau atur ukuran manual
            panelKontainer.Controls.Add(panelScroll);
        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panelKontainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelKontainer.Controls.Clear();

            KelolaKatalog panelScroll = new KelolaKatalog();
            panelScroll.Dock = DockStyle.Fill; // atau atur ukuran manual
            panelKontainer.Controls.Add(panelScroll);
        }
    }

}

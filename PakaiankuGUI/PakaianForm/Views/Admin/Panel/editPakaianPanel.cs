using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakaianForm.Views.Admin.Panel
{
    public partial class editPakaianPanel : UserControl
    {

        public event EventHandler BackToKelolaPakaian;

        public editPakaianPanel()
        {
            InitializeComponent();
        }

        private void namaLabel_Click(object sender, EventArgs e)
        {

        }

        private void namaTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void namaTB_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BackToKelolaPakaian?.Invoke(this, EventArgs.Empty);

        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }
    }
}

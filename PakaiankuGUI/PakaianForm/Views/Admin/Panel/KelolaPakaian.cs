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
    public partial class KelolaPakaian : UserControl
    {
        public event Action<UserControl> OnNavigateToPanel;
        public KelolaPakaian()
        {
            InitializeComponent();
            flowLayoutPanelBarang.AutoScroll = true;
            flowLayoutPanelBarang.WrapContents = true;
            SetupEditButtons();
        }


        private void SetupEditButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button && (control.Name.ToLower().Contains("edit") || control.Text.ToLower() == "edit"))
                {
                    Button editBtn = (Button)control;
                    editBtn.Click += EditButton_Click;
                }

                FindAndSetupEditButtons(control);
            }
        }

        private void FindAndSetupEditButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button && (control.Name.ToLower().Contains("edit") || control.Text.ToLower() == "edit"))
                {
                    Button editBtn = (Button)control;
                    editBtn.Click -= EditButton_Click;
                    editBtn.Click += EditButton_Click;
                }

                if (control.HasChildren)
                {
                    FindAndSetupEditButtons(control);
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            editPakaianPanel editPanel = new editPakaianPanel();

            OnNavigateToPanel?.Invoke(editPanel);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelItem1_Load(object sender, EventArgs e)
        {

        }
    }
}

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
using WinForms = System.Windows.Forms;


namespace PakaianForm.Form.Admin.Katalog
{
    public partial class editPakaianPanel : UserControl
    {
        public editPakaianPanel()
        {
            InitializeComponent();
        }

        private void kembaliBTN_Click(object sender, EventArgs e)
        {
            try
            {
                // Gunakan this.Parent sebagai container
                Control panelKontainer = this.Parent;

                // Cek apakah parent ada
                if (panelKontainer == null)
                {
                    MessageBox.Show("Parent container tidak ditemukan!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                panelKontainer.Controls.Clear();
                KelolaKatalog kelolaKatalog = new KelolaKatalog();
                kelolaKatalog.Dock = DockStyle.Fill;
                panelKontainer.Controls.Add(kelolaKatalog);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat kembali ke katalog: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Alternative method jika masih ada masalah dengan Parent
        private void kembaliBTN_Click_Alternative(object sender, EventArgs e)
        {
            try
            {
                // Cari form parent
                WinForms.Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    // Cari panel utama dalam form
                    Control mainPanel = FindControlByName(parentForm, "mainPanel") ??
                                      FindControlByType<WinForms.Panel>(parentForm);

                    if (mainPanel != null)
                    {
                        mainPanel.Controls.Clear();
                        KelolaKatalog kelolaKatalog = new KelolaKatalog();
                        kelolaKatalog.Dock = DockStyle.Fill;
                        mainPanel.Controls.Add(kelolaKatalog);
                        return;
                    }
                }

                // Fallback: gunakan parent langsung
                if (this.Parent != null)
                {
                    this.Parent.Controls.Clear();
                    KelolaKatalog kelolaKatalog = new KelolaKatalog();
                    kelolaKatalog.Dock = DockStyle.Fill;
                    this.Parent.Controls.Add(kelolaKatalog);
                }
                else
                {
                    MessageBox.Show("Tidak dapat menemukan container yang valid!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method untuk mencari control berdasarkan nama
        private Control FindControlByName(Control parent, string name)
        {
            if (parent.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return parent;

            foreach (Control child in parent.Controls)
            {
                Control found = FindControlByName(child, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        // Helper method untuk mencari control berdasarkan type
        private T FindControlByType<T>(Control parent) where T : Control
        {
            if (parent is T)
                return parent as T;

            foreach (Control child in parent.Controls)
            {
                T found = FindControlByType<T>(child);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}

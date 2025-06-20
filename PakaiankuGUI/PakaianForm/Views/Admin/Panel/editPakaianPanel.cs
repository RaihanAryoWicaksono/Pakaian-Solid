// PakaianForm/Views/Admin/Panel/panelEditPakaian.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using PakaianForm.Models; // Untuk DTO (PakaianDtos)
using PakaianForm.Services;
using System.Linq; // Untuk Enum.GetNames

namespace PakaianForm.Views.Admin.Panel
{
    public partial class panelEditPakaian : UserControl
    {
        private PakaianDto _pakaianToEdit;

        public event Action<PakaianDto> OnPakaianUpdated;
        public event Action<UserControl> OnNavigateToPanel;

        // Kontrol dideklarasikan di Designer.cs. Akses langsung melalui namanya.

        public panelEditPakaian(PakaianDto pakaian)
        {
            InitializeComponent();
            _pakaianToEdit = pakaian;

            InitializeEventHandlers();

            // Inisialisasi ComboBox Status
            if (cbStatus != null)
            {
                cbStatus.DataSource = Enum.GetNames(typeof(StatusPakaian)).ToList();
            }

            this.Load += PanelEditPakaian_Load;
        }

        private void InitializeEventHandlers()
        {
            if (btnSaveEditPakaian != null) btnSaveEditPakaian.Click += BtnSimpan_Click;
            if (btnResetPakaian != null) btnResetPakaian.Click += BtnReset_Click;
            if (btnBackPakaian != null) btnBackPakaian.Click += BtnBack_Click;

            // Event Handlers untuk Validasi Input Numerik
            if (guna2TextBox6 != null) guna2TextBox6.KeyPress += NumericTextBox_KeyPress_Decimal;
            if (guna2TextBox7 != null) guna2TextBox7.KeyPress += NumericTextBox_KeyPress_Integer;
        }


        private void PanelEditPakaian_Load(object sender, EventArgs e)
        {
            if (_pakaianToEdit != null)
            {
                if (guna2TextBox1 != null)
                {
                    guna2TextBox1.Text = _pakaianToEdit.Kode;
                    guna2TextBox1.ReadOnly = true;
                }
                if (guna2TextBox2 != null) guna2TextBox2.Text = _pakaianToEdit.Nama;
                if (guna2TextBox3 != null) guna2TextBox3.Text = _pakaianToEdit.Kategori;
                if (guna2TextBox4 != null) guna2TextBox4.Text = _pakaianToEdit.Warna;
                if (guna2TextBox5 != null) guna2TextBox5.Text = _pakaianToEdit.Ukuran;
                if (guna2TextBox6 != null) guna2TextBox6.Text = _pakaianToEdit.Harga.ToString();
                if (guna2TextBox7 != null) guna2TextBox7.Text = _pakaianToEdit.Stok.ToString();

                if (cbStatus != null)
                {
                    string statusString = _pakaianToEdit.Status.ToString();
                    if (cbStatus.Items.Contains(statusString))
                    {
                        cbStatus.SelectedItem = statusString;
                    }
                    else
                    {
                        cbStatus.SelectedIndex = -1;
                    }
                }

                LoadStaticImage();
            }
            else
            {
                // Mode Tambah Pakaian Baru
                if (guna2TextBox1 != null) guna2TextBox1.Clear();
                if (guna2TextBox2 != null) guna2TextBox2.Clear();
                if (guna2TextBox3 != null) guna2TextBox3.Clear();
                if (guna2TextBox4 != null) guna2TextBox4.Clear();
                if (guna2TextBox5 != null) guna2TextBox5.Clear();
                if (guna2TextBox6 != null) guna2TextBox6.Clear();
                if (guna2TextBox7 != null) guna2TextBox7.Clear();
                if (cbStatus != null) cbStatus.SelectedIndex = -1;
                LoadStaticImage();
            }
        }

        private void LoadStaticImage()
        {
            if (guna2PictureBox1 == null) return;

            try
            {
                if (Properties.Resources.pakaian != null) { guna2PictureBox1.Image = Properties.Resources.pakaian; }
                else if (Properties.Resources.tshirt != null) { guna2PictureBox1.Image = Properties.Resources.tshirt; }
                else { throw new InvalidOperationException("Tidak ada resource gambar pakaian ditemukan (pakaian.png atau tshirt.jpg)."); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat gambar statis: {ex.Message}");
                Bitmap bmp = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.LightGray, 0, 0, 100, 100);
                    g.DrawString("No Image", new Font("Arial", 10), Brushes.Black, 10, 40);
                }
                guna2PictureBox1.Image = bmp;
            }
        }

        private async void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            SetLoadingState(true);

            try
            {
                // Ambil status yang dipilih dari ComboBox
                StatusPakaian? selectedStatus = null;
                if (cbStatus != null && cbStatus.SelectedItem != null)
                {
                    selectedStatus = (StatusPakaian)Enum.Parse(typeof(StatusPakaian), cbStatus.SelectedItem.ToString());
                }

                UpdatePakaianDto updateDto = new UpdatePakaianDto
                {
                    Nama = guna2TextBox2?.Text?.Trim(),
                    Kategori = guna2TextBox3?.Text?.Trim(),
                    Warna = guna2TextBox4?.Text?.Trim(),
                    Ukuran = guna2TextBox5?.Text?.Trim(),
                    Harga = decimal.TryParse(guna2TextBox6?.Text, out decimal hargaVal) ? hargaVal : (decimal?)null,
                    Stok = int.TryParse(guna2TextBox7?.Text, out int stokVal) ? stokVal : (int?)null,
                    Status = (StatusPakaian)selectedStatus // <--- KIRIM STATUS KE API
                };

                PakaianDto updatedPakaian = await KatalogService.UpdatePakaianAsync(_pakaianToEdit.Kode, updateDto);

                MessageBox.Show($"Pakaian '{updatedPakaian.Nama}' berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnPakaianUpdated?.Invoke(updatedPakaian);
                OnNavigateToPanel?.Invoke(new KelolaPakaian());
            }
            catch (FormatException)
            {
                MessageBox.Show("Pastikan Harga dan Stok adalah angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan perubahan: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            PanelEditPakaian_Load(sender, e);
            MessageBox.Show("Form telah direset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            OnNavigateToPanel?.Invoke(new KelolaPakaian());
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1?.Text) || string.IsNullOrWhiteSpace(guna2TextBox2?.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox3?.Text) || string.IsNullOrWhiteSpace(guna2TextBox4?.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox5?.Text) || string.IsNullOrWhiteSpace(guna2TextBox6?.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox7?.Text) || cbStatus?.SelectedItem == null)
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(guna2TextBox6.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus angka positif yang valid!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (guna2TextBox6 != null) guna2TextBox6.Focus();
                return false;
            }

            if (!int.TryParse(guna2TextBox7.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Stok harus angka non-negatif yang valid!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (guna2TextBox7 != null) guna2TextBox7.Focus();
                return false;
            }

            return true;
        }

        private void SetLoadingState(bool loading)
        {
            if (guna2TextBox1 != null) guna2TextBox1.Enabled = !loading;
            if (guna2TextBox2 != null) guna2TextBox2.Enabled = !loading;
            if (guna2TextBox3 != null) guna2TextBox3.Enabled = !loading;
            if (guna2TextBox4 != null) guna2TextBox4.Enabled = !loading;
            if (guna2TextBox5 != null) guna2TextBox5.Enabled = !loading;
            if (guna2TextBox6 != null) guna2TextBox6.Enabled = !loading;
            if (guna2TextBox7 != null) guna2TextBox7.Enabled = !loading;
            if (cbStatus != null) cbStatus.Enabled = !loading;

            if (btnSaveEditPakaian != null) btnSaveEditPakaian.Enabled = !loading;
            if (btnResetPakaian != null) btnResetPakaian.Enabled = !loading;
            if (btnBackPakaian != null) btnBackPakaian.Enabled = !loading;

            this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }

        // Event Handler Baru untuk Validasi Input Numerik
        private void NumericTextBox_KeyPress_Decimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.' && e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (sender as Guna.UI2.WinForms.Guna2TextBox)?.Text.IndexOfAny(new char[] { '.', ',' }) > -1)
            {
                e.Handled = true;
            }
        }

        private void NumericTextBox_KeyPress_Integer(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

// PakaianForm/Views/Admin/Panel/panelAddPakaian.cs
using PakaianForm.Models; // Untuk DTO (PakaianDtos, CreatePakaianDto)
using PakaianForm.Services; // Untuk KatalogService
using System;
using System.Drawing;
using System.Linq; // Untuk Enum.GetNames
using System.Windows.Forms;
using UserSession = PakaianForm.Services.UserSession;

namespace PakaianForm.Views.Admin.Panel
{
    public partial class panelAddPakaian : UserControl
    {
        // Event untuk memberi tahu parent (AdminDashboard/KelolaPakaian) ketika pakaian berhasil ditambahkan
        public event Action<PakaianDto> OnPakaianAdded;
        public event Action<UserControl> OnNavigateToPanel; // Untuk navigasi kembali

        // --- SANGAT PENTING: TIDAK ADA LAGI DEKLARASI VARIABEL KONTROL DI SINI ---
        // Variabel kontrol (e.g., guna2TextBox1, cbStatus, guna2PictureBox1, btnSavePakaian, etc.)
        // dideklarasikan sebagai 'private' di panelAddPakaian.Designer.cs
        // dan diinisialisasi oleh InitializeComponent().
        // Anda dapat mengaksesnya langsung dengan nama yang ada di Designer.cs setelah InitializeComponent() dipanggil.

        public panelAddPakaian()
        {
            InitializeComponent(); // <-- Ini menginisialisasi SEMUA kontrol dari Designer.cs

            // Set judul panel
            if (labelJudulEdit != null) labelJudulEdit.Text = "Tambah Pakaian Baru";

            // Hubungkan event handlers setelah semua komponen diinisialisasi oleh InitializeComponent()
            InitializeEventHandlers();

            // Inisialisasi ComboBox Status
            if (cbStatus != null) // Pastikan cbStatus ada dan diinisialisasi oleh desainer
            {
                cbStatus.DataSource = Enum.GetNames(typeof(StatusPakaian)).ToList();
            }

            this.Load += PanelAddPakaian_Load;
        }

        private void InitializeEventHandlers()
        {
            // Hubungkan event handlers ke tombol
            // Pastikan kontrol-kontrol ini dideklarasikan di Designer.cs dan sudah diinisialisasi.
            if (btnSavePakaian != null) btnSavePakaian.Click += BtnSimpan_Click;
            if (btnResetPakaian != null) btnResetPakaian.Click += BtnReset_Click;
            if (btnBackPakaian != null) btnBackPakaian.Click += BtnBack_Click;

            // --- Tambahan: Event Handlers untuk Validasi Input Numerik ---
            // Pastikan ini ditambahkan ke properti yang benar (guna2TextBox6 untuk Harga, guna2TextBox7 untuk Stok)
            if (guna2TextBox6 != null) guna2TextBox6.KeyPress += NumericTextBox_KeyPress_Decimal; // Untuk Harga
            if (guna2TextBox7 != null) guna2TextBox7.KeyPress += NumericTextBox_KeyPress_Integer; // Untuk Stok
            // --- Akhir Tambahan ---
        }

        private void PanelAddPakaian_Load(object sender, EventArgs e)
        {
            // Mode Tambah Pakaian Baru: Bersihkan form saat dimuat
            ClearForm();
            LoadStaticImage(); // Muat gambar statis default
        }

        private void ClearForm()
        {
            // Menggunakan nama kontrol yang sesuai dari Designer.cs Anda.
            // Pastikan kontrol ada sebelum membersihkan.
            if (guna2TextBox1 != null) guna2TextBox1.Clear();
            if (guna2TextBox2 != null) guna2TextBox2.Clear();
            if (guna2TextBox3 != null) guna2TextBox3.Clear();
            if (guna2TextBox4 != null) guna2TextBox4.Clear();
            if (guna2TextBox5 != null) guna2TextBox5.Clear();
            if (guna2TextBox6 != null) guna2TextBox6.Clear();
            if (guna2TextBox7 != null) guna2TextBox7.Clear();
            if (cbStatus != null) cbStatus.SelectedIndex = -1; // Pilih kosong
        }

        private void LoadStaticImage()
        {
            if (guna2PictureBox1 == null) return; // Pastikan guna2PictureBox1 diinisialisasi

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

            // --- Periksa apakah user login dan admin sebelum mencoba menambah ---
            if (!UserSession.IsLoggedIn || Services.UserSession.Role != UserRole.Admin)
            {
                MessageBox.Show("Anda tidak memiliki izin untuk menambahkan pakaian. Hanya Admin yang dapat melakukan ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // --- Akhir Periksa ---

            SetLoadingState(true);

            try
            {
                CreatePakaianDto createDto = new CreatePakaianDto
                {
                    Kode = guna2TextBox1?.Text?.Trim(),
                    Nama = guna2TextBox2?.Text?.Trim(),
                    Kategori = guna2TextBox3?.Text?.Trim(),
                    Warna = guna2TextBox4?.Text?.Trim(),
                    Ukuran = guna2TextBox5?.Text?.Trim(),
                    Harga = decimal.Parse(guna2TextBox6?.Text),
                    Stok = int.Parse(guna2TextBox7?.Text),
                };

                // Panggil API untuk menambah pakaian baru
                PakaianDto newPakaian = await KatalogService.AddPakaianAsync(createDto);

                MessageBox.Show($"Pakaian '{newPakaian.Nama}' berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnPakaianAdded?.Invoke(newPakaian); // Beri tahu parent bahwa pakaian baru ditambahkan
                OnNavigateToPanel?.Invoke(new KelolaPakaian()); // Kembali ke daftar pakaian
            }
            catch (FormatException)
            {
                MessageBox.Show("Pastikan Harga dan Stok adalah angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan pakaian baru: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            MessageBox.Show("Form telah direset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            OnNavigateToPanel?.Invoke(new KelolaPakaian());
        }

        private bool ValidateInput()
        {
            // Validasi input
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
            // Menggunakan nama kontrol yang sesuai dari Designer.cs Anda.
            if (guna2TextBox1 != null) guna2TextBox1.Enabled = !loading;
            if (guna2TextBox2 != null) guna2TextBox2.Enabled = !loading;
            if (guna2TextBox3 != null) guna2TextBox3.Enabled = !loading;
            if (guna2TextBox4 != null) guna2TextBox4.Enabled = !loading;
            if (guna2TextBox5 != null) guna2TextBox5.Enabled = !loading;
            if (guna2TextBox6 != null) guna2TextBox6.Enabled = !loading;
            if (guna2TextBox7 != null) guna2TextBox7.Enabled = !loading;
            if (cbStatus != null) cbStatus.Enabled = !loading;

            if (btnSavePakaian != null) btnSavePakaian.Enabled = !loading;
            if (btnResetPakaian != null) btnResetPakaian.Enabled = !loading;
            if (btnBackPakaian != null) btnBackPakaian.Enabled = !loading;

            this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }

        // --- Event Handler Baru untuk Validasi Input Numerik ---
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

        private void labelJudulEdit_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnResetPakaian_Click(object sender, EventArgs e)
        {

        }

        private void btnSavePakaian_Click(object sender, EventArgs e)
        {

        }

        private void btnBackPakaian_Click(object sender, EventArgs e)
        {

        }
    }
}

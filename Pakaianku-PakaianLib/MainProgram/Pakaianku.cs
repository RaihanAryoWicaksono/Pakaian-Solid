using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PakaianLib;


namespace Pakaianku
{
    public class Pakaian
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Warna { get; set; }
        public string Ukuran { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public StatusPakaian Status { get; set; }

        private Dictionary<(StatusPakaian, AksiPakaian), StatusPakaian> transisiStatus;

        public Pakaian()
        {
            InisialisasiTabelTransisi();
        }

        public Pakaian(string kode, string nama, string kategori, string warna, string ukuran, decimal harga, int stok)
        {
            Kode = kode;
            Nama = nama;
            Kategori = kategori;
            Warna = warna;
            Ukuran = ukuran;
            Harga = harga;
            Stok = stok;
            Status = stok > 0 ? StatusPakaian.Tersedia : StatusPakaian.TidakTersedia;

            InisialisasiTabelTransisi();
        }

        private void InisialisasiTabelTransisi()
        {
            transisiStatus = new Dictionary<(StatusPakaian, AksiPakaian), StatusPakaian>
            {
                { (StatusPakaian.Tersedia, AksiPakaian.TambahKeKeranjang), StatusPakaian.DalamKeranjang },
                { (StatusPakaian.Tersedia, AksiPakaian.HabisStok), StatusPakaian.TidakTersedia },

                { (StatusPakaian.DalamKeranjang, AksiPakaian.KeluarkanDariKeranjang), StatusPakaian.Tersedia },
                { (StatusPakaian.DalamKeranjang, AksiPakaian.Pesan), StatusPakaian.Dipesan },

                { (StatusPakaian.Dipesan, AksiPakaian.BatalPesan), StatusPakaian.Tersedia },
                { (StatusPakaian.Dipesan, AksiPakaian.Bayar), StatusPakaian.Dibayar },

                { (StatusPakaian.Dibayar, AksiPakaian.Kirim), StatusPakaian.DalamPengiriman },

                { (StatusPakaian.DalamPengiriman, AksiPakaian.TerimaPakaian), StatusPakaian.Diterima },

                { (StatusPakaian.Diterima, AksiPakaian.KembalikanPakaian), StatusPakaian.Dikembalikan },

                { (StatusPakaian.Dikembalikan, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia },

                { (StatusPakaian.TidakTersedia, AksiPakaian.RestokPakaian), StatusPakaian.Tersedia }
            };
        }

        public bool ProsesAksi(AksiPakaian aksi)
        {
            var kunciTransisi = (Status, aksi);

            if (transisiStatus.ContainsKey(kunciTransisi))
            {
                switch (aksi)
                {
                    case AksiPakaian.TambahKeKeranjang:
                        Stok--;
                        break;
                    case AksiPakaian.KeluarkanDariKeranjang:
                    case AksiPakaian.BatalPesan:
                    case AksiPakaian.KembalikanPakaian:
                        Stok++;
                        break;
                    case AksiPakaian.RestokPakaian:
                        Stok += 10;
                        break;
                    case AksiPakaian.HabisStok:
                        Stok = 0;
                        break;
                }

                Status = transisiStatus[kunciTransisi];
                Console.WriteLine($"Pakaian '{Nama}' sekarang dalam status: {Status}, Stok: {Stok}");
                return true;
            }
            else
            {
                Console.WriteLine($"Aksi {aksi} tidak valid untuk pakaian '{Nama}' dalam status {Status}");
                return false;
            }
        }

        public List<AksiPakaian> GetAksiValid()
        {
            List<AksiPakaian> aksiValid = new List<AksiPakaian>();

            foreach (var transisi in transisiStatus)
            {
                if (transisi.Key.Item1 == Status)
                {
                    aksiValid.Add(transisi.Key.Item2);
                }
            }

            return aksiValid;
        }

        public override string ToString()
        {
            return $"Kode: {Kode}, Nama: {Nama}, Kategori: {Kategori}, " +
                   $"Warna: {Warna}, Ukuran: {Ukuran}, " +
                   $"Harga: Rp{Harga:N0}, Stok: {Stok}, Status: {Status}";
        }
    }

    public class KatalogPakaian
    {
        private readonly string _connectionString;

        public KatalogPakaian(string connectionString)
        {
            _connectionString = connectionString;
            InisialisasiDatabase();
        }

        private void InisialisasiDatabase()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Pakaian (
                        Kode VARCHAR(50) PRIMARY KEY,
                        Nama VARCHAR(255) NOT NULL,
                        Kategori VARCHAR(100),
                        Warna VARCHAR(50),
                        Ukuran VARCHAR(20),
                        Harga DECIMAL(10, 2) NOT NULL,
                        Stok INT NOT NULL,
                        Status INT NOT NULL
                    );";
                using (var command = new MySqlCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void TambahPakaian(Pakaian pakaian)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string insertSql = @"
                    INSERT INTO Pakaian (Kode, Nama, Kategori, Warna, Ukuran, Harga, Stok, Status)
                    VALUES (@Kode, @Nama, @Kategori, @Warna, @Ukuran, @Harga, @Stok, @Status);";
                using (var command = new MySqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Kode", pakaian.Kode);
                    command.Parameters.AddWithValue("@Nama", pakaian.Nama);
                    command.Parameters.AddWithValue("@Kategori", pakaian.Kategori);
                    command.Parameters.AddWithValue("@Warna", pakaian.Warna);
                    command.Parameters.AddWithValue("@Ukuran", pakaian.Ukuran);
                    command.Parameters.AddWithValue("@Harga", pakaian.Harga);
                    command.Parameters.AddWithValue("@Stok", pakaian.Stok);
                    command.Parameters.AddWithValue("@Status", (int)pakaian.Status);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Pakaian CariPakaianByKode(string kode)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Pakaian WHERE Kode = @Kode;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@Kode", kode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPakaianFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Pakaian> CariPakaian(string keyword)
        {
            List<Pakaian> hasilPencarian = new List<Pakaian>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string selectSql = @"
                    SELECT * FROM Pakaian
                    WHERE Nama LIKE @Keyword OR Kategori LIKE @Keyword OR
                          Warna LIKE @Keyword OR Ukuran LIKE @Keyword OR
                          Kode LIKE @Keyword;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasilPencarian.Add(MapPakaianFromReader(reader));
                        }
                    }
                }
            }
            return hasilPencarian;
        }

        public List<Pakaian> CariPakaianByHarga(decimal min, decimal max)
        {
            List<Pakaian> hasilPencarian = new List<Pakaian>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Pakaian WHERE Harga >= @MinHarga AND Harga <= @MaxHarga;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@MinHarga", min);
                    command.Parameters.AddWithValue("@MaxHarga", max);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasilPencarian.Add(MapPakaianFromReader(reader));
                        }
                    }
                }
            }
            return hasilPencarian;
        }

        public List<Pakaian> CariPakaianByKategori(string kategori)
        {
            List<Pakaian> hasilPencarian = new List<Pakaian>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Pakaian WHERE Kategori LIKE @Kategori;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@Kategori", $"%{kategori}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasilPencarian.Add(MapPakaianFromReader(reader));
                        }
                    }
                }
            }
            return hasilPencarian;
        }

        public List<Pakaian> GetSemuaPakaian()
        {
            List<Pakaian> semuaPakaian = new List<Pakaian>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Pakaian;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            semuaPakaian.Add(MapPakaianFromReader(reader));
                        }
                    }
                }
            }
            return semuaPakaian;
        }

        public void TampilkanSemuaPakaian()
        {
            Console.WriteLine("\n=== KATALOG PAKAIAN ===");
            List<Pakaian> semuaPakaian = GetSemuaPakaian();
            if (semuaPakaian.Count == 0)
            {
                Console.WriteLine("Katalog kosong.");
                return;
            }

            foreach (var pakaian in semuaPakaian)
            {
                Console.WriteLine(pakaian.ToString());
            }
        }

        public void TampilkanPakaian(List<Pakaian> pakaianList)
        {
            if (pakaianList.Count == 0)
            {
                Console.WriteLine("Tidak ada pakaian yang ditemukan.");
                return;
            }

            foreach (var pakaian in pakaianList)
            {
                Console.WriteLine(pakaian.ToString());
            }
        }

        public bool HapusPakaian(string kode)
        {
            var pakaian = CariPakaianByKode(kode);
            if (pakaian == null)
            {
                Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan.");
                return false;
            }

            if (pakaian.Status != StatusPakaian.Tersedia && pakaian.Status != StatusPakaian.TidakTersedia)
            {
                Console.WriteLine($"Pakaian '{pakaian.Nama}' dengan status '{pakaian.Status}' tidak dapat dihapus.");
                return false;
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string deleteSql = "DELETE FROM Pakaian WHERE Kode = @Kode;";
                using (var command = new MySqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@Kode", kode);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdatePakaian(string kode, string nama = null, string kategori = null,
                                    string warna = null, string ukuran = null,
                                    decimal? harga = null, int? stok = null, StatusPakaian? status = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                Pakaian existingPakaian = CariPakaianByKode(kode);
                if (existingPakaian == null)
                {
                    Console.WriteLine($"Pakaian dengan kode {kode} tidak ditemukan untuk diperbarui.");
                    return false;
                }

                List<string> setClauses = new List<string>();
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@Kode", kode);

                if (nama != null) { setClauses.Add("Nama = @Nama"); command.Parameters.AddWithValue("@Nama", nama); }
                if (kategori != null) { setClauses.Add("Kategori = @Kategori"); command.Parameters.AddWithValue("@Kategori", kategori); }
                if (warna != null) { setClauses.Add("Warna = @Warna"); command.Parameters.AddWithValue("@Warna", warna); }
                if (ukuran != null) { setClauses.Add("Ukuran = @Ukuran"); command.Parameters.AddWithValue("@Ukuran", ukuran); }
                if (harga.HasValue) { setClauses.Add("Harga = @Harga"); command.Parameters.AddWithValue("@Harga", harga.Value); }

                if (stok.HasValue)
                {
                    setClauses.Add("Stok = @Stok");
                    command.Parameters.AddWithValue("@Stok", stok.Value);

                    // If stock changes, trigger status update through the automata
                    if (stok.Value == 0 && existingPakaian.Stok > 0)
                    {
                        existingPakaian.ProsesAksi(AksiPakaian.HabisStok);
                        setClauses.Add("Status = @NewStatus");
                        command.Parameters.AddWithValue("@NewStatus", (int)existingPakaian.Status);
                    }
                    else if (stok.Value > 0 && existingPakaian.Stok == 0)
                    {
                        existingPakaian.ProsesAksi(AksiPakaian.RestokPakaian);
                        setClauses.Add("Status = @NewStatus");
                        command.Parameters.AddWithValue("@NewStatus", (int)existingPakaian.Status);
                    }
                }

                if (status.HasValue)
                {
                    setClauses.Add("Status = @Status");
                    command.Parameters.AddWithValue("@Status", (int)status.Value);
                }


                if (setClauses.Count == 0)
                {
                    Console.WriteLine($"Tidak ada perubahan yang diminta untuk pakaian dengan kode {kode}.");
                    return true;
                }

                string updateSql = $"UPDATE Pakaian SET {string.Join(", ", setClauses)} WHERE Kode = @Kode;";
                command.CommandText = updateSql;

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        private Pakaian MapPakaianFromReader(MySqlDataReader reader)
        {
            return new Pakaian
            {
                Kode = reader["Kode"].ToString(),
                Nama = reader["Nama"].ToString(),
                Kategori = reader["Kategori"].ToString(),
                Warna = reader["Warna"].ToString(),
                Ukuran = reader["Ukuran"].ToString(),
                Harga = reader.GetDecimal("Harga"),
                Stok = reader.GetInt32("Stok"),
                Status = (StatusPakaian)reader.GetInt32("Status")
            };
        }
    }

    public class KeranjangBelanja<T> where T : class
    {
        private List<T> items = new List<T>();

        public bool TambahKeKeranjang(T item)
        {
            items.Add(item);
            return true;
        }

        public bool KeluarkanDariKeranjangByIndex(int index)
        {
            if (index > 0 && index <= items.Count)
            {
                items.RemoveAt(index - 1);
                return true;
            }
            return false;
        }
        public void TampilkanKeranjang()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            Console.WriteLine("=== Keranjang Belanja ===");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }
        public int JumlahItem()
        {
            return items.Count;
        }

        public decimal HitungTotal()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                if (item is Pakaian pakaian)
                {
                    total += pakaian.Harga;
                }
            }
            return total;
        }

        public List<T> GetSemuaItem()
        {
            return items;
        }
        public void KosongkanKeranjang()
        {
            items.Clear();
        }
    }
}
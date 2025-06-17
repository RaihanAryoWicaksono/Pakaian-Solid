// PakaianApi/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using PakaianApi.Models;
using PakaianLib;
using System.Linq;

namespace PakaianApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pakaian> Pakaian { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurasi untuk properti enum di Pakaian dan User
            modelBuilder.Entity<Pakaian>()
                .Property(p => p.Status)
                .HasConversion<string>(); // Simpan enum sebagai string

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(); // Simpan enum sebagai string

            // Konfigurasi Primary Key untuk Pakaian (Kode)
            modelBuilder.Entity<Pakaian>()
                .HasKey(p => p.Kode);

            // Konfigurasi Primary Key untuk User (Id)
            // Ini akan secara otomatis dikenali oleh konvensi karena nama properti "Id"
            // Namun, jika ingin eksplisit:
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Membuat Username unik untuk menghindari duplikasi username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
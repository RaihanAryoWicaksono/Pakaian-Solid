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

            modelBuilder.Entity<Pakaian>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Pakaian>()
                .HasKey(p => p.Kode);

            // Menentukan Id sebagai primary key untuk User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Membuat Username unik untuk menghindari duplikasi username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}

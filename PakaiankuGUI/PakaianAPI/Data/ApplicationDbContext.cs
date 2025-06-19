// PakaianApi/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using PakaianApi.Models; // Untuk KeranjangBelanja, KeranjangItem, User, PakaianDto
using PakaianLib; // Untuk Pakaian (entitas inti)
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
        public DbSet<KeranjangBelanja> KeranjangBelanja { get; set; }
        public DbSet<KeranjangItem> KeranjangItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pakaian>()
                .HasKey(p => p.Kode);
            modelBuilder.Entity<Pakaian>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Konfigurasi KeranjangBelanja dan KeranjangItem
            modelBuilder.Entity<KeranjangBelanja>()
                .HasKey(kb => kb.Id);
            modelBuilder.Entity<KeranjangBelanja>()
                .HasMany(kb => kb.Items)
                .WithOne(ki => ki.KeranjangBelanja)
                .HasForeignKey(ki => ki.KeranjangBelanjaId);

            modelBuilder.Entity<KeranjangItem>()
                .HasKey(ki => ki.Id);
            modelBuilder.Entity<KeranjangItem>()
                .HasOne(ki => ki.Pakaian)
                .WithMany()
                .HasForeignKey(ki => ki.PakaianKode);
        }
    }
}

// PakaianApi/SeedData.cs
using PakaianApi.Models;
using PakaianLib;
using PakaianApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PakaianApi
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Seed Users
                if (!await context.Users.AnyAsync())
                {
                    await context.Users.AddRangeAsync(
                        // Id tidak perlu ditentukan, akan auto-generate
                        new User { Username = "admin", Password = "admin123", Role = UserRole.Admin },
                        new User { Username = "customer1", Password = "customerpassword", Role = UserRole.Customer },
                        new User { Username = "dimas", Password = "123", Role = UserRole.Admin },
                        new User { Username = "rudi", Password = "321", Role = UserRole.Customer }
                    );
                    await context.SaveChangesAsync();
                }

                // Seed Pakaian (dummy data)
                if (!await context.Pakaian.AnyAsync())
                {
                    await context.Pakaian.AddRangeAsync(
                        new Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10),
                        new Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8),
                        new Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5),
                        new Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15),
                        new Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12),
                        new Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7),
                        new Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8),
                        new Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6),
                        new Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10),
                        new Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5),
                        new Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4),
                        new Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7)
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
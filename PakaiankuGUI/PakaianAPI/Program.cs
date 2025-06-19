// PakaianApi/Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PakaianApi;
using PakaianApi.Data;
using PakaianAPI.Models;
using PakaianApi.Services;
using PakaianApi.Extensions;
using PakaianApi.Models;
using Pakaianku;
using PakaianApi.Services;
using PakaianLib;
using System;
using System.Linq;;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PakaianApi.Services.KatalogPakaian>();
builder.Services.AddScoped<IKeranjangService, KeranjangService>();


// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Konfigurasi DbContext untuk MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services for dependency injection
//builder.Services.AddScoped<KeranjangBelanja<Pakaianku.Pakaian>>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Lakukan migrasi database dan seed data saat startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Terjadi kesalahan saat melakukan migrasi atau seed data.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


// Method untuk inisialisasi katalog (sama seperti di CLI)
//static void InisialisasiKatalog(KatalogPakaian katalog)
//{
//    // Menambahkan pakaian ke katalog
//    katalog.TambahPakaian(new Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10));
//    katalog.TambahPakaian(new Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8));
//    katalog.TambahPakaian(new Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5));
//    katalog.TambahPakaian(new Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15));
//    katalog.TambahPakaian(new Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12));
//    katalog.TambahPakaian(new Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7));
//    katalog.TambahPakaian(new Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8));
//    katalog.TambahPakaian(new Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6));
//    katalog.TambahPakaian(new Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10));
//    katalog.TambahPakaian(new Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5));
//    katalog.TambahPakaian(new Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4));
//    katalog.TambahPakaian(new Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7));
//}
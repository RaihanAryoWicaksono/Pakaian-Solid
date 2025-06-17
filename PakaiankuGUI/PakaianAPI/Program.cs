// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PakaianApi.Data;
using PakaianApi.Models; // Pastikan models diimport
using PakaianApi.Extensions; // Pastikan extensions diimport
using PakaianLib; // Pastikan PakaianLib diimport
using System;
using System.Linq;
using PakaianApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Add JSON support

// Konfigurasi DbContext untuk MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services for dependency injection
// KatalogPakaian tidak lagi dibutuhkan sebagai Singleton karena data diambil dari DB
// builder.Services.AddSingleton<KatalogPakaian>(); // Hapus ini
builder.Services.AddSingleton<KeranjangBelanja<Pakaian>>(); // Keranjang masih in-memory

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation(); // Menggunakan extension method

var app = builder.Build();

// Lakukan migrasi database dan seed data saat startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Menerapkan migrasi yang tertunda
        await SeedData.Initialize(services); // Memanggil method seed data
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
    app.UseSwaggerDocumentation(); // Menggunakan extension method
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

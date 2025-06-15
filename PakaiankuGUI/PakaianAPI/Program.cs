using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PakaianLib;
using Pakaianku;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Add JSON support

// Register services for dependency injection
builder.Services.AddSingleton<KatalogPakaian>();
builder.Services.AddSingleton<KeranjangBelanja<Pakaian>>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pakaianku API",
        Version = "v1",
        Description = "API untuk sistem penjualan pakaian",
        Contact = new OpenApiContact
        {
            Name = "Pakaianku Developer",
            Email = "contact@pakaianku.com"
        }
    });

    // Set the comments path for the Swagger JSON and UI
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pakaianku API v1");
        c.RoutePrefix = string.Empty; // Swagger UI di root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inisialisasi katalog dengan data awal
var katalogService = app.Services.GetRequiredService<KatalogPakaian>();
InisialisasiKatalog(katalogService);

app.Run();

// Method untuk inisialisasi katalog (sama seperti di CLI)
static void InisialisasiKatalog(KatalogPakaian katalog)
{
    // Menambahkan pakaian ke katalog
    katalog.TambahPakaian(new Pakaian("KM001", "Kemeja Formal Pria", "Kemeja", "Putih", "L", 250000, 10));
    katalog.TambahPakaian(new Pakaian("KM002", "Kemeja Formal Pria", "Kemeja", "Biru", "M", 245000, 8));
    katalog.TambahPakaian(new Pakaian("KM003", "Kemeja Formal Pria", "Kemeja", "Hitam", "XL", 255000, 5));
    katalog.TambahPakaian(new Pakaian("KS001", "Kaos Premium", "Kaos", "Hitam", "M", 150000, 15));
    katalog.TambahPakaian(new Pakaian("KS002", "Kaos Premium", "Kaos", "Putih", "L", 155000, 12));
    katalog.TambahPakaian(new Pakaian("KS003", "Kaos Grafis", "Kaos", "Merah", "M", 180000, 7));
    katalog.TambahPakaian(new Pakaian("CL001", "Celana Jeans", "Celana", "Biru", "32", 350000, 8));
    katalog.TambahPakaian(new Pakaian("CL002", "Celana Chino", "Celana", "Khaki", "30", 320000, 6));
    katalog.TambahPakaian(new Pakaian("CL003", "Celana Pendek", "Celana", "Hitam", "34", 180000, 10));
    katalog.TambahPakaian(new Pakaian("JK001", "Jaket Bomber", "Jaket", "Hitam", "L", 450000, 5));
    katalog.TambahPakaian(new Pakaian("JK002", "Jaket Denim", "Jaket", "Biru", "M", 480000, 4));
    katalog.TambahPakaian(new Pakaian("JK003", "Jaket Hoodie", "Jaket", "Abu-abu", "XL", 375000, 7));
}
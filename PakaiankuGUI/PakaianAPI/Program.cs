// PakaianApi/Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PakaianApi;
using PakaianApi.Data;
using PakaianApi.Extensions;
using PakaianApi.Models;
using PakaianLib;
using System;
using System.Linq;
// Hapus using Microsoft.AspNetCore.Authentication.JwtBearer;
// Hapus using Microsoft.IdentityModel.Tokens;
// Hapus using System.Text;
// Hapus using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --- HAPUS SEMUA KONFIGURASI JWT AUTHENTICATION INI ---
// builder.Services.AddAuthentication(x => { /* ... */ }).AddJwtBearer(x => { /* ... */ });
// --- AKHIR HAPUS ---

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pakaianku API", Version = "v1" });
    // --- HAPUS KONFIGURASI KEAMANAN UNTUK SWAGGER UI INI ---
    // c.AddSecurityDefinition("Bearer", ...);
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement { ... });
    // --- AKHIR HAPUS ---
});


var app = builder.Build();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pakaianku API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();

// --- HAPUS MIDDLEWARE AUTHENTICATION DAN AUTHORIZATION INI ---
// app.UseAuthentication(); 
// app.UseAuthorization();  
// --- AKHIR HAPUS ---

app.MapControllers();

app.Run();

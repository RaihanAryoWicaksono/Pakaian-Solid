using Pakaianku; 
using PakaianLib; 
using Microsoft.Extensions.Configuration; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<KatalogPakaian>(sp => new KatalogPakaian(connectionString));

var app = builder.Build();

// Konfigurasi pipeline permintaan HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Memetakan controller API ke rute

app.Run();
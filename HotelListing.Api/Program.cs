// ASP.NET Core-Apps, die mit den Webvorlagen erstellt wurden, enthalten den Anwendungsstartcode in der Datei Program.cs.
// Für die Datei Program.cs gilt Folgendes: werden die von der App erforderlichen Dienste konfiguriert.
// Die Anforderungsverarbeitungspipeline der App ist als eine Reihe von Middlewarekomponenten definiert.

using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListlingDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Korrigierte Swagger-Konfiguration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Listing API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Listing API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
// ASP.NET Core-Apps, die mit den Webvorlagen erstellt wurden, enthalten den Anwendungsstartcode in der Datei Program.cs.
// Für die Datei Program.cs gilt Folgendes: werden die von der App erforderlichen Dienste konfiguriert.
// Die Anforderungsverarbeitungspipeline der App ist als eine Reihe von Middlewarekomponenten definiert.

using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ConnectionString im Program.cs definieren , dadurch weiß ASP.NET core wie es auf Datenbank zugreift
// builder.Configuration geht auf appsettings.json
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
// Db Context wird in die ApplicationStart eingefügt (ich nutze Sql Server da WIR SQL benutzen.)
builder.Services.AddDbContext<HotelListlingDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

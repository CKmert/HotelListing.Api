// ASP.NET Core-Apps, die mit den Webvorlagen erstellt wurden, enthalten den Anwendungsstartcode in der Datei Program.cs.
// Für die Datei Program.cs gilt Folgendes: werden die von der App erforderlichen Dienste konfiguriert.
// Die Anforderungsverarbeitungspipeline der App ist als eine Reihe von Middlewarekomponenten definiert.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

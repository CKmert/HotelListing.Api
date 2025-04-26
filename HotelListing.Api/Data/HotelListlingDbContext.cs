using Microsoft.EntityFrameworkCore;

// Basisklasse für den Datenbankkontext --> Klasse zur Verwaltung des Datenbankzugriffs
// Ein Konstruktor ist eine spezielle Methode innerhalb einer Klasse, die aufgerufen wird, wenn ein neues Objekt dieser Klasse erstellt wird.
// Der Konstruktor hat den gleichen Namen wie die Klasse und wird verwendet, um die Eigenschaften des Objekts initial zu setzen. 

namespace HotelListing.Api.Data
{
    public class HotelListlingDbContext : DbContext     // Unsere DBContext erbt die Basiseigenschaften von DbContext
    {
        public HotelListlingDbContext(DbContextOptions<HotelListlingDbContext> options) : base(options)
        {
            
        }
        public DbSet<Country> Countries { get; set; } // Representiert die Tabelle Country in der Datenbank
        public DbSet<Hotel> Hotels { get; set; }

    }
}

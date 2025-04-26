using HotelListing.Api.Controllers;

namespace HotelListing.Api.Data;

public class Hotel      // Klassenerstellung mit Propertiert (Eigenschaft)
{
    public int Id { get; set; } // EF Core weiß dass ID = Primary Key ist und macht autoinkrement also +1 
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }

    public int CountryId { get; set; } // 	Fremdschlüssel zur Country-Tabelle (z. B. ein Hotel liegt in Deutschland = ID 1)

    public Country? Country { get; set; } // Navigation-Eigenschaft: Damit kannst du auf das ganze Land zugreifen

}

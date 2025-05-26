namespace HotelListing.Api.Models.Hotel
{
    public class HotelDTO
    {
        
        
            public int Id { get; set; } // EF Core weiß dass ID = Primary Key ist und macht autoinkrement also +1 
            public string Name { get; set; }
            public string Address { get; set; }
            public double Rating { get; set; }
            public int CountryId { get; set; } // 	Fremdschlüssel zur Country-Tabelle (z. B. ein Hotel liegt in Deutschland = ID 1)

        
    }
}

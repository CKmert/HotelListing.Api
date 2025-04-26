namespace HotelListing.Api.Data;

public class Country
{
    public int Id { get; set; }

    public int Name { get; set; }

    public int ShortName { get; set; }

    public List<Hotel>? Hotels { get; set; } // One to many, ein Land kann mehrere Hotels haben 

     
}

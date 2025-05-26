using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Api.Models.Country
{
    public class GetCountryDTO : BaseCountryDTO         // Für die GET ALL Methode
    {
        public int Id { get; set; } 
       
    }

   
}

using HotelListing.Api.Models.Hotel;

namespace HotelListing.Api.Models.Country
{
    public class CountryDTO          // Detailierte Get Methode für einzelne ID --> detailreicher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public List<HotelDTO> Hotels { get; set; }
    }

   
}

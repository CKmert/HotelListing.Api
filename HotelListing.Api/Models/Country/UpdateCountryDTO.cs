namespace HotelListing.Api.Models.Country
{
    public class UpdateCountryDTO : BaseCountryDTO  // Erbt von BaseCountryDTO, ABER hat eine eigene Id Eigenschaft
    {
        public int Id { get; set; }
    }
}

using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using HotelListing.Api.Models.Hotel;

namespace HotelListing.Api.Configurations
{
    public class MapperConfig : Profile // Funktionale Eigenschaften von Profile erben
    {
        public MapperConfig()  // Konstruktor - das Objekt zu initialisieren, also die Anfangswerte für die Eigenschaften des Objekts festzulegen
        {
            CreateMap<Country, CreateCountryDTO>().ReverseMap(); // Mapping-Profile mappt von A nach B und vise verca
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();
        }
    }
}

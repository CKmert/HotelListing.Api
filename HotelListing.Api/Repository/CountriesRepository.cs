using HotelListing.Api.Contracts;
using HotelListing.Api.Data;

namespace HotelListing.Api.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository // Erbt von I CountriesRepository
    {
        public CountriesRepository(HotelListlingDbContext context) : base(context)
        {
        }
    }
}

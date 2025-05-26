using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository // Erbt von I CountriesRepository
    {
        private readonly HotelListlingDbContext _context;

        public CountriesRepository(HotelListlingDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);
            // Gehe in die Datenbank -> Tabele Countries -> füge die Liste mit Hotels der Countries
            // suche mir nach der Id und gebe mir diesen aus
        }
    }
}

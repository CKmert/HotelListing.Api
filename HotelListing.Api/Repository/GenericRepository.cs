using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class // GenericRepository erbt von Interface (Vertrag) T soll eine Klasse sein
    {
        private readonly HotelListlingDbContext _context;

        public GenericRepository(HotelListlingDbContext context)
        {
            this._context = context;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync() // T ist ein Platzhalter für eine Klasse/DATENTYP
        {
            return await _context.Set<T>().ToListAsync(); // DbSET = Datenbankauflistung/Einträge von Typ T -> spiegelt Tabelle
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        Task<T> IGenericRepository<T>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

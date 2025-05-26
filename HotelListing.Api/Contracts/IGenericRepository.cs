namespace HotelListing.Api.Contracts
{
    public interface IGenericRepository<T> where T : class // Vertrag welche Methoden gemacht werden müssen bei Erbung
    {
        Task<T> GetAsync(int? id); // T = Data models 
        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}

// Ein Namespace in C# ist eine Art von Container, der dazu dient, Klassen, Interfaces, Strukturen und andere Typen zu organisieren und zu gruppieren.
// Man kann sich einen Namespace wie einen Ordner auf deinem Computer vorstellen, in dem du verschiedene Dateien ablegst

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

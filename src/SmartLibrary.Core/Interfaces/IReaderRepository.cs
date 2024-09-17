using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface IReaderRepository
    {
        Task<Reader> GetByIdAsync(int id);
        Task AddAsync(Reader reader);
        Task UpdateAsync(Reader reader);
        Task DeleteAsync(int id);
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<IEnumerable<Reader>> GetReadersWithOverdueLoansAsync();
    }
}

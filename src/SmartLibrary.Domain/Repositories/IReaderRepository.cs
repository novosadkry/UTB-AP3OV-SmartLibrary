using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Repositories
{
    public interface IReaderRepository : IRepositoryBase<Reader>
    {
        Task<IEnumerable<Reader>> GetReadersWithOverdueLoansAsync();
    }
}

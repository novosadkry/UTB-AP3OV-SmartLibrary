using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface IReaderRepository : IRepositoryBase<Reader>
    {
        Task<IEnumerable<Reader>> GetReadersWithOverdueLoansAsync();
    }
}

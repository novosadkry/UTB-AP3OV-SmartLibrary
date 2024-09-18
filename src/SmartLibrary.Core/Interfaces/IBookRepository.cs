using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<Book?> GetByIsbnAsync(string isbn);
    }
}

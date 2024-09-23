using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<Book?> GetByIsbnAsync(string isbn);
    }
}

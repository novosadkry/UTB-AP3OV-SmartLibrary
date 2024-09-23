using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Repositories;
using SmartLibrary.Infrastructure.Data;

namespace SmartLibrary.Infrastructure.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _dbContext.Books
                .FirstAsync(book => book.Isbn == isbn);
        }
    }
}

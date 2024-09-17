using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Interfaces;
using SmartLibrary.Infrastructure.Data;

namespace SmartLibrary.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        private BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _dbContext.Books
                .FirstAsync(book => book.Isbn == isbn);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}

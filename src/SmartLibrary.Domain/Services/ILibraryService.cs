using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Repositories;

namespace SmartLibrary.Domain.Services
{
    public interface ILibraryService
    {
        Task<Loan> BorrowBookAsync(Book book, Reader reader);
        Task ReturnBookAsync(Loan loan);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> GetBookByIsbnAsync(string isbn);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<PagedResult<Book>> GetBooksAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Reader>> GetReadersAsync();
        Task<PagedResult<Reader>> GetReadersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Loan>> GetLoansAsync();
        Task<PagedResult<Loan>> GetLoansAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
    }
}

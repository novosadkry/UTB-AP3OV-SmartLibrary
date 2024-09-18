using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
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
    }
}

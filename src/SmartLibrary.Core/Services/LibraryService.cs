using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly INotificationService _notificationService;

        public LibraryService(
            IBookRepository bookRepository,
            ILoanRepository loanRepository,
            IReaderRepository readerRepository,
            INotificationService notificationService)
        {
            _bookRepository = bookRepository;
            _notificationService = notificationService;
            _loanRepository = loanRepository;
            _readerRepository = readerRepository;
        }

        public async Task<Loan> BorrowBookAsync(Book book, Reader reader)
        {
            var loan = new Loan
            {
                Book = book,
                Reader = reader,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            await _loanRepository.AddAsync(loan);
            await _notificationService.NotifyBookBorrowedAsync(reader, book);

            return loan;
        }

        public async Task ReturnBookAsync(Loan loan)
        {
            if (loan.IsOverdue)
            {
                await _notificationService.NotifyOverdueBookReturnedAsync(loan);
            }

            loan.ReturnDate = DateTime.Now;
            await _loanRepository.UpdateAsync(loan);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _bookRepository.GetByIsbnAsync(isbn);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _notificationService.NotifyNewBookAddedAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(Book book)
        {
            await _bookRepository.DeleteAsync(book);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }
    }
}

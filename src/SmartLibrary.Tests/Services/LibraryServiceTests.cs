using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Services;

namespace SmartLibrary.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly LibraryService _libraryService;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IReaderRepository> _readerRepositoryMock;
        private readonly Mock<INotificationService> _notificationServiceMock;

        public LibraryServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _readerRepositoryMock = new Mock<IReaderRepository>();
            _notificationServiceMock = new Mock<INotificationService>();

            _libraryService = new LibraryService(
                _bookRepositoryMock.Object,
                _loanRepositoryMock.Object,
                _readerRepositoryMock.Object,
                _notificationServiceMock.Object);
        }

        [Fact]
        public async Task BorrowBookAsync_AddsLoanAndNotifies()
        {
            // Arrange

            var reader = new Reader { Id = 1 };
            var book = new Book { Id = 1 };

            _bookRepositoryMock.Setup(b => b.GetByIdAsync(1)).ReturnsAsync(book);
            _readerRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(reader);

            // Act

            var loan = await _libraryService.BorrowBookAsync(book, reader);

            // Assert

            Assert.Equal(loan.Book.Id, book.Id);
            Assert.Equal(loan.Reader.Id, reader.Id);

            _loanRepositoryMock.Verify(l => l.AddAsync(loan), Times.Once);
            _notificationServiceMock.Verify(n => n.NotifyBookBorrowedAsync(reader, book), Times.Once);
        }

        [Fact]
        public async Task ReturnBookAsync_UpdatesLoanAndSetsReturnDate()
        {
            // Arrange
            var book = new Book { Id = 1 };
            var loan = new Loan { Id = 1, Book = book, ReturnDate = null };

            _bookRepositoryMock.Setup(b => b.GetByIdAsync(1)).ReturnsAsync(book);
            _loanRepositoryMock.Setup(l => l.GetByIdAsync(1)).ReturnsAsync(loan);

            // Act
            await _libraryService.ReturnBookAsync(loan);

            // Assert
            Assert.NotNull(loan.ReturnDate);
            _loanRepositoryMock.Verify(l => l.UpdateAsync(loan), Times.Once);
        }

        [Fact]
        public async Task ReturnBookAsync_ShouldNotify_WhenLoanIsOverdue()
        {
            // Arrange
            var book = new Book { Id = 1 };
            var loan = new Loan
            {
                Id = 1,
                Book = book,
                LoanDate = DateTime.Now.AddDays(-3),
                DueDate = DateTime.Now.AddDays(-1)
            };

            _bookRepositoryMock.Setup(b => b.GetByIdAsync(1)).ReturnsAsync(book);
            _loanRepositoryMock.Setup(l => l.GetByIdAsync(1)).ReturnsAsync(loan);

            // Act
            await _libraryService.ReturnBookAsync(loan);

            // Assert
            _notificationServiceMock.Verify(n => n.NotifyOverdueBookReturnedAsync(loan), Times.Once);
        }
    }
}

using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Interfaces
{
    public interface INotificationService
    {
        Task NotifyNewBookAddedAsync(Book book);
        Task NotifyBookBorrowedAsync(Reader reader, Book book);
        Task NotifyOverdueBookReturnedAsync(Loan loan);
    }
}

using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Services
{
    public interface INotificationService
    {
        event EventHandler<NotificationEvent> NotifyEvent;
        Task NotifyNewBookAddedAsync(Book book);
        Task NotifyBookBorrowedAsync(Reader reader, Book book);
        Task NotifyOverdueBookReturnedAsync(Loan loan);
    }
}

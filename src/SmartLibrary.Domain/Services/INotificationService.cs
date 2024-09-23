using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Services
{
    public interface INotificationService : IObservable<NotificationEvent>
    {
        Task NotifyNewBookAddedAsync(Book book);
        Task NotifyBookBorrowedAsync(Reader reader, Book book);
        Task NotifyOverdueBookReturnedAsync(Loan loan);
    }
}

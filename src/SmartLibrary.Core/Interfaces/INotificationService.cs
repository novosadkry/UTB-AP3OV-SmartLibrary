using SmartLibrary.Core.Events;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface INotificationService : IObservable<NotificationEvent>
    {
        Task NotifyNewBookAddedAsync(Book book);
        Task NotifyBookBorrowedAsync(Reader reader, Book book);
        Task NotifyOverdueBookReturnedAsync(Loan loan);
    }
}

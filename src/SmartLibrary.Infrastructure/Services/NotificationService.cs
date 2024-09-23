using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Services;

namespace SmartLibrary.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public event EventHandler<NotificationEvent>? NotifyEvent;

        public Task NotifyNewBookAddedAsync(Book book)
        {
            NotifyEvent?.Invoke(this, new NewBookAddedEvent(book));
            return Task.CompletedTask;
        }

        public Task NotifyBookBorrowedAsync(Reader reader, Book book)
        {
            NotifyEvent?.Invoke(this, new BookBorrowedEvent(reader, book));
            return Task.CompletedTask;
        }

        public Task NotifyOverdueBookReturnedAsync(Loan loan)
        {
            NotifyEvent?.Invoke(this, new OverdueBookReturnedEvent(loan));
            return Task.CompletedTask;
        }
    }
}

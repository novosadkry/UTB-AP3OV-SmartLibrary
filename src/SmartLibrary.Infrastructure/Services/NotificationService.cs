using System.Reactive.Subjects;
using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Services;

namespace SmartLibrary.Infrastructure.Services
{
    public class NotificationService : INotificationService, IDisposable
    {
        private readonly Subject<NotificationEvent> _subject = new();

        public Task NotifyNewBookAddedAsync(Book book)
        {
            _subject.OnNext(new NewBookAddedEvent(book));
            return Task.CompletedTask;
        }

        public Task NotifyBookBorrowedAsync(Reader reader, Book book)
        {
            _subject.OnNext(new BookBorrowedEvent(reader, book));
            return Task.CompletedTask;
        }

        public Task NotifyOverdueBookReturnedAsync(Loan loan)
        {
            _subject.OnNext(new OverdueBookReturnedEvent(loan));
            return Task.CompletedTask;
        }

        public IDisposable Subscribe(IObserver<NotificationEvent> observer)
        {
            return _subject.Subscribe(observer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subject.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

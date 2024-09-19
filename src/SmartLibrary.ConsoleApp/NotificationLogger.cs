using SmartLibrary.Core.Events;
using SmartLibrary.Core.Interfaces;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp
{
    public sealed class NotificationLogger : IObserver<NotificationEvent>, IDisposable
    {
        private readonly IDisposable _unsubscriber;

        public NotificationLogger(INotificationService notificationService)
        {
            _unsubscriber = notificationService.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(NotificationEvent value)
        {
            switch (value)
            {
                case NewBookAddedEvent newBookEvent:
                    AnsiConsole.WriteLine($"New book added: {newBookEvent.Book.Title}");
                    break;
                case BookBorrowedEvent borrowedEvent:
                    AnsiConsole.WriteLine($"{borrowedEvent.Reader.FullName} borrowed {borrowedEvent.Book.Title}");
                    break;
                case OverdueBookReturnedEvent overdueEvent:
                    AnsiConsole.WriteLine($"Overdue book returned: {overdueEvent.Loan.Book.Title}");
                    break;
            }
        }

        public void Dispose()
        {
            _unsubscriber.Dispose();
        }
    }
}

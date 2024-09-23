using System.Collections.Concurrent;
using SmartLibrary.ConsoleApp.Widgets;
using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Services;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp
{
    public sealed class NotificationHandler : IObserver<NotificationEvent>, IDisposable
    {
        private Task? _notificationTask;
        private CancellationTokenSource? _cts;
        private readonly IDisposable _unsubscriber;
        private readonly ConcurrentQueue<NotificationEvent> _eventQueue;

        public NotificationHandler(INotificationService notificationService)
        {
            _unsubscriber = notificationService.Subscribe(this);
            _eventQueue = new ConcurrentQueue<NotificationEvent>();
        }

        public async Task StartAsync()
        {
            if (_notificationTask != null)
            {
                _cts?.Dispose();
                await _notificationTask;
                _notificationTask.Dispose();
            }

            _cts = new CancellationTokenSource();
            _notificationTask = Task.Run(() => NotificationThread(_cts.Token));
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(NotificationEvent value)
        {
            _eventQueue.Enqueue(value);
        }

        private async Task NotificationThread(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                while (Program.ActiveWidget is MenuWidget && _eventQueue.TryDequeue(out var value))
                {
                    switch (value)
                    {
                        case NewBookAddedEvent newBookEvent:
                            AnsiConsole.Write(
                                new Panel($"Přidána nová kniha: {newBookEvent.Book.Title}")
                                    .Header("Nová notifikace"));
                            break;

                        case BookBorrowedEvent borrowedEvent:
                            AnsiConsole.Write(
                                new Panel($"{borrowedEvent.Reader.FullName} si půjčil/a knihu {borrowedEvent.Book.Title}")
                                    .Header("Nová notifikace"));
                            break;

                        case OverdueBookReturnedEvent overdueEvent:
                            AnsiConsole.Write(
                                new Panel($"{overdueEvent.Loan.Reader.FullName} vrátil/a se zpožděním knihu {overdueEvent.Loan.Book.Title}")
                                    .Header("Nová notifikace"));
                            break;
                    }
                }

                await Task.Delay(5000, cancellationToken);
            }
        }

        public void Dispose()
        {
            _cts?.Dispose();
            _unsubscriber.Dispose();
            _notificationTask?.Dispose();
        }
    }
}

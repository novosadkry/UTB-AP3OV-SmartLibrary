using System.Collections.Concurrent;
using SmartLibrary.ConsoleApp.Widgets;
using SmartLibrary.Domain.Events;
using SmartLibrary.Domain.Services;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp
{
    public sealed class NotificationHandler : IDisposable
    {
        private Task? _notificationTask;
        private CancellationTokenSource? _cts;
        private readonly INotificationService _notificationService;
        private readonly ConcurrentQueue<NotificationEvent> _eventQueue;

        public NotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _notificationService.NotifyEvent += OnNotifyEvent;
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

        private void OnNotifyEvent(object? sender, NotificationEvent e)
        {
            _eventQueue.Enqueue(e);
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
                                new Panel($"Přidána nová kniha: [yellow]{newBookEvent.Book.Title}[/]")
                                    .Header("Nová notifikace"));
                            break;

                        case BookBorrowedEvent borrowedEvent:
                            AnsiConsole.Write(
                                new Panel($"[yellow]{borrowedEvent.Reader.FullName}[/] si půjčil/a knihu [yellow]{borrowedEvent.Book.Title}[/]")
                                    .Header("Nová notifikace"));
                            break;

                        case OverdueBookReturnedEvent overdueEvent:
                            AnsiConsole.Write(
                                new Panel($"[yellow]{overdueEvent.Loan.Reader.FullName}[/] vrátil/a se zpožděním knihu [yellow]{overdueEvent.Loan.Book.Title}[/]")
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
            _notificationTask?.Dispose();
            _notificationService.NotifyEvent -= OnNotifyEvent;
        }
    }
}

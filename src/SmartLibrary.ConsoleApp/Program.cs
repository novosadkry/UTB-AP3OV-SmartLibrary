using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Domain.Services;
using SmartLibrary.Infrastructure.Data;
using SmartLibrary.Infrastructure.Extensions;
using SmartLibrary.ConsoleApp.Widgets;

namespace SmartLibrary.ConsoleApp
{
    public static class Program
    {
        public static Widget? ActiveWidget { get; private set; }

        public static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await RunAsync(serviceProvider);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSmartLibrary();
            services.AddSingleton<NotificationHandler>();

            return services;
        }

        private static async Task RunAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var libraryService = scope.ServiceProvider.GetRequiredService<ILibraryService>();
            var notificationHandler = scope.ServiceProvider.GetRequiredService<NotificationHandler>();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            await notificationHandler.StartAsync();

            ActiveWidget = new MenuWidget(libraryService);

            while (ActiveWidget is not null)
            {
                await ActiveWidget.DrawAsync();
                ActiveWidget = ActiveWidget.Successor;
            }
        }
    }
}

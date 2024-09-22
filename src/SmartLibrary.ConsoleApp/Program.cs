using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Extensions;
using SmartLibrary.ConsoleApp.Widgets;

namespace SmartLibrary.ConsoleApp
{
    public static class Program
    {
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
            services.AddSingleton<NotificationLogger>();

            return services;
        }

        private static async Task RunAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var libraryService = scope.ServiceProvider.GetRequiredService<ILibraryService>();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var menuWidget = new MenuWidget(libraryService);
            await menuWidget.DrawAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Services;
using SmartLibrary.ConsoleApp.Widgets;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Repositories;
using Spectre.Console;

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

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlite("Data Source=Library.db");
            });

            services
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IReaderRepository, ReaderRepository>()
                .AddScoped<ILoanRepository, LoanRepository>()
                .AddScoped<ILibraryService, LibraryService>()
                .AddScoped<INotificationService, NotificationService>();

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

            while (true)
            {
                AnsiConsole.Clear();

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Vyberte možnost:")
                        .AddChoices("Přidat knihu", "Zobrazit čtenáře", "Zobrazit výpůjčky", "[red]Ukončit[/]"));

                switch (selection)
                {
                    case "Zobrazit výpůjčky":
                        await AnsiConsole.Status()
                            .Spinner(Spinner.Known.Star)
                            .StartAsync("Načítání...", async _ =>
                                LoansTable.Render(await libraryService.GetLoansAsync()));
                        break;

                    case "[red]Ukončit[/]":
                        return;
                }

                AnsiConsole.Console.Input.ReadKey(false);
            }
        }
    }
}

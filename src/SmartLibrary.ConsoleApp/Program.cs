using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Extensions;
using SmartLibrary.ConsoleApp.Widgets;
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

            while (true)
            {
                AnsiConsole.Clear();

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Vyberte možnost:")
                        .AddChoices("Přidat knihu", "Vytvořit výpůjčku", "Zobrazit čtenáře", "Zobrazit výpůjčky", "[red]Ukončit[/]"));

                switch (selection)
                {
                    case "Zobrazit výpůjčky":
                        var loansTable = new LoansTableWidget(libraryService);
                        await loansTable.DrawAsync();
                        break;

                    case "Vytvořit výpůjčku":
                        var borrowWidget = new BorrowBookWidget(libraryService);
                        await borrowWidget.DrawAsync();
                        break;

                    case "Zobrazit čtenáře":
                        var readersTable = new ReadersTableWidget(libraryService);
                        await readersTable.DrawAsync();
                        break;

                    case "[red]Ukončit[/]":
                        return;
                }
            }
        }
    }
}

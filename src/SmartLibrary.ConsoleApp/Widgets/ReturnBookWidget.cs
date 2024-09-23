using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Services;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public class ReturnBookWidget : Widget
    {
        private readonly ILibraryService _libraryService;

        public ReturnBookWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public override async Task DrawAsync()
        {
            AnsiConsole.Clear();

            var loans = Enumerable.Empty<Loan>();

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .StartAsync("Načítání...", async _ =>
                {
                    loans = await _libraryService.GetActiveLoansAsync();
                });

            var loan = AnsiConsole.Prompt(
                new SelectionPrompt<Loan>()
                    .Title("Vyberte výpůjčku:")
                    .PageSize(20)
                    .EnableSearch()
                    .UseConverter(loan => $"{loan.Book.Isbn} - zapůjčil/a {loan.Reader.FullName}")
                    .SearchPlaceholderText("[grey](Psaním můžete vyhledávat)[/]")
                    .MoreChoicesText("[grey](Posunem nahorů a dolů zobrazíte více možností)[/]")
                    .AddChoices(loans));

            await _libraryService.ReturnBookAsync(loan);
        }
    }
}

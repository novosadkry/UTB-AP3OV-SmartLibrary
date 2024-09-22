using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Models;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public class BorrowBookWidget : IWidget
    {
        private readonly ILibraryService _libraryService;

        public BorrowBookWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task DrawAsync()
        {
            AnsiConsole.Clear();

            var books = Enumerable.Empty<Book>();
            var readers = Enumerable.Empty<Reader>();

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .StartAsync("Načítání...", async _ =>
                {
                    books = await _libraryService.GetBooksAsync();
                    readers = await _libraryService.GetReadersAsync();
                });

            var book = AnsiConsole.Prompt(
                new SelectionPrompt<Book>()
                    .Title("Vyberte knihu:")
                    .PageSize(20)
                    .EnableSearch()
                    .UseConverter(book => $"{book.Isbn} ({book.Title})")
                    .SearchPlaceholderText("[grey](Psaním můžete vyhledávat)[/]")
                    .MoreChoicesText("[grey](Posunem nahorů a dolů zobrazíte více možností)[/]")
                    .AddChoices(books));

            var reader = AnsiConsole.Prompt(
                new SelectionPrompt<Reader>()
                    .Title("Vyberte uživatele:")
                    .PageSize(20)
                    .EnableSearch()
                    .UseConverter(reader => reader.FullName)
                    .SearchPlaceholderText("[grey](Psaním můžete vyhledávat)[/]")
                    .MoreChoicesText("[grey](Posunem nahorů a dolů zobrazíte více možností)[/]")
                    .AddChoices(readers));

            await _libraryService.BorrowBookAsync(book, reader);
        }
    }
}

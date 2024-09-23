using Bogus;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Extensions;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public class AddBookWidget : IWidget
    {
        private readonly ILibraryService _libraryService;

        public AddBookWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task DrawAsync()
        {
            AnsiConsole.Clear();

            var faker = new Faker();
            var randomIsbn = faker.Random.Isbn();

            var book = new Book
            {
                Title = AnsiConsole.Ask<string>("Název knihy:"),
                Isbn = AnsiConsole.Ask("ISBN:", randomIsbn),
                Author = AnsiConsole.Ask<string>("Autor knihy:"),
                Genre = AnsiConsole.Ask<string>("Žánr:"),
                Year = AnsiConsole.Ask<int>("Rok vydání:")
            };

            await _libraryService.AddBookAsync(book);
        }
    }
}

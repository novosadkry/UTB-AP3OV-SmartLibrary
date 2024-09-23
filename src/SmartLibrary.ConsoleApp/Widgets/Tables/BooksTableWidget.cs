using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Services;
using SmartLibrary.Domain.Repositories;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets.Tables
{
    public class BooksTableWidget : TableWidget<Book>
    {
        private readonly ILibraryService _libraryService;

        public BooksTableWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        protected override Task<PagedResult<Book>> FetchPageAsync(int pageNumber)
        {
            return _libraryService.GetBooksAsync(pageNumber, 20);
        }

        protected override Task DrawTableAsync(PagedResult<Book> pagedResult)
        {
            AnsiConsole.Clear();

            var pageNumber = pagedResult.PageNumber;
            var totalPages = pagedResult.TotalPages;

            var table = new Table
            {
                Title = new TableTitle($"Seznam knih (strana {pageNumber}/{totalPages})"),
                Border = TableBorder.SimpleHeavy,
                Expand = true
            };

            table.AddColumn("ISBN");
            table.AddColumn("Název");
            table.AddColumn("Žánr");
            table.AddColumn("Autor");
            table.AddColumn("Rok vydání");

            foreach (var book in pagedResult.Items)
            {
                table.AddRow(
                    book.Isbn,
                    book.Title,
                    book.Genre,
                    book.Author,
                    book.Year.ToString());
            }

            AnsiConsole.Write(table);

            return Task.CompletedTask;
        }
    }
}

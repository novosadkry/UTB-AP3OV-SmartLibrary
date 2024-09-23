using SmartLibrary.Core.Data;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Interfaces;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets.Tables
{
    public class ReadersTableWidget : TableWidget<Reader>
    {
        private readonly ILibraryService _libraryService;

        public ReadersTableWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        protected override Task<PagedResult<Reader>> FetchPageAsync(int pageNumber)
        {
            return _libraryService.GetReadersAsync(pageNumber, 20);
        }

        protected override Task DrawTableAsync(PagedResult<Reader> pagedResult)
        {
            AnsiConsole.Clear();

            var table = new Table
            {
                Title = new TableTitle("Seznam čtenářů"),
                Border = TableBorder.SimpleHeavy
            };

            table.AddColumn("Jméno");
            table.AddColumn("Email");
            table.AddColumn("Datum narození");
            table.AddColumn("Věk");
            table.AddColumn("Počet výpůjček");

            foreach (var reader in pagedResult.Items)
            {
                table.AddRow(
                    reader.FullName,
                    reader.Email,
                    reader.BirthDate.ToString("dd/MM/yyyy"),
                    reader.Age.ToString(),
                    reader.Loans.Count.ToString());
            }

            AnsiConsole.Write(table);

            return Task.CompletedTask;
        }
    }
}

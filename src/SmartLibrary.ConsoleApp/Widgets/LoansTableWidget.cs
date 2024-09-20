using SmartLibrary.Core.Data;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Interfaces;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public class LoansTableWidget : TableWidget<Loan>
    {
        private readonly ILibraryService _libraryService;

        public LoansTableWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        protected override Task<PagedResult<Loan>> FetchPageAsync(int pageNumber)
        {
            return _libraryService.GetLoansAsync(pageNumber, 20);
        }

        protected override Task DrawTableAsync(PagedResult<Loan> pagedResult)
        {
            AnsiConsole.Clear();

            var pageNumber = pagedResult.PageNumber;
            var totalPages = pagedResult.TotalPages;

            var table = new Table
            {
                Title = new TableTitle($"Seznam výpůjček (strana {pageNumber}/{totalPages})"),
                Border = TableBorder.SimpleHeavy,
                Expand = true
            };

            table.AddColumn("ISBN");
            table.AddColumn("Čtenář");
            table.AddColumn("Datum vypůjčení");
            table.AddColumn("Datum vrácení");
            table.AddColumn("Termín vrácení");
            table.AddColumn("Zpoždění (počet dní)");

            foreach (var loan in pagedResult.Items)
            {
                table.AddRow(
                    loan.Book.Isbn,
                    loan.Reader.FullName,
                    loan.LoanDate.ToString("dd.MM.yyyy"),
                    loan.ReturnDate?.ToString("dd.MM.yyyy") ?? "--.--.----",
                    loan.DueDate.ToString("dd.MM.yyyy"),
                    loan.IsOverdue ? $"Ano ({loan.DaysOverdue})" : "Ne");
            }

            AnsiConsole.Write(table);

            return Task.CompletedTask;
        }
    }
}

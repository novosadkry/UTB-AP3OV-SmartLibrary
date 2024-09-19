using SmartLibrary.Core.Models;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public static class LoansTable
    {
        public static void Render(IEnumerable<Loan> loans)
        {
            var table = new Table
            {
                Title = new TableTitle("Seznam výpůjček"),
                Border = TableBorder.SimpleHeavy
            };

            table.AddColumn("Kniha ID");
            table.AddColumn("Čtenář ID");
            table.AddColumn("Název knihy");
            table.AddColumn("Čtenář");

            foreach (var loan in loans)
            {
                table.AddRow(
                    loan.Book.Id.ToString(),
                    loan.Reader.Id.ToString(),
                    loan.Book.Title,
                    loan.Reader.FullName);
            }

            AnsiConsole.Write(table);
        }
    }
}

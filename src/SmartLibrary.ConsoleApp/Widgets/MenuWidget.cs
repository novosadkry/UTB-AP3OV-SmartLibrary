using SmartLibrary.Core.Interfaces;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public enum MenuOption
    {
        CreateLoan,
        ShowReaders,
        ShowLoans,
        Exit
    }

    public static class MenuOptionExtensions
    {
        public static string ToDisplayString(this MenuOption option) => option switch
        {
            MenuOption.CreateLoan => "Vytvořit výpůjčku",
            MenuOption.ShowReaders => "Zobrazit čtenáře",
            MenuOption.ShowLoans => "Zobrazit výpůjčky",
            MenuOption.Exit => "[red]Ukončit[/]",
            _ => string.Empty
        };
    }

    public class MenuWidget : IWidget
    {
        private readonly ILibraryService _libraryService;

        public MenuWidget(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task DrawAsync()
        {
            while (true)
            {
                AnsiConsole.Clear();

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOption>()
                        .Title("Vyberte možnost:")
                        .AddChoices(Enum.GetValues<MenuOption>())
                        .UseConverter(option => option.ToDisplayString()));

                switch (selection)
                {
                    case MenuOption.ShowLoans:
                        var loansTable = new LoansTableWidget(_libraryService);
                        await loansTable.DrawAsync();
                        break;

                    case MenuOption.CreateLoan:
                        var borrowWidget = new BorrowBookWidget(_libraryService);
                        await borrowWidget.DrawAsync();
                        break;

                    case MenuOption.ShowReaders:
                        var readersTable = new ReadersTableWidget(_libraryService);
                        await readersTable.DrawAsync();
                        break;

                    case MenuOption.Exit:
                        return;
                }
            }
        }
    }
}

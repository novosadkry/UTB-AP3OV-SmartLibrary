using SmartLibrary.Core.Interfaces;
using SmartLibrary.ConsoleApp.Widgets.Tables;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public enum MenuOption
    {
        AddBook,
        CreateLoan,
        ShowReaders,
        ShowLoans,
        Exit
    }

    public static class MenuOptionExtensions
    {
        public static string ToDisplayString(this MenuOption option) => option switch
        {
            MenuOption.AddBook => "Přidat knihu",
            MenuOption.CreateLoan => "Vytvořit výpůjčku",
            MenuOption.ShowReaders => "Zobrazit čtenáře",
            MenuOption.ShowLoans => "Zobrazit výpůjčky",
            MenuOption.Exit => "[red]Ukončit[/]",
            _ => string.Empty
        };
    }

    public class MenuWidget : IWidget
    {
        private readonly Dictionary<MenuOption, Func<IWidget>> _options;

        public MenuWidget(ILibraryService libraryService)
        {
            _options = new Dictionary<MenuOption, Func<IWidget>>
            {
                { MenuOption.AddBook, () => new AddBookWidget(libraryService) },
                { MenuOption.ShowLoans, () => new LoansTableWidget(libraryService) },
                { MenuOption.CreateLoan, () => new BorrowBookWidget(libraryService) },
                { MenuOption.ShowReaders, () => new ReadersTableWidget(libraryService) }
            };
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

                if (selection == MenuOption.Exit) break;
                if (!_options.TryGetValue(selection, out var widgetProducer)) continue;

                var widget = widgetProducer();
                await widget.DrawAsync();
            }
        }
    }
}

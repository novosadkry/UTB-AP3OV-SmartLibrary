using SmartLibrary.Domain.Services;
using SmartLibrary.ConsoleApp.Widgets.Tables;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public enum MenuOption
    {
        AddBook,
        CreateLoan,
        ShowBooks,
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
            MenuOption.ShowBooks => "Zobrazit knihy",
            MenuOption.ShowReaders => "Zobrazit čtenáře",
            MenuOption.ShowLoans => "Zobrazit výpůjčky",
            MenuOption.Exit => "[red]Ukončit[/]",
            _ => string.Empty
        };
    }

    public class MenuWidget : Widget
    {
        private readonly Dictionary<MenuOption, Func<Widget>> _options;

        public MenuWidget(ILibraryService libraryService)
        {
            _options = new Dictionary<MenuOption, Func<Widget>>
            {
                { MenuOption.AddBook, () => new AddBookWidget(libraryService) },
                { MenuOption.CreateLoan, () => new BorrowBookWidget(libraryService) },
                { MenuOption.ShowBooks, () => new BooksTableWidget(libraryService) },
                { MenuOption.ShowReaders, () => new ReadersTableWidget(libraryService) },
                { MenuOption.ShowLoans, () => new LoansTableWidget(libraryService) }
            };
        }

        public override Task DrawAsync()
        {
            AnsiConsole.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                    .Title("Vyberte možnost:")
                    .AddChoices(Enum.GetValues<MenuOption>())
                    .UseConverter(option => option.ToDisplayString()));

            if (selection == MenuOption.Exit)
            {
                SetSuccessor(null);
                return Task.CompletedTask;
            }

            if (!_options.TryGetValue(selection, out var widgetProducer))
            {
                SetSuccessor(this);
                return Task.CompletedTask;
            }

            SetSuccessor(widgetProducer().SetSuccessor(this));
            return Task.CompletedTask;
        }
    }
}

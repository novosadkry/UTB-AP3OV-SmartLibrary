﻿using SmartLibrary.Domain.Repositories;
using Spectre.Console;

namespace SmartLibrary.ConsoleApp.Widgets
{
    public abstract class TableWidget<T> : Widget
    {
        protected abstract Task DrawTableAsync(PagedResult<T> pagedResult);
        protected abstract Task<PagedResult<T>> FetchPageAsync(int pageNumber);

        public override async Task DrawAsync()
        {
            int pageNumber = 1;

            while (true)
            {
                var pagedResult = await FetchDataWithSpinnerAsync(pageNumber);
                await DrawTableAsync(pagedResult);

                AnsiConsole.Markup("[gray](ESC pro návrat do menu)[/]");

                var keyInfo = AnsiConsole.Console.Input.ReadKey(false);
                if (keyInfo is null) return;

                switch (keyInfo.Value.Key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.LeftArrow:
                        pageNumber = Math.Clamp(pageNumber - 1, 1, Math.Max(1, pagedResult.TotalPages));
                        break;
                    case ConsoleKey.RightArrow:
                        pageNumber = Math.Clamp(pageNumber + 1, 1, Math.Max(1, pagedResult.TotalPages));
                        break;
                }
            }
        }

        private async Task<PagedResult<T>> FetchDataWithSpinnerAsync(int pageNumber)
        {
            var pagedResult = new PagedResult<T>();

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .StartAsync("Načítání...", async _ =>
                {
                    pagedResult = await FetchPageAsync(pageNumber);
                });

            return pagedResult;
        }
    }
}

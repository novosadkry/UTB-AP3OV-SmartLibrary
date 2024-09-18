using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Services;
using SmartLibrary.Core.Repositories;

namespace SmartLibrary.ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.ClearProviders();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite(
                    "Data Source=Library.db",
                    sqlite => sqlite.MigrationsAssembly("SmartLibrary.Core")));

            builder.Services
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IReaderRepository, ReaderRepository>()
                .AddScoped<ILoanRepository, LoanRepository>()
                .AddScoped<ILibraryService, LibraryService>()
                .AddScoped<INotificationService, NotificationService>();

            builder.Services.AddScoped<NotificationLogger>();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await RunAsync(serviceProvider);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite("Data Source=Library.db"));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILibraryService, LibraryService>();

            return services;
        }

        private static async Task RunAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var libraryService = scope.ServiceProvider.GetRequiredService<ILibraryService>();

            var book = await libraryService.GetBookByIdAsync(1);
            Console.WriteLine($"Book: {book.Title} by {book.Author}");
        }
    }
}

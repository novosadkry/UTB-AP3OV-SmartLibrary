using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Services;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Repositories;

namespace SmartLibrary.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmartLibrary(this IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlite("Data Source=Library.db");
            });

            services
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IReaderRepository, ReaderRepository>()
                .AddScoped<ILoanRepository, LoanRepository>()
                .AddScoped<ILibraryService, LibraryService>()
                .AddSingleton<INotificationService, NotificationService>();

            return services;
        }
    }
}

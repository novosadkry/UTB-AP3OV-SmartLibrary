using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Domain.Services;
using SmartLibrary.Domain.Repositories;
using SmartLibrary.Infrastructure.Data;
using SmartLibrary.Infrastructure.Services;
using SmartLibrary.Infrastructure.Repositories;

namespace SmartLibrary.Infrastructure.Extensions
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

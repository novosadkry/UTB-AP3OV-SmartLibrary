using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Models;

namespace SmartLibrary.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
    }
}

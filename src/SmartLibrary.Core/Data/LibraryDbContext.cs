using Microsoft.EntityFrameworkCore;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Data
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seeder = new LibraryDbContextSeeder();

            modelBuilder.Entity<Book>().HasData(seeder.Books);
            modelBuilder.Entity<Reader>().HasData(seeder.Readers);
            modelBuilder.Entity<Loan>().HasData(seeder.Loans);

            base.OnModelCreating(modelBuilder);
        }
    }
}

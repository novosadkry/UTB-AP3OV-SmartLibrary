using Bogus;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Extensions;

namespace SmartLibrary.Core.Data
{
    public class LibraryDbContextSeeder
    {
        public IReadOnlyCollection<Book> Books { get; }
        public IReadOnlyCollection<Reader> Readers { get; }
        public IReadOnlyCollection<Loan> Loans { get; }

        public LibraryDbContextSeeder()
        {
            Books = GenerateBooks(100);
            Readers = GenerateReaders(20);
            Loans = GenerateLoans(30, Books, Readers);
        }

        private static IReadOnlyCollection<Book> GenerateBooks(int count)
        {
            var fakerIndex = 1;
            var faker = new Faker<Book>("cz")
                .RuleFor(b => b.Id, _ => fakerIndex++)
                .RuleFor(b => b.Genre, f => f.Random.Word())
                .RuleFor(b => b.Title, f => f.Random.Words())
                .RuleFor(b => b.Author, f => f.Person.FullName)
                .RuleFor(b => b.Isbn, f => f.Random.Isbn());

            var books = Enumerable.Range(1, count)
                .Select(_ => faker.Generate())
                .ToList();

            return books;
        }

        private static IReadOnlyCollection<Reader> GenerateReaders(int count)
        {
            var fakerIndex = 1;
            var faker = new Faker<Reader>("cz")
                .RuleFor(r => r.Id, _ => fakerIndex++)
                .RuleFor(r => r.FirstName, f => f.Name.FirstName())
                .RuleFor(r => r.LastName, f => f.Name.LastName())
                .RuleFor(r => r.Age, f => f.Random.Number(18, 70))
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.BirthDate, f => f.Date.Past(50, DateTime.Now.AddYears(-18)));

            var readers = Enumerable.Range(1, count)
                .Select(_ => faker.Generate())
                .ToList();

            return readers;
        }

        private static IReadOnlyCollection<Loan> GenerateLoans(
            int count,
            IReadOnlyCollection<Book> books,
            IReadOnlyCollection<Reader> readers)
        {
            var fakerIndex = 1;
            var faker = new Faker<Loan>("cz")
                .RuleFor(l => l.Id, _ => fakerIndex++)
                .RuleFor(l => l.LoanDate, f => f.Date.Past())
                .RuleFor(l => l.DueDate, f => f.Date.Recent(5))
                .RuleFor(l => l.ReturnDate, f =>
                    f.PickRandom<DateTime?>(f.Date.Recent(), null))
                .RuleFor(l => l.BookId, f => f.PickRandom<Book>(books).Id)
                .RuleFor(l => l.ReaderId, f => f.PickRandom<Reader>(readers).Id);

            var loans = Enumerable.Range(1, count)
                .Select(_ => faker.Generate())
                .DistinctBy(l => new { l.ReaderId, l.BookId })
                .ToList();

            return loans;
        }
    }
}

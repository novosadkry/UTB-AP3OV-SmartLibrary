using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Repositories;
using SmartLibrary.Infrastructure.Data;

namespace SmartLibrary.Infrastructure.Repositories
{
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReaderRepository(LibraryDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reader>> GetReadersWithOverdueLoansAsync()
        {
            return await _dbContext.Readers
                .Where(reader => reader.Loans.Any(loan => loan.IsOverdue))
                .ToListAsync();
        }
    }
}

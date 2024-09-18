using Microsoft.EntityFrameworkCore;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Interfaces;

namespace SmartLibrary.Core.Repositories
{
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {
        private readonly LibraryDbContext _dbContext;

        private ReaderRepository(LibraryDbContext dbContext)
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

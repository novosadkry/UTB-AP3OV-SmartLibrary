using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Models;
using SmartLibrary.Domain.Repositories;
using SmartLibrary.Infrastructure.Data;

namespace SmartLibrary.Infrastructure.Repositories
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        private readonly LibraryDbContext _dbContext;

        public LoanRepository(LibraryDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _dbContext.Loans
                .Where(loan => loan.IsOverdue)
                .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Models;
using SmartLibrary.Core.Interfaces;

namespace SmartLibrary.Core.Repositories
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

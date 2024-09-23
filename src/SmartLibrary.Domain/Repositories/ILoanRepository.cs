using SmartLibrary.Domain.Models;

namespace SmartLibrary.Domain.Repositories
{
    public interface ILoanRepository : IRepositoryBase<Loan>
    {
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    }
}

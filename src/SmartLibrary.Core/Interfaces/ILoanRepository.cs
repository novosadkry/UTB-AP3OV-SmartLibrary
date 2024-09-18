using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface ILoanRepository : IRepositoryBase<Loan>
    {
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    }
}

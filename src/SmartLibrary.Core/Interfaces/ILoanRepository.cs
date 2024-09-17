using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    }
}

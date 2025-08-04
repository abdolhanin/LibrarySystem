using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task<List<Loan>> GetAllAsync();
        Task SaveChangesAsync(CancellationToken cancellation);
        Task<Loan?> GetByIdAsync(Guid id);
    }
}

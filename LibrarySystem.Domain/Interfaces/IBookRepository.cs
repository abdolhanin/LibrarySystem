using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAvailableAsync();
        Task SaveChangesAsync();
    }
}

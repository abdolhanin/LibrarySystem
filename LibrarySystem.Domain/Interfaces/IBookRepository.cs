using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> GetByIdAsync(Guid id);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<Book>> GetAvailableBooksAsync();

    }
}

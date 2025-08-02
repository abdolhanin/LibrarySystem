using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Persistence
{
    public class BookRepository(LibraryDbContext context) : IBookRepository
    {
        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await context.Books.AddAsync(book, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAvailableAsync()
        {
            return await context.Books
                .Where(b => b.IsAvailable)
                .ToListAsync();
        }
    }
}

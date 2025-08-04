using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Persistence
{
    public class LoanRepository(LibraryDbContext context, IDomainEventDispatcher eventDispatcher) : ILoanRepository
    {
        public async Task AddAsync(Loan loan)
        {
            await context.Loans.AddAsync(loan);
        }
        public async Task<List<Loan>> GetAllAsync()
        {
            return await context.Loans.ToListAsync();
        }
        public async Task<Loan?> GetByIdAsync(Guid id) =>
            await context.Loans.FindAsync(id);
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entitiesWithEvents = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = entitiesWithEvents
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            entitiesWithEvents.ForEach(e => e.Entity.ClearDomainEvents());

            await context.SaveChangesAsync(cancellationToken);
            await eventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);
        }

    }
}

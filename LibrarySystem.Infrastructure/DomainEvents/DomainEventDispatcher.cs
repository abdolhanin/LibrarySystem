using MediatR;

namespace LibrarySystem.Infrastructure.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken);
    }

    public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
    {
        public async Task DispatchEventsAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}

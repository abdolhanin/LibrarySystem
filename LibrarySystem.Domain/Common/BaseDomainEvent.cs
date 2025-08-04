using MediatR;

namespace LibrarySystem.Domain.Common
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
        public Guid EventId { get; protected set; } = Guid.NewGuid();
    }
}

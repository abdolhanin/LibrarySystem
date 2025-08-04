using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Common
{
    public abstract class BaseEntity
    {
        private readonly List<BaseDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}

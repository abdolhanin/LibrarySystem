using LibrarySystem.Domain.Common;

namespace LibrarySystem.Domain.Events
{
    public class LoanReturnedEvent(Guid loanId, Guid bookId, DateTime returnedAt) : BaseDomainEvent
    {
        public Guid LoanId { get; } = loanId;
        public Guid BookId { get; } = bookId;
        public DateTime ReturnedAt { get; } = returnedAt;
    }

}

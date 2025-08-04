using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.Events;

namespace LibrarySystem.Domain.Entities;

public class Loan : BaseEntity
{
    private Loan()
    {
    } // FOR EF Core

    public Loan(Guid bookId, string borrower)
    {
        BookId = bookId;
        Borrower = borrower;
        LoanDate = DateTime.UtcNow;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public string Borrower { get; private set; } = default!;
    public DateTime LoanDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public Book Book { get; private set; } = default!;
    public bool IsReturned { get; private set; }


    public void Return()
    {
        if (ReturnDate.HasValue)
            throw new InvalidOperationException("Book already returned");

        ReturnDate = DateTime.UtcNow;

        AddDomainEvent(new LoanReturnedEvent(Id, BookId, ReturnDate.Value));
    }
}
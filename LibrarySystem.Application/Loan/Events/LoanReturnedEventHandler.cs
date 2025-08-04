using LibrarySystem.Domain.Events;
using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Loan.Events
{
    public class LoanReturnedEventHandler(IBookRepository bookRepository) : INotificationHandler<LoanReturnedEvent>
    {
        public async Task Handle(LoanReturnedEvent notification, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(notification.BookId);
            if (book == null) return;

            book.IncreaseAvailableCopies();

            await bookRepository.SaveChangesAsync(cancellationToken);
        }
    }
}

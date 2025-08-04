using LibrarySystem.Domain.Interfaces;
using MediatR;
namespace LibrarySystem.Application.Loan.Command
{
    public class CreateLoanCommandHandler(IBookRepository bookRepo, ILoanRepository loanRepo)
        : IRequestHandler<CreateLoanCommand, Guid>
    {
        public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepo.GetByIdAsync(request.BookId);
            if (book == null || !book.IsAvailable)
                throw new Exception("Book not available");

            book.Lend();

            var loan = new Domain.Entities.Loan(book.Id, request.Borrower);
            await loanRepo.AddAsync(loan);
            await loanRepo.SaveChangesAsync(cancellationToken);
            await bookRepo.SaveChangesAsync(cancellationToken);

            return loan.Id;
        }
    }
}

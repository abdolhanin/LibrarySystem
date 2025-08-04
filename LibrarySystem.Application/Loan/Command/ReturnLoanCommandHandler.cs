using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Loan.Command
{
    public class ReturnLoanCommandHandler(ILoanRepository loanRepo, IBookRepository bookRepo)
        : IRequestHandler<ReturnLoanCommand>
    {
        public async Task Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loans = await loanRepo.GetAllAsync();
                var loan = loans.FirstOrDefault(l => l.Id == request.LoanId);
                if (loan == null)
                    throw new KeyNotFoundException($"Loan with ID '{request.LoanId}' not found.");

                loan.Return();

                var book = await bookRepo.GetByIdAsync(loan.BookId);
                book?.IncreaseAvailableCopies();

                await loanRepo.SaveChangesAsync(cancellationToken);
                await bookRepo.SaveChangesAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while returning the loan.", ex);
            }
        }
    }
}

using MediatR;

namespace LibrarySystem.Application.Loan.Command
{
    public record CreateLoanCommand(Guid BookId, string Borrower) : IRequest<Guid>;

}

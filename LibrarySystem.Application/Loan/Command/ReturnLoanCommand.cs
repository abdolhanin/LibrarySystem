using MediatR;

namespace LibrarySystem.Application.Loan.Command
{
    public record ReturnLoanCommand(Guid LoanId) : IRequest;

}

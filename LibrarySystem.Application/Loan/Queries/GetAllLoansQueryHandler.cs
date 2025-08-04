using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Loan.Queries
{
    public class GetAllLoansQueryHandler(ILoanRepository loanRepository)
        : IRequestHandler<GetAllLoansQuery, List<Domain.Entities.Loan>>
    {
        public async Task<List<Domain.Entities.Loan>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            return await loanRepository.GetAllAsync();
        }
    }
}

using MediatR;

namespace LibrarySystem.Application.Loan.Queries
{
    public record GetAllLoansQuery : IRequest<List<Domain.Entities.Loan>>;

}

using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public record LendBookCommand(Guid BookId) : IRequest;

}

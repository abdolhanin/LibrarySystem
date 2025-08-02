using MediatR;

namespace LibrarySystem.Application.Books.Commands
{


    public record CreateBookCommand(
        string Title,
        string Author,
        string ISBN,
        int AvailableCopies
    ) : IRequest<Guid>;
}

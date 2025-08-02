using LibrarySystem.Application.Common;
using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class LendBookCommandHandler(IBookRepository repository) : IRequestHandler<LendBookCommand>
    {
        public async Task Handle(LendBookCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.BookId);
            if (book == null)
                throw new NotFoundException("Book not found.");

            book.Lend();
            await repository.SaveChangesAsync();
        }
    }
}

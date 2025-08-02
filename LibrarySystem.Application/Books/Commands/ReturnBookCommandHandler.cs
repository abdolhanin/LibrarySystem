using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class ReturnBookCommandHandler(IBookRepository repository) : IRequestHandler<ReturnBookCommand>
    {
        public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.BookId);
            if (book == null)
                throw new Exception("Book not found.");

            book.Return();
            await repository.SaveChangesAsync();
        }
    }
}

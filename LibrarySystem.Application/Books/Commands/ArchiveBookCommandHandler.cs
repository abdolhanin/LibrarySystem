using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class ArchiveBookCommandHandler(IBookRepository repository) : IRequestHandler<ArchiveBookCommand>
    {
        public async Task Handle(ArchiveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.BookId);
            if (book == null)
                throw new Exception("Book not found.");

            book.Archive();
            await repository.SaveChangesAsync();
        }
    }
}

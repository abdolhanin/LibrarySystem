using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    internal class UpdateBookCommandHandler(IBookRepository repository) : IRequestHandler<UpdateBookCommand>
    {
        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.Id);
            if (book == null)
                throw new Exception("Book not found.");

            book.UpdateDetails(request.Title, request.Author, request.ISBN);

            await repository.SaveChangesAsync();
        }
    }
}

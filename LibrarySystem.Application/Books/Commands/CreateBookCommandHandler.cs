using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class CreateBookCommandHandler(IBookRepository repository) : IRequestHandler<CreateBookCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                AvailableCopies = request.AvailableCopies
            };

            await repository.AddAsync(book, cancellationToken);
            return book.Id;
        }
    }
}

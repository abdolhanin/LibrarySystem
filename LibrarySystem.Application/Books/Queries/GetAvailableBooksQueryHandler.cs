using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Interfaces;
using MediatR;

namespace LibrarySystem.Application.Books.Queries
{
    public class GetAvailableBooksQueryHandler(IBookRepository repository)
        : IRequestHandler<GetAvailableBooksQuery, List<AvailableBookDto>>
    {
        public async Task<List<AvailableBookDto>> Handle(GetAvailableBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await repository.GetAvailableBooksAsync();

            return books.Select(book => new AvailableBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                AvailableCopies = book.AvailableCopies
            }).ToList();
        }
    }
}

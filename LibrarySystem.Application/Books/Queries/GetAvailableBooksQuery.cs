using LibrarySystem.Application.DTOs;
using MediatR;

namespace LibrarySystem.Application.Books.Queries
{
    public class GetAvailableBooksQuery : IRequest<List<AvailableBookDto>>
    {
    }

}

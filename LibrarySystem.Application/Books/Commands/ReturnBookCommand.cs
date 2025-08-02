using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class ReturnBookCommand : IRequest
    {
        public Guid BookId { get; set; }
    }
}

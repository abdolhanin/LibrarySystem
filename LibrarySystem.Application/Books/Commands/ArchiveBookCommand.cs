using MediatR;

namespace LibrarySystem.Application.Books.Commands
{
    public class ArchiveBookCommand : IRequest
    {
        public Guid BookId { get; set; }
    }
}

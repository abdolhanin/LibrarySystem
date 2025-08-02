using LibrarySystem.Application.Books.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }

        [HttpPost("{id}/lend")]
        public async Task<IActionResult> Lend(Guid id)
        {
            await mediator.Send(new LendBookCommand(id));
            return NoContent();
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> Return(Guid id)
        {
            await mediator.Send(new ReturnBookCommand { BookId = id });
            return NoContent();
        }
    }
}

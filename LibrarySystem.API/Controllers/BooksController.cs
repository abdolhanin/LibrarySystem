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
    }
}

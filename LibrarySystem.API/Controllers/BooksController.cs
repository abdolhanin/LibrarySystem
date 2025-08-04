using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Books.Queries;
using LibrarySystem.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;

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

    [HttpGet("available")]
    public async Task<ActionResult<List<AvailableBookDto>>> GetAvailableBooks()
    {
        var result = await mediator.Send(new GetAvailableBooksQuery());
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch.");

        await mediator.Send(command);
        return NoContent();
    }
    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        await mediator.Send(new ArchiveBookCommand { BookId = id });
        return NoContent();
    }
}
using LibrarySystem.Application.Loan.Command;
using LibrarySystem.Application.Loan.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await mediator.Send(new GetAllLoansQuery());
            return Ok(loans);
        }
        [HttpPost("{id}/return")]
        public async Task<IActionResult> Return(Guid id)
        {
            await mediator.Send(new ReturnLoanCommand(id));
            return NoContent();
        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Owners.Command;
using RealState.Application.Owners.Query;

namespace RealState.Api.Controllers.Owners
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await mediator.Send(new GetAllOwnerQuery());
            return Ok(owners);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertOwnerCommand command)
        {
            var ownerId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id = ownerId }, ownerId);
        }
    }
}

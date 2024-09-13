namespace RealState.Api.Controllers.Properties
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using RealState.Application.Properties.Command;
    using RealState.Application.Properties.Query;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController(IMediator mediator) : ControllerBase
    {  
        [HttpGet("filters")]
        public async Task<IActionResult> GetAllWithFilters([FromQuery] GetAllPropertyQuery query)
        {
            var properties = await mediator.Send(query);
            return Ok(properties);
        }

        [HttpPut("{propertyId:guid}/{price:decimal}")]
        public async Task<IActionResult> ChangePrice(Guid propertyId, decimal price)
        {
            var command = new ChangePropertyPriceCommand(propertyId, price);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePropertyCommand command)
        {
            command = command with { Id = id };
            var updatedId = await mediator.Send(command);
            return Ok(updatedId);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertPropertyCommand command)
        {
            var createdId = await mediator.Send(command);
            return Ok(createdId);
        }
    }
}

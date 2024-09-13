using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Api.Filters;
using RealState.Application.PropertyImages.Command;

namespace RealState.Api.Controllers.PropertyImages;


[ApiController]
[Route("api/[controller]")]
public class PropertyImageController(IMediator mediator) : ControllerBase
{ 
    [HttpPost("{idProperty}/images")]
    [DisableRequestSizeLimit]
    [ValidateFile(2 * 1024 * 1024, [".jpg", ".jpeg", ".png"])]
    public async Task<IActionResult> AddImage(Guid idProperty, [FromForm] IFormFile file)
    {
        var idPropertyImage = await mediator.Send(new InsertPropertyImageCommand(idProperty, file));
        return Created($"/images/{idPropertyImage}", new { id = idPropertyImage, name = file.FileName });
    }
}
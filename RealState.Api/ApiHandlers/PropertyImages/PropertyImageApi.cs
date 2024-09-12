using MediatR;
using RealState.Application.PropertyImages.Command;

namespace RealState.Api.ApiHandlers.PropertyImages
{
    public static class PropertyImageApi
    { 
        public static RouteGroupBuilder MapPropertyImage(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/{propertyId}/image",

                    async (IMediator mediator, Guid propertyId, IFormFile file) =>
                    {
                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        var idPropertyImage = await mediator.Send(new InsertPropertyImageCommand(propertyId, memoryStream.ToArray()));
                        return Results.Created($"/images/{idPropertyImage}", new { id = idPropertyImage, name = file.FileName });
                    })
                .Produces(StatusCodes.Status200OK)
                .WithSummary("Upload to image for property ")
                .WithOpenApi();
            return (RouteGroupBuilder)routeHandler;
        }
    }
}

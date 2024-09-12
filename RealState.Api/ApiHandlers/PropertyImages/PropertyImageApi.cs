using MediatR;
using RealState.Api.Filters;
using RealState.Application.PropertyImages.Command;

namespace RealState.Api.ApiHandlers.PropertyImages
{
    public static class PropertyImageApi
    {

        public static RouteGroupBuilder MapPropertyImage(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/",
                    async (IMediator mediator, [Validate] InsertPropertyImageCommand command) => Results.Ok(await mediator.Send(
                        command)))
                .Produces(StatusCodes.Status200OK)
                .WithSummary("Change price of property ")
                .WithOpenApi();
            return (RouteGroupBuilder)routeHandler;
        }
    }
}

using MediatR;
using RealState.Api.Filters;
using RealState.Application.Owners.Command;
using RealState.Application.Owners.Query;

namespace RealState.Api.ApiHandlers.Holidays
{
    public static class OwnerApi
    {
        public static RouteGroupBuilder MapOwner(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapGet("/", async (IMediator mediator) => Results.Ok(await mediator.Send(new GetAllOwnerQuery())))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Get all owner")
            .WithOpenApi();


            routeHandler.MapPost("/", async (IMediator mediator, [Validate] InsertOwnerCommand owner) =>
            {
                var ownerId = await mediator.Send(owner);
                return Results.Ok(ownerId);
            })
            .Produces(statusCode: StatusCodes.Status201Created)
            .WithSummary("Create new owner")
            .WithOpenApi();

            return (RouteGroupBuilder)routeHandler;
        }
    }
}

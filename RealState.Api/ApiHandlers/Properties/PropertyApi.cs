using MediatR;
using RealState.Api.Filters;
using RealState.Application.Properties.Command;
using RealState.Application.Properties.Query;

namespace RealState.Api.ApiHandlers.Properties
{
    public static class PropertyApi
    {
        public static RouteGroupBuilder MapProperty(this IEndpointRouteBuilder routeHandler)
        {

            routeHandler.MapGet("/filters", async (IMediator mediator, GetAllPropertyFilter filter) =>
                {
                    var query = new GetAllPropertyQuery()
                    {
                        Address = filter.Address,
                        Name = filter.Name,
                        Price = filter.Price,
                        Year = filter.Year,
                        PageNumber = filter.PageNumber,
                        PageSize = filter.PageSize,
                        CodeInternal = filter.CodeInternal
                    };
                    return Results.Ok(await mediator.Send(query));
                })
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Get all properties with filters")
            .WithOpenApi();

            routeHandler.MapPut("/{propertyId}/price",
                    async (IMediator mediator, Guid propertyId, decimal price) => Results.Ok(await mediator.Send(
                            new ChangePropertyPriceCommand(propertyId, price))))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Change price of property ")
            .WithOpenApi();


            routeHandler.MapPost("/", async (IMediator mediator, [Validate] InsertPropertyCommand reservation) =>
            {
                var reservationId = await mediator.Send(reservation);
                return Results.Ok(reservationId);
            })
            .Produces(statusCode: StatusCodes.Status201Created)
            .WithSummary("Create new property")
            .WithOpenApi();



            return (RouteGroupBuilder)routeHandler;
        }
    }
}

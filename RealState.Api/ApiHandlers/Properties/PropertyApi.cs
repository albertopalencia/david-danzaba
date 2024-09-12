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

            routeHandler.MapGet("/filters", async (IMediator mediator,
                    string? name, string? address, decimal? price, int? codeInternal,
                    int? year, int pageNumber = 1, int pageSize = 10) =>
            {
                var query = new GetAllPropertyQuery()
                {
                    Address = address,
                    Name = name,
                    Price = price,
                    Year = year,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    CodeInternal = codeInternal
                };
                return Results.Ok(await mediator.Send(query));
            })
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Get all properties with filters")
            .WithOpenApi();

            routeHandler.MapPut("/{propertyId}/{price}",
                    async (IMediator mediator, Guid propertyId, decimal price) =>
                    {
                        return Results.Ok(await mediator.Send(new ChangePropertyPriceCommand(propertyId, price)));
                    })
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

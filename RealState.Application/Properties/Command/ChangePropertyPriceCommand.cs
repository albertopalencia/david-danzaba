using MediatR;

namespace RealState.Application.Properties.Command
{
    public record ChangePropertyPriceCommand(Guid PropertyId, decimal Price) : IRequest<Unit>;
}

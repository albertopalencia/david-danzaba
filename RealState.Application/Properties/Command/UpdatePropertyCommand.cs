using MediatR;

namespace RealState.Application.Properties.Command
{
    public record UpdatePropertyCommand(Guid Id, string Name, string Address, decimal Price, int Year, Guid IdOwner) : IRequest<Unit>;
}

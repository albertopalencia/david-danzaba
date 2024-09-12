using MediatR;

namespace RealState.Application.Properties.Command
{
    public record InsertPropertyCommand(string Name, string Address, decimal Price, int Year, Guid IdOwner) : IRequest<Guid>;
}

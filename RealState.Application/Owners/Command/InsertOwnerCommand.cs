using MediatR;

namespace RealState.Application.Owners.Command
{
    public record InsertOwnerCommand(string Name, string Address, string Photo, DateTime Birthday) : IRequest<Guid>;
}

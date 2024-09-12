using MediatR;

namespace RealState.Application.PropertyImages.Command
{
    public record InsertPropertyImageCommand(Guid IdProperty, byte[] File) : IRequest<Guid>;
}

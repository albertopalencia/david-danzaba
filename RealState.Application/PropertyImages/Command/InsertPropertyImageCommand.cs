using MediatR;
using Microsoft.AspNetCore.Http;

namespace RealState.Application.PropertyImages.Command
{
    public record InsertPropertyImageCommand(Guid IdProperty, IFormFile? File) : IRequest<Guid>;
}

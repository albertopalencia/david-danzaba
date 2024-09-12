using MediatR;
using RealState.Application.Owners.Query.Dto;

namespace RealState.Application.Owners.Query
{
    public record GetAllOwnerQuery() : IRequest<IEnumerable<SummaryOwnerDto>>;
}

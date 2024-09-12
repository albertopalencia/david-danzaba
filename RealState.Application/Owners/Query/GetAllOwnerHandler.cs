using AutoMapper;
using MediatR;
using RealState.Application.Owners.Query.Dto;
using RealState.Domain.Owners.Port;

namespace RealState.Application.Owners.Query
{
    internal class GetAllOwnerHandler(IOwnerQueryRepository holidayRepository, IMapper mapper) : IRequestHandler<GetAllOwnerQuery, IEnumerable<SummaryOwnerDto>>
    {
        public async Task<IEnumerable<SummaryOwnerDto>> Handle(GetAllOwnerQuery request, CancellationToken cancellationToken)
        {
            var owners = await holidayRepository.GetAllAsync();
            return mapper.Map<IEnumerable<SummaryOwnerDto>>(owners);
        }
    }
}

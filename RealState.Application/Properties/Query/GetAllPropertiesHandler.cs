using AutoMapper;
using MediatR;
using RealState.Application.Properties.Query.Dto;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Query
{
    internal class GetAllPropertiesHandler(IPropertyRepository propertyRepository, IMapper mapper) : IRequestHandler<GetAllPropertyQuery, IEnumerable<SummaryPropertiesDto>>
    {
        public async Task<IEnumerable<SummaryPropertiesDto>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            // var properties = await propertyRepository.

            return mapper.Map<IEnumerable<SummaryPropertiesDto>>(null);
        }
    }
}

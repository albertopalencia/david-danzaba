using AutoMapper;
using RealState.Application.Owners.Query.Dto;

namespace RealState.Application.Owners
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Domain.Owners.Entity.Owner, SummaryOwnerDto>(); 
        }
    }
}

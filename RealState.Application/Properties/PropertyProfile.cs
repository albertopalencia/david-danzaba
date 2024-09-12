﻿using AutoMapper;
using RealState.Application.Properties.Query.Dto;

namespace RealState.Application.Properties
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        { 
            CreateMap<Domain.Properties.Entity.Property, SummaryPropertiesDto>();
        }
    }
}

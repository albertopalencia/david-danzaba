// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyProfile.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using RealState.Application.Properties.Command;
using RealState.Application.Properties.Query;
using RealState.Application.Properties.Query.Dto;
using RealState.Domain.Properties.Dto;

namespace RealState.Application.Properties
{
    /// <summary>
    /// Class PropertyProfile.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class PropertyProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProfile"/> class.
        /// </summary>
        public PropertyProfile()
        { 
            CreateMap<Domain.Properties.Entity.Property, SummaryPropertiesDto>();
            CreateMap<Domain.Properties.Entity.Property, UpdatePropertyCommand>().ReverseMap();
            CreateMap<GetAllPropertyQuery, PropertyFilterQueryDto>();
        }
    }
}

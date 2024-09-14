// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerProfile.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using RealState.Application.Owners.Query.Dto;

namespace RealState.Application.Owners
{
    /// <summary>
    /// Class OwnerProfile.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class OwnerProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerProfile"/> class.
        /// </summary>
        public OwnerProfile()
        {
            CreateMap<Domain.Owners.Entity.Owner, SummaryOwnerDto>(); 
        }
    }
}

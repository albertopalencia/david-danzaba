// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="GetAllPropertiesHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using MediatR;
using RealState.Application.Properties.Query.Dto;
using RealState.Domain.Properties.Dto;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Query
{
    /// <summary>
    /// Class GetAllPropertiesHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Properties.Query.GetAllPropertyQuery, System.Collections.Generic.IEnumerable{RealState.Application.Properties.Query.Dto.SummaryPropertiesDto}}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Properties.Query.GetAllPropertyQuery, System.Collections.Generic.IEnumerable{RealState.Application.Properties.Query.Dto.SummaryPropertiesDto}}" />
    public class GetAllPropertiesHandler(IPropertyRepository propertyRepository, IMapper mapper) : IRequestHandler<GetAllPropertyQuery, IEnumerable<SummaryPropertiesDto>>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>IEnumerable&lt;SummaryPropertiesDto&gt;.</returns>
        public async Task<IEnumerable<SummaryPropertiesDto>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            var properties = await propertyRepository.GetPropertiesAsync(mapper.Map<PropertyFilterQueryDto>(request));

            return mapper.Map<IEnumerable<SummaryPropertiesDto>>(properties);
        }
    }
}

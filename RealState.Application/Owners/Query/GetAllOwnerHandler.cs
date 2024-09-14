// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="GetAllOwnerHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using MediatR;
using RealState.Application.Owners.Query.Dto;
using RealState.Domain.Owners.Port;

namespace RealState.Application.Owners.Query
{
    /// <summary>
    /// Class GetAllOwnerHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Owners.Query.GetAllOwnerQuery, System.Collections.Generic.IEnumerable{RealState.Application.Owners.Query.Dto.SummaryOwnerDto}}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Owners.Query.GetAllOwnerQuery, System.Collections.Generic.IEnumerable{RealState.Application.Owners.Query.Dto.SummaryOwnerDto}}" />
    public class GetAllOwnerHandler(IOwnerQueryRepository holidayRepository, IMapper mapper) : IRequestHandler<GetAllOwnerQuery, IEnumerable<SummaryOwnerDto>>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>IEnumerable&lt;SummaryOwnerDto&gt;.</returns>
        public async Task<IEnumerable<SummaryOwnerDto>> Handle(GetAllOwnerQuery request, CancellationToken cancellationToken)
        {
            var owners = await holidayRepository.GetAllAsync();
            return mapper.Map<IEnumerable<SummaryOwnerDto>>(owners);
        }
    }
}

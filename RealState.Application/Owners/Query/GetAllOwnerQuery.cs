// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="GetAllOwnerQuery.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Owners.Query.Dto;

namespace RealState.Application.Owners.Query
{
    /// <summary>
    /// Class GetAllOwnerQuery.
    /// Implements the <see cref="IRequest`1" />
    /// Implements the <see cref="System.IEquatable`1" />
    /// </summary>
    /// <seealso cref="IRequest`1" />
    /// <seealso cref="System.IEquatable`1" />
    public record GetAllOwnerQuery() : IRequest<IEnumerable<SummaryOwnerDto>>;
}

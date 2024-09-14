// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="GetAllPropertyQuery.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Properties.Query.Dto;

namespace RealState.Application.Properties.Query
{
    /// <summary>
    /// Class GetAllPropertyQuery.
    /// Implements the <see cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{RealState.Application.Properties.Query.Dto.SummaryPropertiesDto}}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{RealState.Application.Properties.Query.Dto.SummaryPropertiesDto}}" />
    public class GetAllPropertyQuery : IRequest<IEnumerable<SummaryPropertiesDto>>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string? Address { get; set; }
        /// <summary>
        /// Gets or sets the minimum price.
        /// </summary>
        /// <value>The minimum price.</value>
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// Gets or sets the maximum price.
        /// </summary>
        /// <value>The maximum price.</value>
        public decimal? MaxPrice  { get; set; }
        /// <summary>
        /// Gets or sets the maximum year.
        /// </summary>
        /// <value>The maximum year.</value>
        public int? MaxYear { get; set; }
        /// <summary>
        /// Gets or sets the minimum year.
        /// </summary>
        /// <value>The minimum year.</value>
        public int? MinYear { get; set; }
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>The owner identifier.</value>
        public string? OwnerId { get; set; }
    }
}

// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="SummaryPropertiesDto.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Domain.Owners.Entity;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Application.Properties.Query.Dto
{
    /// <summary>
    /// Class SummaryPropertiesDto.
    /// Implements the <see cref="System.IEquatable`1" />
    /// </summary>
    /// <seealso cref="System.IEquatable`1" />
    public record SummaryPropertiesDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public required string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public required string Address { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the code internal.
        /// </summary>
        /// <value>The code internal.</value>
        public string CodeInternal { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public Owner Owner { get; set; }
    }
}

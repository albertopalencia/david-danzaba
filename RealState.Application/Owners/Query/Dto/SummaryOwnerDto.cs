// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="SummaryOwnerDto.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace RealState.Application.Owners.Query.Dto
{
    /// <summary>
    /// Class SummaryOwnerDto.
    /// Implements the <see cref="System.IEquatable`1" />
    /// </summary>
    /// <seealso cref="System.IEquatable`1" />
    public record SummaryOwnerDto
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
        /// Gets or sets the photo.
        /// </summary>
        /// <value>The photo.</value>
        public required string Photo { get; set; }
        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public DateTime Birthday { get; set; }
    }
}

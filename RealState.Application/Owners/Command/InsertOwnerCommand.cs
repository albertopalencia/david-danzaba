// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertOwnerCommand.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace RealState.Application.Owners.Command
{
    /// <summary>
    /// Class InsertOwnerCommand.
    /// </summary>
    public record InsertOwnerCommand : IRequest<Guid>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>The photo.</value>
        public string Photo { get; set; }


        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public DateTime Birthday { get; set; }
    }
}

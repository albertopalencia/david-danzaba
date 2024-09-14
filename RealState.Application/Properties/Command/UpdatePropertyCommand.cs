// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="UpdatePropertyCommand.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class UpdatePropertyCommand.
    /// </summary>
    public record UpdatePropertyCommand(Guid Id, string? Name, string? Address, int Year, Guid IdOwner) : IRequest<Unit>;
}

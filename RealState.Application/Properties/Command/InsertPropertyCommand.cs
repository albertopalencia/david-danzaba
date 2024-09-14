// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="InsertPropertyCommand.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class InsertPropertyCommand.
    /// </summary>
    public record InsertPropertyCommand(string Name, string Address, decimal Price, int Year, Guid IdOwner) : IRequest<Guid>;
}

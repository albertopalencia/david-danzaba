// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="ChangePropertyPriceCommand.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class ChangePropertyPriceCommand.
    /// </summary>
    public record ChangePropertyPriceCommand(Guid PropertyId, decimal Price) : IRequest<Unit>;
}

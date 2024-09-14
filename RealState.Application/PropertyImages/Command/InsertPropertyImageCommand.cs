// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertPropertyImageCommand.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RealState.Application.PropertyImages.Command
{
    /// <summary>
    /// Class InsertPropertyImageCommand.
    /// </summary>
    public record InsertPropertyImageCommand: IRequest<Guid>
    {
       public Guid IdProperty {get;set;}
       public required IFormFile File { get; set; }
    }
}

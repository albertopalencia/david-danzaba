// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="PropertyImageController .cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Api.Filters;
using RealState.Application.PropertyImages.Command;

namespace RealState.Api.Controllers.PropertyImages;


/// <summary>
/// Class PropertyImageController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("api/[controller]")]
public class PropertyImageController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Adds the image.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [DisableRequestSizeLimit]
    [ValidateFile(2 * 1024 * 1024, [".jpg", ".jpeg", ".png"])]
    public async Task<IActionResult> AddImage([FromQuery] InsertPropertyImageCommand request)
    {
        var idPropertyImage = await mediator.Send(request);
        return Created($"/images/{idPropertyImage}", new { id = idPropertyImage, name = request.File.FileName });
    }
}
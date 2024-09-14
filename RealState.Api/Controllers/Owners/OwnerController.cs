// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerController.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Owners.Command;
using RealState.Application.Owners.Query;

namespace RealState.Api.Controllers.Owners
{
    /// <summary>
    /// Class OwnerController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await mediator.Send(new GetAllOwnerQuery());
            return Ok(owners);
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertOwnerCommand command)
        {
            var ownerId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id = ownerId }, ownerId);
        }
    }
}

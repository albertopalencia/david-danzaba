// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyController.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace RealState.Api.Controllers.Properties
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using RealState.Application.Properties.Command;
    using RealState.Application.Properties.Query;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Class PropertyController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Gets all with filters.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("filters")]
        public async Task<IActionResult> GetAllWithFilters([FromQuery] GetAllPropertyQuery query)
        {
            var properties = await mediator.Send(query);
            return Ok(properties);
        }

        /// <summary>
        /// Changes the price.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="price">The price.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{propertyId:guid}/{price:decimal}")]
        public async Task<IActionResult> ChangePrice(Guid propertyId, decimal price)
        {
            var command = new ChangePropertyPriceCommand(propertyId, price);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePropertyCommand command)
        { 
            var updatedId = await mediator.Send(command);
            return Ok(updatedId);
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertPropertyCommand command)
        {
            var createdId = await mediator.Send(command);
            return Ok(createdId);
        }
    }
}

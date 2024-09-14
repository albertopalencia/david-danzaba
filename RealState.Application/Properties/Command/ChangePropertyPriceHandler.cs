// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="ChangePropertyPriceHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class ChangePropertyPriceHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.ChangePropertyPriceCommand, MediatR.Unit}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.ChangePropertyPriceCommand, MediatR.Unit}" />
    public class ChangePropertyPriceHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangePropertyPriceCommand, Unit>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Unit.</returns>
        public async Task<Unit> Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            request.Price.ValidateGreaterThanZero("Price must be positive");

            var property = await propertyRepository.GetByIdAsync(request.PropertyId);
            property.ValidateNull("Property not found"); 

            property.ChangePrice(request.Price); 
            
            await propertyRepository.UpdateAsync(property);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}

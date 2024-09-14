// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="UpdatePropertyHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class UpdatePropertyHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.UpdatePropertyCommand, MediatR.Unit}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.UpdatePropertyCommand, MediatR.Unit}" />
    public class UpdatePropertyHandler(IPropertyRepository propertyRepository, IOwnerQueryRepository ownerRepository,
        IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePropertyCommand, Unit>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Unit.</returns>
        public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToUpdate = await propertyRepository.GetByIdAsync(request.Id);
            propertyToUpdate.ValidateNull("Property not found");

            if (request.IdOwner != default)
            {
                var owner = await ownerRepository.GetByIdAsync(request.IdOwner);
                owner.ValidateNull("Owner not found");
                propertyToUpdate.IdOwner = request.IdOwner;
            }

            propertyToUpdate.Name = request.Name ?? propertyToUpdate.Name;
            propertyToUpdate.Address = request.Address ?? propertyToUpdate.Address;
            propertyToUpdate.Year = request.Year != default ? request.Year : propertyToUpdate.Year;

            await propertyRepository.UpdateAsync(propertyToUpdate);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}

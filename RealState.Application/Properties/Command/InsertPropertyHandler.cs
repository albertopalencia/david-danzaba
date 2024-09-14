// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="InsertPropertyHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    /// <summary>
    /// Class InsertPropertyHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.InsertPropertyCommand, System.Guid}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Properties.Command.InsertPropertyCommand, System.Guid}" />
    public class InsertPropertyHandler(IPropertyRepository propertyRepository, IOwnerQueryRepository ownerRepository
        , IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyCommand, Guid>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Guid.</returns>
        public async Task<Guid> Handle(InsertPropertyCommand request, CancellationToken cancellationToken)
        {
            var owner = await ownerRepository.GetByIdAsync(request.IdOwner);
            owner.ValidateNull("Owner not found");

            var property = new Property
            {
                IdOwner = owner.Id,
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                CodeInternal = Guid.NewGuid().ToString(),
                Year = request.Year
            };

            var propertyCreated = await propertyRepository.AddAsync(property);
            await unitOfWork.SaveAsync();

            return propertyCreated.Id;
        }
    }
}

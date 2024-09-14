// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="InsertOwnerHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;

namespace RealState.Application.Owners.Command
{
    /// <summary>
    /// Class InsertOwnerHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.Owners.Command.InsertOwnerCommand, System.Guid}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.Owners.Command.InsertOwnerCommand, System.Guid}" />
    public class InsertOwnerHandler(IOwnerRepository ownerRepository, IUnitOfWork unitOfWork) : IRequestHandler<InsertOwnerCommand, Guid>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Guid.</returns>
        public async Task<Guid> Handle(InsertOwnerCommand request, CancellationToken cancellationToken)
        {
            Owner owner = new()
            {
                Address = request.Address,
                Name = request.Name,
                Photo = request.Photo,
                Birthday = request.Birthday
            };

            owner.ValidateNull("the owner cannot be null");

            var result = await ownerRepository.AddAsync(owner);
            await unitOfWork.SaveAsync();
            return result;
        }
    }
}

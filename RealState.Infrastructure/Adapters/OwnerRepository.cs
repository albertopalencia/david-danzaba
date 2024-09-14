// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerRepository.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    /// <summary>
    /// Class OwnerRepository.
    /// Implements the <see cref="IOwnerRepository" />
    /// </summary>
    /// <seealso cref="IOwnerRepository" />
    [Repository]
    public class OwnerRepository(IRepository<Owner> repository) : IOwnerRepository
    {
        /// <summary>
        /// Add as an asynchronous operation.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>A Task&lt;Guid&gt; representing the asynchronous operation.</returns>
        public async Task<Guid> AddAsync(Owner owner)
        {
            var ownerNew = await repository.AddAsync(owner);
            return ownerNew.Id;
        }

    }
}

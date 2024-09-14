// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerQueryRepository.cs" company="RealState.Infrastructure">
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
    /// Class OwnerQueryRepository.
    /// Implements the <see cref="IOwnerQueryRepository" />
    /// </summary>
    /// <seealso cref="IOwnerQueryRepository" />
    [Repository]
    public class OwnerQueryRepository(IRepository<Owner> ownerRepository) : IOwnerQueryRepository
    {
        /// <summary>
        /// Get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;Owner&gt; representing the asynchronous operation.</returns>
        public async Task<Owner> GetByIdAsync(Guid id) => await ownerRepository.GetOneAsync(id);

        /// <summary>
        /// Get all as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await ownerRepository.GetManyAsync();
        }
    }
}

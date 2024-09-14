// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyImageRepository.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    /// <summary>
    /// Class PropertyImageRepository.
    /// Implements the <see cref="IPropertyImageRepository" />
    /// </summary>
    /// <seealso cref="IPropertyImageRepository" />
    [Repository]
    public class PropertyImageRepository(IRepository<PropertyImage> repository) : IPropertyImageRepository
    {
        /// <summary>
        /// Add as an asynchronous operation.
        /// </summary>
        /// <param name="propertyImage">The property image.</param>
        /// <returns>A Task&lt;PropertyImage&gt; representing the asynchronous operation.</returns>
        public async Task<PropertyImage> AddAsync(PropertyImage propertyImage) => await repository.AddAsync(propertyImage);

        /// <summary>
        /// Get by property identifier as an asynchronous operation.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="include">The include.</param>
        /// <returns>A Task&lt;PropertyImage&gt; representing the asynchronous operation.</returns>
        public async Task<PropertyImage> GetByPropertyIdAsync(Guid propertyId, string? include = null) => await repository.GetOneAsync(propertyId, include);
         
    }
}

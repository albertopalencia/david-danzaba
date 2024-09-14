// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-14-2024
// ***********************************************************************
// <copyright file="PropertyRepository.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using RealState.Domain.Properties.Dto;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using RealState.Infrastructure.DataSource;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    /// <summary>
    /// Class PropertyRepository.
    /// Implements the <see cref="IPropertyRepository" />
    /// </summary>
    /// <seealso cref="IPropertyRepository" />
    [Repository]
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IRepository<Property> _repository;
        private readonly DataContext _dataContext;
        public PropertyRepository(IRepository<Property> repository, DataContext dataContext)
        {
            _repository = repository;
            _dataContext = dataContext;
        }

        /// <summary>
        /// Add as an asynchronous operation.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>A Task&lt;Property&gt; representing the asynchronous operation.</returns>
        public async Task<Property> AddAsync(Property property)
        {
          return  await _repository.AddAsync(property); 
        } 

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Task.</returns>
        public Task UpdateAsync(Property property)
        {
            _repository.UpdateAsync(property);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="include">The include.</param>
        /// <returns>A Task&lt;Property&gt; representing the asynchronous operation.</returns>
        public async Task<Property> GetByIdAsync(Guid id, string? include = default) => await _repository.GetOneAsync(id, include);


        /// <summary>
        /// Get properties as an asynchronous operation.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Property>> GetPropertiesAsync(PropertyFilterQueryDto query)
        {
            var propertiesQuery =  _dataContext.Property.AsQueryable();

            var properties = await propertiesQuery
                .Where(p => (string.IsNullOrEmpty(query.Name) ||
                             p.Name.Contains(query.Name)) &&
                            (string.IsNullOrEmpty(query.Address) ||
                             p.Address.Contains(query.Address)) &&
                            (!query.MinPrice.HasValue || p.Price >= query.MinPrice) &&
                            (!query.MaxPrice.HasValue || p.Price <= query.MaxPrice) &&
                            (!query.MinYear.HasValue || p.Year >= query.MinYear) &&
                            (!query.MaxYear.HasValue || p.Year <= query.MaxYear) &&
                            (string.IsNullOrEmpty(query.OwnerId) || p.IdOwner == Guid.Parse(query.OwnerId)))
                .Skip((query.Page - 1) * query.Size)
                .Take(query.Size)
                .Include(i => i.Owner)
                .ToListAsync();

            return properties;

        }
    }
}

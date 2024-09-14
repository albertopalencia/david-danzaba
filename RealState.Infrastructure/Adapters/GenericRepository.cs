// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="GenericRepository.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using RealState.Domain.Common;
using RealState.Infrastructure.DataSource;
using RealState.Infrastructure.Ports;
using System.Linq.Expressions;

namespace RealState.Infrastructure.Adapters;

/// <summary>
/// Class GenericRepository.
/// Implements the <see cref="IRepository{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IRepository{T}" />
public class GenericRepository<T> : IRepository<T> where T : DomainEntity
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly DataContext _context;
    /// <summary>
    /// The dataset
    /// </summary>
    private readonly DbSet<T> _dataset;
    /// <summary>
    /// The separator
    /// </summary>
    private readonly char[] _separator = [','];

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GenericRepository(DataContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    /// <summary>
    /// Get many as an asynchronous operation.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="orderBy">The order by.</param>
    /// <param name="includeStringProperties">The include string properties.</param>
    /// <param name="isTracking">if set to <c>true</c> [is tracking].</param>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeStringProperties = "", bool isTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeStringProperties))
        {
            query = includeStringProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync().ConfigureAwait(false);
        }

        return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

  
    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
    /// <exception cref="System.ArgumentNullException">entity - Entity can not be null</exception>
    public async Task<T> AddAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
        await _dataset.AddAsync(entity);
        return entity;
    }

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <exception cref="System.ArgumentNullException">entity - Entity can not be null</exception>
    public void DeleteAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
        _dataset.Remove(entity);
    }

    /// <summary>
    /// Get one as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="includeStringProperties">The include string properties.</param>
    /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
    public async Task<T> GetOneAsync(Guid id, string? includeStringProperties = default)
    {
        var query = _dataset.AsQueryable();

        if (string.IsNullOrEmpty(includeStringProperties))
            return await query.FirstOrDefaultAsync(entity => entity.Id == id) ?? default!;
        query = includeStringProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(entity => entity.Id == id) ?? default!;
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void UpdateAsync(T entity)
    {
        _dataset.Update(entity);
    }

    /// <summary>
    /// Gets the count asynchronous.
    /// </summary>
    /// <returns>Task&lt;System.Int32&gt;.</returns>
    public Task<int> GetCountAsync()
    {
        return _dataset.CountAsync();
    }

}

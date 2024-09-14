// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="IRepository.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Domain.Common;
using System.Linq.Expressions;

namespace RealState.Infrastructure.Ports;

/// <summary>
/// Interface IRepository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : DomainEntity
{
    /// <summary>
    /// Gets the one asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="includeStringProperties">The include string properties.</param>
    /// <returns>Task&lt;T&gt;.</returns>
    Task<T> GetOneAsync(Guid id, string? includeStringProperties = default);

    /// <summary>
    /// Gets the many asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="orderBy">The order by.</param>
    /// <param name="includeStringProperties">The include string properties.</param>
    /// <param name="isTracking">if set to <c>true</c> [is tracking].</param>
    /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
    Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeStringProperties = "",
        bool isTracking = false);


    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task&lt;T&gt;.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void UpdateAsync(T entity);
    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void DeleteAsync(T entity);
    /// <summary>
    /// Gets the count asynchronous.
    /// </summary>
    /// <returns>Task&lt;System.Int32&gt;.</returns>
    Task<int> GetCountAsync();

}

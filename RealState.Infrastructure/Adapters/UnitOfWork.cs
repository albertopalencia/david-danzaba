// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="UnitOfWork.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Application.Ports;
using RealState.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;

namespace RealState.Infrastructure.Adapters;

/// <summary>
/// Class UnitOfWork.
/// Implements the <see cref="IUnitOfWork" />
/// </summary>
/// <seealso cref="IUnitOfWork" />
public class UnitOfWork(DataContext context) : IUnitOfWork
{
    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task SaveAsync(CancellationToken? cancellationToken = null)
    {
        var token = cancellationToken ?? new CancellationTokenSource().Token;

        await context.SaveChangesAsync(token);
    }
}

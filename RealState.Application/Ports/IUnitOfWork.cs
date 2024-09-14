// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="IUnitOfWork.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace RealState.Application.Ports
{
    /// <summary>
    /// Interface IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task SaveAsync(CancellationToken? cancellationToken = null);
    }
}

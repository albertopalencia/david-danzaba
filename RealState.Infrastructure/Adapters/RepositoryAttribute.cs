// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="RepositoryAttribute.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace RealState.Infrastructure.Adapters;

/// <summary>
/// Class RepositoryAttribute. This class cannot be inherited.
/// Implements the <see cref="System.Attribute" />
/// </summary>
/// <seealso cref="System.Attribute" />
[AttributeUsage(AttributeTargets.Class)]
public sealed class RepositoryAttribute : Attribute
{
}

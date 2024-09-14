// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-14-2024
// ***********************************************************************
// <copyright file="DataContext.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Properties.Entity;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyTraces.Entity;

namespace RealState.Infrastructure.DataSource;

/// <summary>
/// Class DataContext.
/// Implements the <see cref="DbContext" />
/// </summary>
/// <seealso cref="DbContext" />
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Property> Property { get; set; }

    /// <summary>
    /// Called when [model creating].
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        modelBuilder.Entity<Owner>();
        modelBuilder.Entity<Property>();
        modelBuilder.Entity<PropertyTrace>();
        modelBuilder.Entity<PropertyImage>();

        base.OnModelCreating(modelBuilder);
    }
}


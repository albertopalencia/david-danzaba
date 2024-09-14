// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyTraceConfig.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.PropertyTraces.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig;

/// <summary>
/// Class PropertyTraceConfig.
/// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.PropertyTraces.Entity.PropertyTrace}" />
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.PropertyTraces.Entity.PropertyTrace}" />
public class PropertyTraceConfig : IEntityTypeConfiguration<PropertyTrace>
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder.HasKey(property => property.Id);
        builder.Property(property => property.Id)
            .HasColumnName("IdPropertyTrace")
            .HasValueGenerator<GuidValueGenerator>()
            .ValueGeneratedOnAdd();
        ConfigureRelationships(builder);
    }

    /// <summary>
    /// Configures the relationships.
    /// </summary>
    /// <param name="builder">The builder.</param>
    private static void ConfigureRelationships(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder.HasOne(r => r.Property)
            .WithOne()
            .HasForeignKey<PropertyTrace>(r => r.IdProperty)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
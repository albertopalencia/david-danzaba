// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyImageConfig.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    /// <summary>
    /// Class PropertyImageConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.PropertyImages.Entity.PropertyImage}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.PropertyImages.Entity.PropertyImage}" />
    internal class PropertyImageConfig : IEntityTypeConfiguration<PropertyImage>
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                .HasColumnName("IdPropertyImage")
                .HasValueGenerator<GuidValueGenerator>()
                .ValueGeneratedOnAdd();

            builder.Property(h => h.File).IsRequired();
            builder.Property(h => h.Enabled).HasDefaultValue(true);
        }
    }
}

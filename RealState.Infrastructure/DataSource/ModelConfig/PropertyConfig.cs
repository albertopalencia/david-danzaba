// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyConfig.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.Properties.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    /// <summary>
    /// Class PropertyConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.Properties.Entity.Property}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.Properties.Entity.Property}" />
    internal class PropertyConfig : IEntityTypeConfiguration<Property>
    {

        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property");

            builder.HasKey(property => property.Id);

            builder.Property(property => property.Id)
                .HasColumnName("IdProperty")
                .HasValueGenerator<GuidValueGenerator>()
                .ValueGeneratedOnAdd();

            ConfigureRelationships(builder); 
        }

        /// <summary>
        /// Configures the relationships.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void ConfigureRelationships(EntityTypeBuilder<Property> builder)
        {
            builder.HasOne(r => r.Owner)
                .WithOne()
                .HasForeignKey<Property>(r => r.IdOwner);

            builder.HasMany(r => r.PropertyImages)
                .WithOne(r => r.Property)
                .HasForeignKey(r => r.IdProperty);
        }
    }
}

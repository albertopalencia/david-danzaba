// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerConfig.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.Owners.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    /// <summary>
    /// Class OwnerConfig.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.Owners.Entity.Owner}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RealState.Domain.Owners.Entity.Owner}" />
    internal class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(s => s.Id)
                .HasName("IdOwner");
            
            builder.Property(s => s.Id)
                .HasValueGenerator<GuidValueGenerator>()
                .ValueGeneratedOnAdd(); 

            builder.Property(s => s.Name);
            builder.Property(s => s.Address).IsRequired();
            builder.Property(s => s.Birthday).IsRequired();
            builder.Property(s => s.Photo).IsRequired();
        }
    }
}

// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="20240912085003_InitialCreate.Designer.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealState.Infrastructure.DataSource;

#nullable disable

namespace RealState.Infrastructure.Migrations
{
    /// <summary>
    /// Class InitialCreate.
    /// Implements the <see cref="Migration" />
    /// </summary>
    /// <seealso cref="Migration" />
    [DbContext(typeof(DataContext))]
    [Migration("20240912085003_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealState.Domain.Owners.Entity.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("IdOwner");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("RealState.Domain.Properties.Entity.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdProperty");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdOwner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPropertyImage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdOwner")
                        .IsUnique();

                    b.ToTable("Property");
                });

            modelBuilder.Entity("RealState.Domain.PropertyImages.Entity.PropertyImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdPropertyImage");

                    b.Property<bool>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("IdProperty")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdProperty");

                    b.ToTable("PropertyImage");
                });

            modelBuilder.Entity("RealState.Domain.PropertyTraces.Entity.PropertyTrace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdPropertyTrace");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdProperty")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdProperty")
                        .IsUnique();

                    b.ToTable("PropertyTrace");
                });

            modelBuilder.Entity("RealState.Domain.Properties.Entity.Property", b =>
                {
                    b.HasOne("RealState.Domain.Owners.Entity.Owner", "Owner")
                        .WithOne()
                        .HasForeignKey("RealState.Domain.Properties.Entity.Property", "IdOwner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RealState.Domain.PropertyImages.Entity.PropertyImage", b =>
                {
                    b.HasOne("RealState.Domain.Properties.Entity.Property", "Property")
                        .WithMany("PropertyImages")
                        .HasForeignKey("IdProperty")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealState.Domain.PropertyTraces.Entity.PropertyTrace", b =>
                {
                    b.HasOne("RealState.Domain.Properties.Entity.Property", "Property")
                        .WithOne()
                        .HasForeignKey("RealState.Domain.PropertyTraces.Entity.PropertyTrace", "IdProperty")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealState.Domain.Properties.Entity.Property", b =>
                {
                    b.Navigation("PropertyImages");
                });
#pragma warning restore 612, 618
        }
    }
}

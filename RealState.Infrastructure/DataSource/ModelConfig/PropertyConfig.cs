using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.Properties.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    internal class PropertyConfig : IEntityTypeConfiguration<Property>
    {

        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(property => property.Id);

            builder.Property(property => property.Id)
                .HasColumnName("IdProperty")
                .HasValueGenerator<GuidValueGenerator>()
                .ValueGeneratedOnAdd();

            ConfigureRelationships(builder); 
        }

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

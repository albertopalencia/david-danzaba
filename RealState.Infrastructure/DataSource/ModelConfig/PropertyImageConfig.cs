using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    internal class PropertyImageConfig : IEntityTypeConfiguration<PropertyImage>
    {  
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.PropertyTraces.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig;

public class PropertyTraceConfig : IEntityTypeConfiguration<PropertyTrace>
{
    public void Configure(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder.HasKey(property => property.Id);
        builder.Property(property => property.Id)
            .HasColumnName("IdPropertyTrace")
            .HasValueGenerator<GuidValueGenerator>()
            .ValueGeneratedOnAdd();
        ConfigureRelationships(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder.HasOne(r => r.Property)
            .WithOne()
            .HasForeignKey<PropertyTrace>(r => r.IdProperty)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
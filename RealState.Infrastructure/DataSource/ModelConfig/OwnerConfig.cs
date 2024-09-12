using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RealState.Domain.Owners.Entity;

namespace RealState.Infrastructure.DataSource.ModelConfig
{
    internal class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
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

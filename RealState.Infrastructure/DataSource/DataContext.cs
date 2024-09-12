using Microsoft.EntityFrameworkCore;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Properties.Entity;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyTraces.Entity;

namespace RealState.Infrastructure.DataSource;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

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


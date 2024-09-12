using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    [Repository]
    public class PropertyImageRepository(IRepository<PropertyImage> repository) : IPropertyImageRepository
    {
        public async Task<PropertyImage> AddAsync(PropertyImage propertyImage) => await repository.AddAsync(propertyImage);

        public async Task<PropertyImage> GetByPropertyIdAsync(Guid propertyId, string? include = null) => await repository.GetOneAsync(propertyId, include);
         
    }
}

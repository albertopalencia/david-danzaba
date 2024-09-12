using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    [Repository]
    public class PropertyRepository(IRepository<Property> propertyRepository) : IPropertyRepository
    {
        public async Task<Property> AddAsync(Property property) => await propertyRepository.AddAsync(property);

        public Task updateAsync(Property property)
        {
            propertyRepository.UpdateAsync(property);
            return Task.CompletedTask;
        }

        public async Task<Property> GetByIdAsync(Guid id, string? include = default) => await propertyRepository.GetOneAsync(id, include);

    }
}

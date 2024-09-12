using RealState.Domain.Properties.Entity;

namespace RealState.Domain.Properties.Port
{
    public interface IPropertyRepository
    {
        Task<Property> AddAsync(Property property);
         
        Task  UpdateAsync(Property property);

        Task<Property> GetByIdAsync(Guid id, string? include = default);
    }
}

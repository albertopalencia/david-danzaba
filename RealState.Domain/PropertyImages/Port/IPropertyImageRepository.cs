using RealState.Domain.PropertyImages.Entity;

namespace RealState.Domain.PropertyImages.Port
{
     
    public interface IPropertyImageRepository
    { 
        Task<PropertyImage> GetByPropertyIdAsync(Guid propertyId, string? include = null); 
         
        Task<PropertyImage> AddAsync(PropertyImage propertyImage); 
    }
}

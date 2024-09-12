using RealState.Domain.Common;
using RealState.Domain.Owners.Entity;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Domain.Properties.Entity
{
    public class Property : DomainEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Guid IdOwner { get; set; }
        public Guid IdPropertyImage { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
    }
}

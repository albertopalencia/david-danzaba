using RealState.Domain.Common;
using RealState.Domain.Owners.Entity;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Domain.Properties.Entity
{
    public class Property : DomainEntity
    {
        public  string? Name { get; set; }  = default!;
        public  string Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; } = Guid.NewGuid().ToString();  
        public int Year { get; set; }
        public Guid IdOwner { get; set; } 
        public virtual Owner Owner { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }

        public void ChangePrice(decimal price)
        {
            Price = price;
        }
    }
}

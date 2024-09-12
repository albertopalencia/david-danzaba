using RealState.Domain.Common;
using RealState.Domain.Properties.Entity;

namespace RealState.Domain.PropertyTraces.Entity
{
    public class PropertyTrace : DomainEntity
    { 
        public DateTime DateSale { get; set; }
        public required string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public Guid IdProperty { get; set; }
        public Property Property { get; set; }
    }
}
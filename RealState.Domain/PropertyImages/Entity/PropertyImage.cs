using RealState.Domain.Common;
using RealState.Domain.Properties.Entity;

namespace RealState.Domain.PropertyImages.Entity
{
     
    public class PropertyImage : DomainEntity
    {  
        public Guid IdProperty { get; set; }
       
        public required byte[] File { get; set; }
        public bool Enabled { get; set; }
        public Property Property { get; set; }
    }
}

using RealState.Domain.Common;

namespace RealState.Domain.Owners.Entity
{
    public class Owner : DomainEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}

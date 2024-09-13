using RealState.Domain.Owners.Entity;
using RealState.Domain.PropertyImages.Entity;

namespace RealState.Application.Properties.Query.Dto
{
    public record SummaryPropertiesDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Owner Owner { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
    }
}

namespace RealState.Application.Owners.Query.Dto
{
    public record SummaryOwnerDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}

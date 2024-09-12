namespace RealState.Application.Owners.Query.Dto
{
    public record SummaryOwnerDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = default!;
    }
}

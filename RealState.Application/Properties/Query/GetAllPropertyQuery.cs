using MediatR;
using RealState.Application.Properties.Query.Dto;

namespace RealState.Application.Properties.Query
{
    public class GetAllPropertyQuery : IRequest<IEnumerable<SummaryPropertiesDto>>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice  { get; set; } 
        public int? MaxYear { get; set; }
        public int? MinYear { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string? OwnerId { get; set; }
    }
}

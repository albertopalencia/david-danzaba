using MediatR;
using RealState.Application.Properties.Query.Dto;

namespace RealState.Application.Properties.Query
{
    public class GetAllPropertyFilter 
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllPropertyQuery : IRequest<IEnumerable<SummaryPropertiesDto>>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

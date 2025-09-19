using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionByDateRangeQuery : IRequest<List<HeroSectionResponse>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public GetHeroSectionByDateRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}

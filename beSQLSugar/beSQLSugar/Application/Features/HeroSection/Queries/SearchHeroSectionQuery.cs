using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class SearchHeroSectionQuery : IRequest<List<HeroSectionResponse>>
    {
        public string Keyword { get; set; }
        public SearchHeroSectionQuery(string keyword)
        {
            Keyword = keyword;
        }
    }
}

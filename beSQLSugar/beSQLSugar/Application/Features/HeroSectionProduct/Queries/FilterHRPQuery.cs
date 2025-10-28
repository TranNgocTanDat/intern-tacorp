using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Queries
{
    public class FilterHRPQuery : IRequest<List<HeroSectionProductResponse>>
    {
        public HeroSectionProductFilterRequest? FilterRequest { get; set; }
        public FilterHRPQuery(HeroSectionProductFilterRequest? filterRequest)
        {
            FilterRequest = filterRequest;
        }
    }
}

using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Queries
{
    public class FilterSpecQuery : IRequest<List<ProductSpecResponse>>
    {
        public ProductSpecFilterRequest? FilterRequest { get; set; }
        public FilterSpecQuery(ProductSpecFilterRequest? filterRequest)
        {
            FilterRequest = filterRequest;
        }
    }
}

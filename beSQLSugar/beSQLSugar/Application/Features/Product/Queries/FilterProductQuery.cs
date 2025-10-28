using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class FilterProductQuery : IRequest<List<ProductResponse>>
    {
        public ProductFilterRequest? FilterRequest { get; set; }
        public FilterProductQuery(ProductFilterRequest? filterRequest)
        {
            FilterRequest = filterRequest;
        }
    }
}

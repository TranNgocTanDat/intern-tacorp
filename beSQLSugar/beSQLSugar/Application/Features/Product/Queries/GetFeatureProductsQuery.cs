using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class GetFeatureProductsQuery : IRequest<List<ProductResponse>>
    {
        public GetFeatureProductsQuery() { }
    }
}

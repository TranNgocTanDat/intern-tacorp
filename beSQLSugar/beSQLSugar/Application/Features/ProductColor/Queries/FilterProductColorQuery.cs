using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Queries
{
    public class FilterProductColorQuery : IRequest<List<ProductColorResponse>>
    {
        public ProductColorFilterRequest? Request { get; set; }
        public FilterProductColorQuery(ProductColorFilterRequest? request)
        {
            Request = request;
        }
    }
}

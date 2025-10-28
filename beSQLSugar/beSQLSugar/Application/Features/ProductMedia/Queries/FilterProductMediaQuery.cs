using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Queries
{
    public class FilterProductMediaQuery : IRequest<List<ProductMediaResponse>>
    {
        public ProductMediaFilterRequest? Request { get; set; }
        public FilterProductMediaQuery(ProductMediaFilterRequest? request)
        {
            Request = request;
        }
    }
}

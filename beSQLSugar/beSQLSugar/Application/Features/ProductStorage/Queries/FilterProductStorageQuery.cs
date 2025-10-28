using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductStorage;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Queries
{
    public class FilterProductStorageQuery : IRequest<List<ProductStorageResponse>>
    {
        public ProductStorageFilterRequest Request { get; set; }
        public FilterProductStorageQuery(ProductStorageFilterRequest request)
        {
            Request = request;
        }
    }
}

using beSQLSugar.Application.Dto.response.ProductStorage;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Queries
{
    public class GetAllProductStorageQuery : IRequest<List<ProductStorageResponse>>
    {
        public GetAllProductStorageQuery() { }
    }
}

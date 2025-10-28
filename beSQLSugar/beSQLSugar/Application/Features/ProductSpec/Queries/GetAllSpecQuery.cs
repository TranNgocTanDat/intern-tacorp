using beSQLSugar.Application.Dto.response.ProductSpec;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Queries
{
    public class GetAllSpecQuery : IRequest<List<ProductSpecResponse>>
    {
        public GetAllSpecQuery() { }
    }
}

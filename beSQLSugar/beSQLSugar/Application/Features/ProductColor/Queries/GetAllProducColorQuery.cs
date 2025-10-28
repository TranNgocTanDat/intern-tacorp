using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Queries
{
    public class GetAllProducColorQuery : IRequest<List<ProductColorResponse>>
    {
        public GetAllProducColorQuery() { }
    }
}

using beSQLSugar.Application.Dto.response.ProductMedia;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Queries
{
    public class GetAllMediaProductQuery : IRequest<List<ProductMediaResponse>>
    {
        public GetAllMediaProductQuery() { }
    }
}

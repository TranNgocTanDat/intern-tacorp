using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class GetAllProductQuery : IRequest<List<ProductResponse>>
    {
        // Không cần tham số, lấy tất cả sản phẩm
        public GetAllProductQuery() { }
    }
}

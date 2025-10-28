using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class GetProductBySlugQuery : IRequest<ProductResponse>
    {
        public string Slug { get; set; }
        public GetProductBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}

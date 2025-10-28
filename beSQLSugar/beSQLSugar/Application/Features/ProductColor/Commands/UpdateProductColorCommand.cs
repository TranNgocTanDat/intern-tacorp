using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Commands
{
    public class UpdateProductColorCommand : IRequest<ProductColorResponse>
    {
        public int Id { get; set; }
        public ProductColorRequest Request { get; set; }

        public UpdateProductColorCommand(int id, ProductColorRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}

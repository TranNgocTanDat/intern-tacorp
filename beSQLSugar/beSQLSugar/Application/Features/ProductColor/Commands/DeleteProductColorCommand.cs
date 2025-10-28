using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Commands
{
    public class DeleteProductColorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteProductColorCommand(int id)
        {
            Id = id;
        }
    }
}

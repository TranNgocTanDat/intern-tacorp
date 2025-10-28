using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Commands
{
    public class DeleteProductStorageCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public DeleteProductStorageCommand(int id)
        {
            Id = id;
        }
    }
}

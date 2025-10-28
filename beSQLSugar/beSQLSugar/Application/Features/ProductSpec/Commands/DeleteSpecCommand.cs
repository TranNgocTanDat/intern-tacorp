using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Commands
{
    public class DeleteSpecCommand : IRequest<bool>
    {
        public int SpecId { get; set; }
        public DeleteSpecCommand(int specId)
        {
            SpecId = specId;
        }
    }
  
}

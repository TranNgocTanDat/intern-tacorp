using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Commands
{
    public class DeleteMediaCommand : IRequest<bool>
    {
        public int MediaId { get; set; }
        public DeleteMediaCommand(int mediaId)
        {
            MediaId = mediaId;
        }
    }
  
}

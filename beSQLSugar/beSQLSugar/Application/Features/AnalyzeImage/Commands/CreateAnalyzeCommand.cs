using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;
using MediatR;

namespace beSQLSugar.Application.Features.AnalyzeImage.Commands
{
    public class CreateAnalyzeCommand : IRequest<AnalyzeImageResponse>
    {
        public AnalyzeImageRequest Request { get; set; }
        public CreateAnalyzeCommand(AnalyzeImageRequest request)
        {
            Request = request;
        }
    }
}

using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;
using MediatR;

namespace beSQLSugar.Application.Features.AnalyzeImage.Commands
{
    public class FindPathCommand : IRequest<FindPathResponse>
    {
        public FindPathRequest Request { get; set; }
        public FindPathCommand(FindPathRequest request)
        {
            Request = request;
        }
    }
}

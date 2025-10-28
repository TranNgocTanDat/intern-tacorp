using beSQLSugar.Application.Dto.response.AnalyzeImage;
using beSQLSugar.Application.Features.AnalyzeImage.Commands;
using beSQLSugar.Application.Services.AnalyzerImageServices;
using MediatR;

namespace beSQLSugar.Application.Features.AnalyzeImage.Handlers
{
    public class AnalyzeCommandHandler : 
        IRequestHandler<CreateAnalyzeCommand, AnalyzeImageResponse>,
        IRequestHandler<FindPathCommand, FindPathResponse>
    {
        private readonly IAnalyzerImageSerivce _analyzerImageSerivce;
        public AnalyzeCommandHandler(IAnalyzerImageSerivce analyzerImageSerivce)
        {
            _analyzerImageSerivce = analyzerImageSerivce;
        }
        public async Task<AnalyzeImageResponse> Handle(CreateAnalyzeCommand request, CancellationToken cancellationToken)
        {
            return await _analyzerImageSerivce.AddAsync(request.Request);
        }

        public async Task<FindPathResponse> Handle(FindPathCommand request, CancellationToken cancellationToken)
        {
            return await _analyzerImageSerivce.FindPathAsync(request.Request);
        }
    }
}

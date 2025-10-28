using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public interface IAnalyzerImageSerivce
    {
        Task<AnalyzeImageResponse> AddAsync(AnalyzeImageRequest request);

        Task<FindPathResponse> FindPathAsync(FindPathRequest request);
    }
}

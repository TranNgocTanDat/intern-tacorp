using beSQLSugar.Application.Dto.response.AnalyzeImage;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public interface IImageService
    {
        AImageResult Analyze(string FilePathUrl, string filePathMap);
    }
}

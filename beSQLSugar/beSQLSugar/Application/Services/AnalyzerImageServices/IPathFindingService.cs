using beSQLSugar.Application.Dto.response.AnalyzeImage;

namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public interface IPathFindingService
    {
        FindPathResponse FindPath(int[,] grid, int startX, int startY, int endX, int endY);
    }
}

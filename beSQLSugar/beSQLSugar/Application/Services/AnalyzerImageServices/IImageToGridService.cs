namespace beSQLSugar.Application.Services.AnalyzerImageServices
{
    public interface IImageToGridService
    {
         int[,] ConvertImageToGrid(string filePath);
    }
}

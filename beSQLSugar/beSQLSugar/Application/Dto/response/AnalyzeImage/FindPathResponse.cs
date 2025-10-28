namespace beSQLSugar.Application.Dto.response.AnalyzeImage
{
    public class FindPathResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public List<PathPoint>? Path { get; set; }
    }

    public class PathPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

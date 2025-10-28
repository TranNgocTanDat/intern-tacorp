namespace beSQLSugar.Application.Dto.request.AnalyzeImage
{
    public class FindPathRequest
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int ImageId { get; set; }
    }
}

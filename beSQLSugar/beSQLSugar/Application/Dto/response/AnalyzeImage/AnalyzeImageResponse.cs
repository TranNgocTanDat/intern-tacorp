namespace beSQLSugar.Application.Dto.response.AnalyzeImage
{
    public class AnalyzeImageResponse
    {
        public int Id { get; set; }
        public bool IsMapLike { get; set; }
        public string? FilePathUrl { get; set; }
        public string? FilePathMap { get; set; }
        public string? Message { get; set; }

    }
}

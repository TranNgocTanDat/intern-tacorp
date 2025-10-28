namespace beSQLSugar.Application.Dto.response.AnalyzeImage
{
    public class AImageResult
    {
        public bool IsMap { get; set; }
        public string CleanMaskPath { get; set; } = string.Empty;
        public string ResizedImage { get; set; } = string.Empty;
        public string ResizedImageMap { get; set; } = string.Empty;
        public int LineCount { get; set; }
        public int PolygonCount { get; set; }
        public double EdgeDensity { get; set; }
        public string? Message { get; set; }
    }
}

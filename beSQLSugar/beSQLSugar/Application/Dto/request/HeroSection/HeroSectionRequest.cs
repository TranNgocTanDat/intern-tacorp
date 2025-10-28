namespace beSQLSugar.Application.Dto.request.HeroSection
{
    public class HeroSectionRequest
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public IFormFile? HeroMediaFile { get; set; } 
        public string? PageHero { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishFrom { get; set; }
        public DateTime? PublishTo { get; set; }
        public string? Note { get; set; }

    }
}

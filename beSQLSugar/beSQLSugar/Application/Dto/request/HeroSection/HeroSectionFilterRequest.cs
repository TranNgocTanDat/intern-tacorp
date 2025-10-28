namespace beSQLSugar.Application.Dto.request.HeroSection
{
    public class HeroSectionFilterRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PageHero { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishFrom { get; set; }
        public DateTime? PublishTo { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
    }
}

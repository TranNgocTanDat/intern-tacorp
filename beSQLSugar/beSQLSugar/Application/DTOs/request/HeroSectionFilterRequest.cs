namespace beSQLSugar.Application.DTOs.request
{
    public class HeroSectionFilterRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PageHero { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishFrom { get; set; }
        public DateTime? PublishTo { get; set; }
    }
}

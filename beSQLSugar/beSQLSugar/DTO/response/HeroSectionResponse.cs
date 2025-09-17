using beSQLSugar.Models;

namespace beSQLSugar.DTO.response
{
    public class HeroSectionResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? HeroMediaUrl { get; set; }
        public string? PageHero { get; set; }
        public string? HeroMediaType { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishFrom { get; set; }
        public DateTime? PublishTo { get; set; }
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? Note { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }

        public List<HeroSectionProduct>? HeroProducts { get; set; }
    }
}

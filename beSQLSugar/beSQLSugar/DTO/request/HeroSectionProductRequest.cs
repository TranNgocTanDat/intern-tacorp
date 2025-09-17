namespace beSQLSugar.DTO.request
{
    public class HeroSectionProductRequest
    {
        public int HeroSectionId { get; set; }
        public int ProductId { get; set; }
        public int OrderIndex { get; set; } = 0;
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? Note { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
    }
}

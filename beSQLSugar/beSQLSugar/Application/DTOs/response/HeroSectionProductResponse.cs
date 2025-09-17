namespace beSQLSugar.Application.DTO.response
{
    public class HeroSectionProductResponse
    {
        public int Id { get; set; }
        public int HeroSectionId { get; set; }
        public int ProductId { get; set; }
        public int OrderIndex { get; set; }
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

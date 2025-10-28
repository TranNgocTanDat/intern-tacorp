namespace beSQLSugar.Application.Dto.response.Partner
{
    public class PartnerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? ImgDefaultUrl { get; set; }
        public string? ImgHoverUrl { get; set; }
        public string? Slug { get; set; }
        public string? Link { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
        public string? Note { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
    }
}

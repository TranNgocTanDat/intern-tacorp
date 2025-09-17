namespace beSQLSugar.DTO.request
{
    public class PartnerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Link { get; set; }
        public int OrderIndex { get; set; } = 0;
        public bool IsActive { get; set; } = true;
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

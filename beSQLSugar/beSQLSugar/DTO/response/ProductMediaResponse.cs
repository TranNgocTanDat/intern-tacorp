namespace beSQLSugar.DTO.response
{
    public class ProductMediaResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? MediaFileUrl { get; set; }
        public string? MediaType { get; set; }
        public bool IsPrimary { get; set; }
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

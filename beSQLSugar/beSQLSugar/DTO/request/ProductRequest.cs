namespace beSQLSugar.DTO.request
{
    public class ProductRequest
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public decimal? Price { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int ViewsCount { get; set; } = 0;
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

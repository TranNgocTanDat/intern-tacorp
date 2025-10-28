namespace beSQLSugar.Application.Dto.request.ProductMedia
{
    public class ProductMediaFilterRequest
    {
        // Cho phép lọc theo ProductId hoặc ProductName
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? DescriptionMedia { get; set; }

        public string? MediaFileUrl { get; set; }

        public string? MediaType { get; set; } // "image" | "video"

        public bool? IsPrimary { get; set; }

        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }

        public string? Note { get; set; }
    }
}

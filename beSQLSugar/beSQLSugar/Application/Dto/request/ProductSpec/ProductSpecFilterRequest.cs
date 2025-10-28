namespace beSQLSugar.Application.Dto.request.ProductSpec
{
    public class ProductSpecFilterRequest
    {
        // Lọc theo ProductId hoặc ProductName
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }

        // Các trường filter chính
        public string? SpecKey { get; set; }
        public string? SpecValue { get; set; }
        public int? OrderIndex { get; set; }

        // Metadata
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }
        public string? Note { get; set; }
    }
}

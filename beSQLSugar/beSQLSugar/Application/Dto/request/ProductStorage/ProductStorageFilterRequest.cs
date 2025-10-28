namespace beSQLSugar.Application.Dto.request.ProductStorage
{
    public class ProductStorageFilterRequest
    {
        public int ProductId { get; set; }                   // ID sản phẩm
        public string? ProductName { get; set; }
        public string StorageName { get; set; } = string.Empty; // Ví dụ: "128GB"
        public decimal? AdditionalPrice { get; set; }        // Giá cộng thêm
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }

        public string? Note { get; set; }
    }
}

namespace beSQLSugar.Application.Dto.response.ProductStorage
{
    public class ProductStorageResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string StorageName { get; set; } = string.Empty; // "128GB", "256GB"
        public decimal? AdditionalPrice { get; set; }           // Giá cộng thêm
        public bool IsAvailable { get; set; } = true;

        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
    }
}

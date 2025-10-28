namespace beSQLSugar.Application.Dto.request.ProductStorage
{
    public class ProductStorageRequest
    {
        public int ProductId { get; set; }                   // ID sản phẩm
        public string StorageName { get; set; } = string.Empty; // Ví dụ: "128GB"
        public decimal? AdditionalPrice { get; set; }        // Giá cộng thêm
        public bool IsAvailable { get; set; } = true;        // Còn hàng hay không
        public string? Note { get; set; }
    }
}

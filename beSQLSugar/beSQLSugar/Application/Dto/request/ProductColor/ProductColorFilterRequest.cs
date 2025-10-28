namespace beSQLSugar.Application.Dto.request.ProductColor
{
    public class ProductColorFilterRequest
    {
        public int ProductId { get; set; }               // ID sản phẩm
        public string? ProductName { get; set; }
        public string ColorName { get; set; } = string.Empty; // Tên màu: "Trắng Titan"
        public string? ColorCode { get; set; }           // Mã màu: "#FFFFFF"
        public bool IsAvailable { get; set; } = true;    // Có sẵn hay không
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }

        public string? Note { get; set; }
    }
}

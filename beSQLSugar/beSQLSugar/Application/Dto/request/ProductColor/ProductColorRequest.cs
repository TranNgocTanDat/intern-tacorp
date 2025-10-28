namespace beSQLSugar.Application.Dto.request.ProductColor
{
    public class ProductColorRequest
    {
        public int ProductId { get; set; }               // ID sản phẩm
        public string ColorName { get; set; } = string.Empty; // Tên màu: "Trắng Titan"
        public string? ColorCode { get; set; }           // Mã màu: "#FFFFFF"
        public bool IsAvailable { get; set; } = true;    // Có sẵn hay không
        public string? Note { get; set; }
    }
}

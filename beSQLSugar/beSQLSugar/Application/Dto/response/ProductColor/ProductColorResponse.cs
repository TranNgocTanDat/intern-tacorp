using beSQLSugar.Application.Dto.response.ProductMedia;

namespace beSQLSugar.Application.Dto.response.ProductColor
{
    public class ProductColorResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string ColorName { get; set; } = string.Empty; // Ví dụ: "Trắng Titan"
        public string? ColorCode { get; set; }                // Ví dụ: "#FFFFFF"
        public bool IsAvailable { get; set; } = true;

        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        // 🔗 Nếu muốn hiển thị danh sách media cho màu này
        public List<ProductMediaResponse>? MediaList { get; set; } = new();
    }
}

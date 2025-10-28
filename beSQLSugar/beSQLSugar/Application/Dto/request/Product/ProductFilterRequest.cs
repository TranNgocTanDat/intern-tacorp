namespace beSQLSugar.Application.Dto.request.Product
{
    public class ProductFilterRequest
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }

        // Lọc theo khoảng giá
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public bool? IsFeatured { get; set; }
        public bool? IsActive { get; set; }

        // Lọc theo khoảng views
        public int? MinViewsCount { get; set; }
        public int? MaxViewsCount { get; set; }

        // User tạo/sửa
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        // Lọc theo thời gian
        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }

        public string? Note { get; set; }

    }
}

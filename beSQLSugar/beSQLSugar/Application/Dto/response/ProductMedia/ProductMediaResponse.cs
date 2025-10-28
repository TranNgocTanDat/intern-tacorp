namespace beSQLSugar.Application.Dto.response.ProductMedia
{
    public class ProductMediaResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? MediaFileUrl { get; set; }
        public string? MediaType { get; set; }
        public bool IsPrimary { get; set; }
        public string? DescriptionMedia { get; set; }
        public int OrderIndex { get; set; }
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        // Thông tin về màu
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }

    }
}

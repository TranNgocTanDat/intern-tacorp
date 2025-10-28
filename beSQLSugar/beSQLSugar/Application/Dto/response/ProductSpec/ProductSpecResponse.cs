namespace beSQLSugar.Application.Dto.response.ProductSpec
{
    public class ProductSpecResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string SpecKey { get; set; } = string.Empty;
        public string SpecValue { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

    }
}

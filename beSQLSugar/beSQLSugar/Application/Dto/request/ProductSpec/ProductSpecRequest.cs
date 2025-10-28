namespace beSQLSugar.Application.Dto.request.ProductSpec
{
    public class ProductSpecRequest
    {
        public string SpecKey { get; set; } = string.Empty;
        public string? SpecValue { get; set; }
        public int OrderIndex { get; set; } = 0;
        public string? Note { get; set; }

    }
}

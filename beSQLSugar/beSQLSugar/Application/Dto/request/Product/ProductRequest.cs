namespace beSQLSugar.Application.Dto.request.Product
{
    public class ProductRequest
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }

    }
}

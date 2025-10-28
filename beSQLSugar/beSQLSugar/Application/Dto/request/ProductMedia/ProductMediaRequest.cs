namespace beSQLSugar.Application.Dto.request.ProductMedia
{
    public class ProductMediaRequest
    {
        public IFormFile? MediaFileUrl { get; set; }
        public int ColorId { get; set; } 
        public string? MediaType { get; set; } // "image" or "video"
        public bool IsPrimary { get; set; } = false;
        public string? DescriptionMedia { get; set; }
        public int OrderIndex { get; set; } = 0;
        public string? Note { get; set; }

    }
}

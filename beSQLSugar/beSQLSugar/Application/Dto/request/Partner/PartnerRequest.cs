namespace beSQLSugar.Application.Dto.request.Partner
{
    public class PartnerRequest
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? LogoFile { get; set; }
        public IFormFile? ImgDefaultFile { get; set; }
        public IFormFile? ImgHoverFile { get; set; }
        public string? Slug { get; set; }
        public string? Link { get; set; }
        public int OrderIndex { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }
      
    }
}

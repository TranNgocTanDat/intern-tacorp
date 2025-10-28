namespace beSQLSugar.Application.Dto.request.Category
{
    public class CategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? PartnerId { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }
    }
}

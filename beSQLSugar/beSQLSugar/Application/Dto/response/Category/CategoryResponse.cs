using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Application.Dto.response.Product;

namespace beSQLSugar.Application.Dto.response.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public int? CreateUid { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
        public string? Note { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }

        public List<CategoryResponse>? Children { get; set; } = new();
        public List<ProductResponse>? Products { get; set; } = new();
        public PartnerResponse? Partner { get; set; }
    }
}

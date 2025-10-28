namespace beSQLSugar.Application.Dto.request.Category
{
    public class CategoryFilterRequest
    {
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentId { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
        public DateTime? UpdateTimeFrom { get; set; }
        public DateTime? UpdateTimeTo { get; set; }
        public string? Note { get; set; }


    }
}

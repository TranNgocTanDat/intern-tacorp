namespace beSQLSugar.Application.DTOs.request
{
    public class CategoryFilterRequest
    {
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

    }
}

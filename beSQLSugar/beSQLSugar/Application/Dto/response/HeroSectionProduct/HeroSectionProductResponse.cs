using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Application.Dto.response.Product;

namespace beSQLSugar.Application.Dto.response.HeroSectionProduct
{
    public class HeroSectionProductResponse
    {
        public int Id { get; set; }
        public int OrderIndex { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public HeroSectionResponse? HeroSection { get; set; }
        public ProductResponse? Product { get; set; }
        
    }
}

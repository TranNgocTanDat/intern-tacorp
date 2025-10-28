namespace beSQLSugar.Application.Dto.request.HeroSectionProduct
{
    public class HeroSectionProductFilterRequest
    {
        public string? HeroSectionTitle { get; set; }
        public string? ProductName { get; set; }

        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }

        public DateTime? UpdateTimeFrom { get; set; }
        public DateTime? UpdateTimeTo { get; set; }

        // Tìm theo note (ghi chú)
        public string? Note { get; set; }
    }
}

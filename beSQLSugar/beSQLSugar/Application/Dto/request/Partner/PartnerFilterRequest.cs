namespace beSQLSugar.Application.Dto.request.Partner
{
    public class PartnerFilterRequest
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedName { get; set; }
        public string? UpdatedName { get; set; }
        public DateTime? UpdateTimeFrom { get; set; }
        public DateTime? UpdateTimeTo { get; set; }
        public string? Note { get; set; }
    }
}

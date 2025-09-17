namespace beSQLSugar.DTO.request
{
    public class DeviceRequest
    {
        public string? CodeDevice { get; set; } = string.Empty;
        public string? NameDevice { get; set; } = string.Empty;
        public int? DeviceTypeId { get; set; }

        public string? NameParent { get; set; }
        public bool? IsStatus { get; set; } = true;
        public string? StatusUseDevice { get; set; }
        public string? PingStatus { get; set; }
        public string? SnmpStatus { get; set; }
        public string? CheckTime { get; set; }
        public string? IpAddress { get; set; }
        public string? Port { get; set; }
        public string? ComPort { get; set; }
        public string? OID { get; set; }
        public string? Vendor { get; set; }
        public string? PathUrl { get; set; }
        public string? Protocol { get; set; }
    }
}

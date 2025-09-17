
namespace beSQLSugar.DTO.response
{
    public class DeviceResponse
    {
        public int Id { get; set; }

        public string CodeDevice { get; set; } = string.Empty;

        public string NameDevice { get; set; } = string.Empty;

        public int DeviceTypeId { get; set; }
        public string? DeviceTypeName { get; set; }

        public string? NamePraentt { get; set; }

        public bool IsStatus { get; set; } = true;

        public string? StatusUseDevice { get; set; }

        public string? PingStatus { get; set; }

        public string? SnmpStatus { get; set; }

        public string? CheckTime { get; set; }

        public string? IpAddress { get; set; }

        public string? Port { get; set; }

        public string? ComPort { get; set; }

        public string? OID { get; set; }

        public string? Vendor { get; set; }

        public string? path_url { get; set; }

        public string? Protocol { get; set; }

    }
}

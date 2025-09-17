using SqlSugar;

namespace beSQLSugar.models
{
    public class Device
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 100, IsNullable = false)]
        public string CodeDevice { get; set; } = string.Empty;

        [SugarColumn(Length = 200, IsNullable = false)]
        public string NameDevice { get; set; } = string.Empty;

        [SugarColumn(IsNullable = false)]
        public int DeviceTypeId { get; set; }

        [SugarColumn(IsNullable = true)]
        public DeviceType? DeviceType { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string? NamePraentt { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool IsStatus { get; set; } = true;

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? StatusUseDevice { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? PingStatus { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? SnmpStatus { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? CheckTime { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? IpAddress { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? Port { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? ComPort { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? OID { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? Vendor { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? path_url { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string? Protocol { get; set; }

    }
}

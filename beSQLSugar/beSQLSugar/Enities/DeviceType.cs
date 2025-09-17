using SqlSugar;

namespace beSQLSugar.models
{
    public class DeviceType
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 100, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        [SugarColumn(Length = 255, IsNullable = true)]
        public string? Description { get; set; }

        //Navigation property
        [SugarColumn(IsIgnore = true)]
        public ICollection<Device>? Devices { get; set; }
    }
}

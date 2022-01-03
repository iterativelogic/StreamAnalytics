namespace StreamAnalytics.System.Data.Entities
{
  public partial class MasterStreamType
  {
    public Guid StreamTypeId { get; set; }
    public string StreamTypeName { get; set; } = null!;
    public Guid DataTypeId { get; set; }

    public virtual DataType DataType { get; set; } = null!;
  }
}

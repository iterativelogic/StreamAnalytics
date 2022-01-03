namespace StreamAnalytics.System.Data.Entities
{
  public partial class OpcTagStream
  {
    public Guid TenantId { get; set; }
    public Guid TagId { get; set; }
    public Guid StreamTypeId { get; set; }
    public Guid? UnitId { get; set; }

    public virtual StreamType StreamType { get; set; } = null!;
    public virtual OpcTag T { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual MeasurementUnit? Unit { get; set; }
  }
}

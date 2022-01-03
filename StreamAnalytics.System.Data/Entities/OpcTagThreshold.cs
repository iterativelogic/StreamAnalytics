namespace StreamAnalytics.System.Data.Entities
{
  public partial class OpcTagThreshold
  {
    public Guid TenantId { get; set; }
    public Guid ThresholdTagId { get; set; }
    public Guid? OpcTagId { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public virtual OpcTag? OpcTag { get; set; }
    public virtual OpcTag T { get; set; } = null!;
  }
}

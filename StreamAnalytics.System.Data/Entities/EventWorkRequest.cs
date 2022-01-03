namespace StreamAnalytics.System.Data.Entities
{
  public partial class EventWorkRequest
  {
    public Guid TenantId { get; set; }
    public Guid EventId { get; set; }
    public string WorkRequestNo { get; set; } = null!;
    public DateTimeOffset CreatedTimestamp { get; set; }
    public Guid CreatedBy { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
  }
}

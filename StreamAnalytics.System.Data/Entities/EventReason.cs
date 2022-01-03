namespace StreamAnalytics.System.Data.Entities
{
  public partial class EventReason
  {
    public EventReason()
    {
      OpcTagEventEventReasons = new HashSet<OpcTagEvent>();
      OpcTagEventTNavigations = new HashSet<OpcTagEvent>();
    }

    public Guid TenantId { get; set; }
    public Guid EventReasonId { get; set; }
    public string EventReasonName { get; set; } = null!;
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedTimestamp { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTimeOffset UpdatedTimestamp { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<OpcTagEvent> OpcTagEventEventReasons { get; set; }
    public virtual ICollection<OpcTagEvent> OpcTagEventTNavigations { get; set; }
  }
}

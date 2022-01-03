namespace StreamAnalytics.System.Data.Entities
{
  public partial class OpcTagEvent
  {
    public Guid TenantId { get; set; }
    public Guid TagId { get; set; }
    public Guid EventTypeId { get; set; }
    public Guid? TrueAnnotation { get; set; }
    public Guid? FalseAnnotation { get; set; }

    public virtual EventReason? EventReason { get; set; }
    public virtual EventType EventType { get; set; } = null!;
    public virtual OpcTag T { get; set; } = null!;
    public virtual EventReason? TNavigation { get; set; }
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

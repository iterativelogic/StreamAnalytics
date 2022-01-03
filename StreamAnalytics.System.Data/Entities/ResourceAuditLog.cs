namespace StreamAnalytics.System.Data.Entities
{
  public partial class ResourceAuditLog
  {
    public Guid TenantId { get; set; }
    public long ResourceAuditLogKey { get; set; }
    public Guid ResourceId { get; set; }
    public string TableName { get; set; } = null!;
    public Guid AccountId { get; set; }
    public string ActionType { get; set; } = null!;
    public DateTimeOffset ActionDate { get; set; }
  }
}

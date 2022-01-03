namespace StreamAnalytics.System.Data.Entities
{
  public partial class AuditLog
  {
    public long AuditLogKey { get; set; }
    public Guid TenantId { get; set; }
    public Guid AccountId { get; set; }
    public string EntityName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string? OldEntity { get; set; }
    public string? NewEntity { get; set; }
    public DateTimeOffset CreatedTimeStamp { get; set; }
  }
}

namespace StreamAnalytics.System.Data.Entities
{
  public partial class EnterpriseTenantActive
  {
    public Guid EnterpriseId { get; set; }
    public string EnterpriseName { get; set; } = null!;
    public Guid TenantId { get; set; }
    public string TenantName { get; set; } = null!;
  }
}

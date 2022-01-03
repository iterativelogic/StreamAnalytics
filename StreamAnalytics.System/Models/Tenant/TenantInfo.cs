namespace StreamAnalytics.System.Models.Tenant
{
  public struct TenantInfo
  {
    public TenantInfo(TenantId tenantId, AccountId accountId)
    {
      TenantId = tenantId;
      AccountId = accountId;
    }

    public TenantInfo(Guid tenantId, Guid accountId)
    {
      TenantId = tenantId;
      AccountId = accountId;
    }

    public TenantInfo(string tenantId, string accountId)
    {
      TenantId = tenantId;
      AccountId = accountId;
    }

    public TenantId TenantId { get; init; }
    public AccountId AccountId { get; init; }
  }
}

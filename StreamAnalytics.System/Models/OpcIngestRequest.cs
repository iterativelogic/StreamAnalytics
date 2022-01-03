using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Models
{
  public class OpcIngestRequest
  {
    public TenantId TenantId { get; set; }
    public AccountId AccountId { get; set; }
    public Guid GatewayId { get; set; }
    public OpcIngestPayload Payload { get; set; }
  }
}

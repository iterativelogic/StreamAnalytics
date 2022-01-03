using StreamAnalytics.System.Models;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Ingest.System.Dataflow.Objects
{
  public class IngestDataTransform
  {
    public TenantId TenantId { get; set; }
    public AccountId AccountId { get; set; }
    public Guid GatewayId { get; set; }
    public List<OpcIngestData> DataList { get; set; } = new List<OpcIngestData>();
  }
}

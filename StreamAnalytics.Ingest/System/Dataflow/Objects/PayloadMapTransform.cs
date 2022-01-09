using StreamAnalytics.System.Models;
using StreamAnalytics.System.Models.Assets;
using StreamAnalytics.System.Models.Tags;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Ingest.System.Dataflow.Objects
{
  public class PayloadMapTransform
  {
    public TenantId TenantId { get; set; }
    public AccountId AccountId { get; set; }
    public Guid GatewayId { get; set; }
    public OpcIngestData Payload { get; set; }
    public Tag Tag { get; set; }

    public EventData ConvertToEvent()
    {
      return new EventData {
        TenantId = TenantId,
        TagName = Tag.TagName,
        GatewayId = GatewayId,
        AssetId = Tag?.PhysicalAssetId,
        EventTypeId = Tag.StreamType,
        DataType = Tag.DataTypeId,
        Severity = Tag.SeverityId,
        Timestamp = DateTimeOffset.FromUnixTimeSeconds(Payload.Timestamp).ToUnixTimeMilliseconds(),
        Value = Payload.Value.ToString(),
        Quality = Payload.Quality,
      };
    }
  }
}

using System.Text.Json.Serialization;
using StreamAnalytics.System.Models.Tenant;
using StreamAnalytics.System.Serialization;

namespace StreamAnalytics.Ingest.System
{
  public class EventData
  {
    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid EventId { get; set; } = Guid.NewGuid();

    public TenantId TenantId { get; set; }

    public string TagName { get; set; }

    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid GatewayId { get; set; }

    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid? AssetId { get; set; }

    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid EventTypeId { get; set; }

    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid DataType { get; set; }

    [JsonConverter(typeof(GuidUpperCaseJsonConverter))]
    public Guid? Severity { get; set; }

    public string EventReason { get; set; }

    public long Timestamp { get; set; }

    public string Value { get; set; }

    public bool Quality { get; set; }
  }
}

using System.Text.Json.Serialization;

namespace StreamAnalytics.System.Models
{
  public class OpcIngestPayload
  {
    [JsonPropertyName("st")]
    public long Timestamp { get; set; }

    [JsonPropertyName("f")]
    public OpcIngestData[] Data { get; set; }
  }
}

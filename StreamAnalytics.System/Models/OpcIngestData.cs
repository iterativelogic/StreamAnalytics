using System.Text.Json.Serialization;
using StreamAnalytics.System.Serialization;

namespace StreamAnalytics.System.Models
{
  public class OpcIngestData
  {
    [JsonPropertyName("id")]
    public string TagName { get; set; }

    [JsonPropertyName("q")]
    public bool Quality { get; set; }

    [JsonPropertyName("v")]
    [JsonConverter(typeof(IngestDataValueJsonConverter))]
    public string Value { get; set; }

    [JsonPropertyName("t")]
    public long Timestamp { get; set; }
  }
}

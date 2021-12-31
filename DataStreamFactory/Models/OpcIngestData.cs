using System.Text.Json.Serialization;

namespace DataStreamFactory.Models
{
    public class OpcIngestData
    {
        [JsonPropertyName("id")]
        public string TagName { get; set; }

        [JsonPropertyName("q")]
        public bool Quality { get; set; }

        [JsonPropertyName("v")]
        public bool Value { get; set; }

        [JsonPropertyName("t")]
        public long Timestamp { get; set; }
    }
}

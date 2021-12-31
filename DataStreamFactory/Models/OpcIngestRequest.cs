using System.Text.Json.Serialization;

namespace DataStreamFactory.Models
{
    public class OpcIngestRequest
    {
        [JsonPropertyName("st")]
        public long Timestamp { get; set; }

        [JsonPropertyName("f")]
        public OpcIngestData[] Data { get; set; }
    }
}

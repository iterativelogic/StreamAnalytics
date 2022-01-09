using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamAnalytics.System.Serialization
{
  public class IngestDataValueJsonConverter : JsonConverter<string>
  {
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      => reader.TokenType switch {
        JsonTokenType.Number => reader.GetInt32().ToString(),
        JsonTokenType.String => reader.GetString().ToString(),
        JsonTokenType.True => reader.GetBoolean().ToString(),
        JsonTokenType.False => reader.GetBoolean().ToString(),
        _ => throw new NotSupportedException()
      };


    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value);
    }
  }
}

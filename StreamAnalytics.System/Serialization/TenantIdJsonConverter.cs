using System.Text.Json;
using System.Text.Json.Serialization;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Serialization
{
  public class TenantIdJsonConverter : JsonConverter<TenantId>
  {
    public override TenantId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetGuid();
    }

    public override void Write(Utf8JsonWriter writer, TenantId value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString().ToUpper());
    }
  }
}

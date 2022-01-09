using System.Text.Json;
using System.Text.Json.Serialization;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Serialization
{
  public class AccountIdJsonConverter : JsonConverter<AccountId>
  {
    public override AccountId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetGuid();
    }

    public override void Write(Utf8JsonWriter writer, AccountId value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}

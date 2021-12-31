using DataStreamFactory.Models;
using System.Text.Json;

namespace DataStreamFactory.System
{
  public class StreamFactory
  {
    public OpcIngestRequest CreateStream(string name)
    {
      var data = new OpcIngestData
      {
        TagName = name,
        Value = true,
        Quality = true,
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
      };

      return new OpcIngestRequest
      {
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        Data = new OpcIngestData[]
        {
          data
        }
      };
    }

    public string CreateStreamJson(string name)
    {
      var streamObject = CreateStream(name);

      var op = new JsonSerializerOptions 
      {
        WriteIndented = true
      };

      string json = JsonSerializer.Serialize(streamObject, op);
      return json;
    }
  }
}

using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;

namespace StreamAnalytics.Ingest.System.Producer
{
  public class OpcIngestSinkDataProducer
  {
    private readonly ILogger<OpcIngestSinkDataProducer> logger;
    private readonly IProducer<string, string> producer;

    public OpcIngestSinkDataProducer(ILogger<OpcIngestSinkDataProducer> logger)
    {
      this.logger = logger;

      var producerBuilder = new ProducerBuilder<string, string>(new ProducerConfig {
        BootstrapServers = "localhost:9093"
      });

      producer = producerBuilder.Build();
    }

    public async Task ProduceAsync(EventData data, string topic)
    {

      var options = new JsonSerializerOptions {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };

      var value = JsonSerializer.Serialize(data, options);

      logger.LogInformation($"Producing message for {topic}: {value}");

      var message = new Message<string, string> {
        Key = null,
        Value = value
      };

      var dr = await producer.ProduceAsync(topic, message);

      logger.LogInformation($"Message delivered to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");
    }
  }
}

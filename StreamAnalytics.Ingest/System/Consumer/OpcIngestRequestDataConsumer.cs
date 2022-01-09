using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;
using Confluent.Kafka;
using StreamAnalytics.Ingest.System.Dataflow;
using StreamAnalytics.System.Constants;
using StreamAnalytics.System.Models;

namespace StreamAnalytics.Ingest.System.Consumer
{
  public class OpcIngestRequestDataConsumer
  {
    private readonly IConsumer<string, string> _consumer;
    private readonly IngestPipeline pipeline;
    private readonly ILogger<OpcIngestRequestDataConsumer> logger;

    public OpcIngestRequestDataConsumer(IngestPipeline pipeline, ILogger<OpcIngestRequestDataConsumer> logger)
    {
      var consumerBuilder = new ConsumerBuilder<string, string>(new ConsumerConfig {
        BootstrapServers = "localhost:9093",
        GroupId = "TEST_GROUP"
      });

      _consumer = consumerBuilder.Build();
      _consumer.Subscribe(KafkaTopics.CM_OPC_INGEST_REQUEST);

      this.pipeline = pipeline;
      this.logger = logger;
      logger.LogInformation($"Initialized {nameof(OpcIngestRequestDataConsumer)}");
    }

    public async Task ConsumeStreamAsync(CancellationToken ct)
    {
      await foreach (var item in ConsumeAsync(ct)) {
        var request = JsonSerializer.Deserialize<OpcIngestRequest>(item.Message.Value);

        var isAccepted = await pipeline.BufferBlock.SendAsync(request);

        _consumer.Commit(item);

        logger.LogInformation($"Accepted: {isAccepted}");
        logger.LogInformation(request.TenantId.ToString());
      }
    }

    public async IAsyncEnumerable<ConsumeResult<string, string>> ConsumeAsync([EnumeratorCancellation] CancellationToken ct)
    {
      while (!ct.IsCancellationRequested) {
        var result = await Task.Run(() => _consumer.Consume(ct), ct);

        if (result == null || result.IsPartitionEOF)
          continue;

        yield return result;
      }
    }
  }
}

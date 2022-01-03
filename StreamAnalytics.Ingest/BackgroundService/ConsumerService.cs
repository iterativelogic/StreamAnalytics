using StreamAnalytics.Ingest.System.Consumer;

namespace StreamAnalytics.Ingest.BackgroundService
{
  public class ConsumerService : Microsoft.Extensions.Hosting.BackgroundService
  {
    private readonly OpcIngestRequestDataConsumer consumer;
    private readonly ILogger<ConsumerService> logger;

    public ConsumerService(OpcIngestRequestDataConsumer consumer, ILogger<ConsumerService> logger)
    {
      this.consumer = consumer;
      this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      logger.LogInformation("STARTED THE KAFKA CONSUMER");
      await consumer.ConsumeStreamAsync(stoppingToken);
      logger.LogInformation("STOPPED THE KAFKA CONSUMER");
    }
  }
}

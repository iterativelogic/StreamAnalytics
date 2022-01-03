using StreamAnalytics.System.Data.Entities;

namespace StreamAnalytics.Ingest.System.Producer
{
  public class ProduceKafkaMessage
  {
    public void Access()
    {
      StreamAnalyticsDbContext dbContext = new StreamAnalyticsDbContext();
      var tags = dbContext.OpcTags.ToList();
    }
  }
}

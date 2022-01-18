using StreamAnalytics.Core.Models;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Core.Services
{
  public class DruidDataService
  {
    public async Task<IEnumerable<EventData>> GetLogs(TenantInfo tenant, CancellationToken ct)
    {
      var logs = new List<EventData>();

      for (int i = 0; i < 10; i++) {
        logs.Add(new EventData {
          Timestamp = DateTimeOffset.UtcNow,
          TagName = "Test.Tag",
          AssetName = "Test.Asset",
          StreamType = "Problem",
          State = true,
          Severity = "High",
          Source = "CEP"
        });
      }

      return await Task.FromResult(logs);
    }
  }
}

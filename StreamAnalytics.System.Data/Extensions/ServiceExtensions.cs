using Microsoft.Extensions.DependencyInjection;
using StreamAnalytics.System.Data.Entities;

namespace StreamAnalytics.System.Data.Extensions
{
  public static class ServiceExtensions
  {
    public static void RegisterDbContext(this IServiceCollection services)
    {
      services.AddDbContext<StreamAnalyticsDbContext>();
    }
  }
}

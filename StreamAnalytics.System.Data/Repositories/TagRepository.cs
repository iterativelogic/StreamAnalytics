using Microsoft.EntityFrameworkCore;
using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Data.Extensions.Mapping;
using StreamAnalytics.System.Models.Tags;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Data.Repositories
{
  public class TagRepository
  {
    private readonly StreamAnalyticsDbContext dbContext;

    public TagRepository(StreamAnalyticsDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync(TenantId tenantId, Guid gatewayId, IEnumerable<string> tagNames)
    {
      var tags = await dbContext.OpcTags
        .Where(tag => tag.TenantId == tenantId.Value
          && tag.IoTGatewayId == gatewayId
          && tagNames.Contains(tag.TagName))
        .ToListAsync();

      return tags.Select(tag => tag.Map()).ToList();
    }
  }
}

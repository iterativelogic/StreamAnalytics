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
        .AsNoTracking()
        .ToListAsync();

      return tags.Select(tag => tag.Map()).ToList();
    }

    public async Task CreateTagsAsync(IEnumerable<Tag> tags)
    {
      var newTagEntities = tags
        .Select(tag => tag.ToEntity())
        .ToList();

      dbContext.OpcTags.AddRange(newTagEntities);
      await dbContext.SaveChangesAsync();
    }

    public async Task UpdateTagsAsync(IEnumerable<Tag> tags, IEnumerable<Tag> existingTags)
    {
      var tagGroups = tags
        .GroupBy(tag => existingTags.Any(existingTag => tag.TenantId == existingTag.TenantId
          && tag.GatewayId == existingTag.GatewayId
          && string.Equals(tag.TagName, existingTag.TagName, StringComparison.OrdinalIgnoreCase)));

      foreach (var tagGroup in tagGroups) {
        var entities = tagGroup
          .Select(tag => tag.ToEntity())
          .ToList();

        if (tagGroup.Key) {
          dbContext.OpcTags.UpdateRange(entities);
        }
        else {
          dbContext.OpcTags.AddRange(entities);
        }
      }

      await dbContext.SaveChangesAsync();
    }
  }
}

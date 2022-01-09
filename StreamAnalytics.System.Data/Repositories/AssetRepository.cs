using Microsoft.EntityFrameworkCore;
using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Data.Extensions.Mapping;
using StreamAnalytics.System.Models.Assets;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Data.Repositories
{
  public class AssetRepository
  {
    private readonly StreamAnalyticsDbContext dbContext;

    public AssetRepository(StreamAnalyticsDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Asset>> GetAssetsAsync(TenantId tenantId, IEnumerable<Guid?> assetIds)
    {
      var assets = await dbContext.PhysicalAssets
        .Where(asset => asset.TenantId == tenantId.Value && assetIds.Contains(asset.PhysicalAssetId))
        .AsNoTracking()
        .ToListAsync();

      return assets.Select(asset => asset.Map()).ToList();
    }
  }
}

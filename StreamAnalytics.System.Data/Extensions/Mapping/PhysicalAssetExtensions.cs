using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Models.Assets;

namespace StreamAnalytics.System.Data.Extensions.Mapping
{
  public static class PhysicalAssetExtensions
  {
    public static Asset Map(this PhysicalAsset asset)
      => new Asset {
        TenantId = asset.TenantId,
        PhysicalAssetId = asset.PhysicalAssetId,
        AssetName = asset.PhysicalAssetName,
        AssetCode = asset.AssetCode,
        Description = asset.Description,
        SerialNumber = asset.AssetSerialNumber,
        IsActive = asset.Active.Value
      };
  }
}

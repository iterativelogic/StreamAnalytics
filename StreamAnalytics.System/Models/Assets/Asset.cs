using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Models.Assets
{
  public class Asset
  {
    public TenantId TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public string AssetName { get; set; }
    public string AssetCode { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public bool IsActive { get; set; }
  }
}

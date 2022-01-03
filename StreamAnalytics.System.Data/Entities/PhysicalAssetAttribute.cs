namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetAttribute
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid AttributeValueId { get; set; }

    public virtual AttributeValue AttributeValue { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

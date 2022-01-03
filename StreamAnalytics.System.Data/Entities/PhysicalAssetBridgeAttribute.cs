namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetBridgeAttribute
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid BridgeAttributeId { get; set; }
    public Guid PhysicalAssetBridgeAttributeId { get; set; }
    public string? ValuePayload { get; set; }

    public virtual BridgeAttribute BridgeAttribute { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

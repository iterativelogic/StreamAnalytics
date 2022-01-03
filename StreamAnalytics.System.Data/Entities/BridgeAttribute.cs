namespace StreamAnalytics.System.Data.Entities
{
  public partial class BridgeAttribute
  {
    public BridgeAttribute()
    {
      PhysicalAssetBridgeAttributes = new HashSet<PhysicalAssetBridgeAttribute>();
    }

    public Guid BridgeAttributeId { get; set; }
    public string BridgeAttributeName { get; set; } = null!;

    public virtual ICollection<PhysicalAssetBridgeAttribute> PhysicalAssetBridgeAttributes { get; set; }
  }
}

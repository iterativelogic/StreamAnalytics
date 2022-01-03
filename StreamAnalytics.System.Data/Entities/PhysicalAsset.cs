namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAsset
  {
    public PhysicalAsset()
    {
      OpcTags = new HashSet<OpcTag>();
      PhysicalAssetAttributes = new HashSet<PhysicalAssetAttribute>();
      PhysicalAssetBridgeAttributes = new HashSet<PhysicalAssetBridgeAttribute>();
      PhysicalAssetIoTGatewayReportedStatusR1s = new HashSet<PhysicalAssetIoTGatewayReportedStatusR1>();
      PhysicalAssetIoTGateways = new HashSet<PhysicalAssetIoTGateway>();
      TagEventData = new HashSet<TagEventDatum>();
    }

    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public string PhysicalAssetName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string AssetSerialNumber { get; set; } = null!;
    public bool? Active { get; set; }
    public string AssetCode { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
    public virtual PhysicalAssetOpcStatusConfigR1 PhysicalAssetOpcStatusConfigR1 { get; set; } = null!;
    public virtual PhysicalAssetOpcStatusPayloadR1 PhysicalAssetOpcStatusPayloadR1 { get; set; } = null!;
    public virtual ICollection<OpcTag> OpcTags { get; set; }
    public virtual ICollection<PhysicalAssetAttribute> PhysicalAssetAttributes { get; set; }
    public virtual ICollection<PhysicalAssetBridgeAttribute> PhysicalAssetBridgeAttributes { get; set; }
    public virtual ICollection<PhysicalAssetIoTGatewayReportedStatusR1> PhysicalAssetIoTGatewayReportedStatusR1s { get; set; }
    public virtual ICollection<PhysicalAssetIoTGateway> PhysicalAssetIoTGateways { get; set; }
    public virtual ICollection<TagEventDatum> TagEventData { get; set; }
  }
}

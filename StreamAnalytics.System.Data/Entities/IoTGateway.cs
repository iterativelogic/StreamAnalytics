namespace StreamAnalytics.System.Data.Entities
{
  public partial class IoTGateway
  {
    public IoTGateway()
    {
      OpcTags = new HashSet<OpcTag>();
      PhysicalAssetIoTGatewayReportedStatusR1s = new HashSet<PhysicalAssetIoTGatewayReportedStatusR1>();
      PhysicalAssetIoTGateways = new HashSet<PhysicalAssetIoTGateway>();
      PhysicalAssetOpcStatusConfigR1s = new HashSet<PhysicalAssetOpcStatusConfigR1>();
      PhysicalAssetOpcStatusPayloadR1s = new HashSet<PhysicalAssetOpcStatusPayloadR1>();
    }

    public Guid TenantId { get; set; }
    public Guid IoTGatewayId { get; set; }
    public string GatewayName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool? Active { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<OpcTag> OpcTags { get; set; }
    public virtual ICollection<PhysicalAssetIoTGatewayReportedStatusR1> PhysicalAssetIoTGatewayReportedStatusR1s { get; set; }
    public virtual ICollection<PhysicalAssetIoTGateway> PhysicalAssetIoTGateways { get; set; }
    public virtual ICollection<PhysicalAssetOpcStatusConfigR1> PhysicalAssetOpcStatusConfigR1s { get; set; }
    public virtual ICollection<PhysicalAssetOpcStatusPayloadR1> PhysicalAssetOpcStatusPayloadR1s { get; set; }
  }
}

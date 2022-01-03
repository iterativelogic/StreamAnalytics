namespace StreamAnalytics.System.Data.Entities
{
  public partial class Tenant
  {
    public Tenant()
    {
      AttributeValues = new HashSet<AttributeValue>();
      Attributes = new HashSet<Attribute>();
      EventReasons = new HashSet<EventReason>();
      EventWorkRequests = new HashSet<EventWorkRequest>();
      IoTGateways = new HashSet<IoTGateway>();
      OpcTagEvents = new HashSet<OpcTagEvent>();
      OpcTagStreams = new HashSet<OpcTagStream>();
      PhysicalAssetAttributes = new HashSet<PhysicalAssetAttribute>();
      PhysicalAssetBridgeAttributes = new HashSet<PhysicalAssetBridgeAttribute>();
      PhysicalAssetIoTGatewayReportedStatusR1s = new HashSet<PhysicalAssetIoTGatewayReportedStatusR1>();
      PhysicalAssetIoTGateways = new HashSet<PhysicalAssetIoTGateway>();
      PhysicalAssetOpcStatusConfigR1s = new HashSet<PhysicalAssetOpcStatusConfigR1>();
      PhysicalAssetOpcStatusPayloadR1s = new HashSet<PhysicalAssetOpcStatusPayloadR1>();
      PhysicalAssets = new HashSet<PhysicalAsset>();
      StreamTypes = new HashSet<StreamType>();
      UserAccounts = new HashSet<UserAccount>();
    }

    public Guid TenantId { get; set; }
    public Guid EnterpriseId { get; set; }
    public string TenantName { get; set; } = null!;
    public bool? Active { get; set; }
    public bool? PmcEnabled { get; set; }
    public string? TenantCode { get; set; }

    public virtual Enterprise Enterprise { get; set; } = null!;
    public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    public virtual ICollection<Attribute> Attributes { get; set; }
    public virtual ICollection<EventReason> EventReasons { get; set; }
    public virtual ICollection<EventWorkRequest> EventWorkRequests { get; set; }
    public virtual ICollection<IoTGateway> IoTGateways { get; set; }
    public virtual ICollection<OpcTagEvent> OpcTagEvents { get; set; }
    public virtual ICollection<OpcTagStream> OpcTagStreams { get; set; }
    public virtual ICollection<PhysicalAssetAttribute> PhysicalAssetAttributes { get; set; }
    public virtual ICollection<PhysicalAssetBridgeAttribute> PhysicalAssetBridgeAttributes { get; set; }
    public virtual ICollection<PhysicalAssetIoTGatewayReportedStatusR1> PhysicalAssetIoTGatewayReportedStatusR1s { get; set; }
    public virtual ICollection<PhysicalAssetIoTGateway> PhysicalAssetIoTGateways { get; set; }
    public virtual ICollection<PhysicalAssetOpcStatusConfigR1> PhysicalAssetOpcStatusConfigR1s { get; set; }
    public virtual ICollection<PhysicalAssetOpcStatusPayloadR1> PhysicalAssetOpcStatusPayloadR1s { get; set; }
    public virtual ICollection<PhysicalAsset> PhysicalAssets { get; set; }
    public virtual ICollection<StreamType> StreamTypes { get; set; }
    public virtual ICollection<UserAccount> UserAccounts { get; set; }
  }
}

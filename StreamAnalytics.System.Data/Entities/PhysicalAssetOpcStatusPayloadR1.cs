namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetOpcStatusPayloadR1
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid IoTGatewayId { get; set; }
    public string Payload { get; set; } = null!;
    public DateTimeOffset UpdateDate { get; set; }

    public virtual IoTGateway IoTGateway { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetIoTGateway
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid IoTGatewayId { get; set; }

    public virtual IoTGateway IoTGateway { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

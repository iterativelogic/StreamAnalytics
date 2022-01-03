namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetOpcStatusConfigR1
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid IoTGatewayId { get; set; }
    public string InCycleStatusTag { get; set; } = null!;
    public string IdleStatusTag { get; set; } = null!;
    public string OffStatusTag { get; set; } = null!;
    public string ProblemStatusTag { get; set; } = null!;

    public virtual IoTGateway IoTGateway { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

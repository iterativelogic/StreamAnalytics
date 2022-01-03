namespace StreamAnalytics.System.Data.Entities
{
  public partial class PhysicalAssetIoTGatewayReportedStatusR1
  {
    public Guid TenantId { get; set; }
    public Guid PhysicalAssetId { get; set; }
    public Guid IoTGatewayId { get; set; }
    public bool InCycleStatus { get; set; }
    public bool IdleStatus { get; set; }
    public bool OffStatus { get; set; }
    public bool ProblemStatus { get; set; }
    public DateTimeOffset ReportedDate { get; set; }

    public virtual IoTGateway IoTGateway { get; set; } = null!;
    public virtual PhysicalAsset PhysicalAsset { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
  }
}

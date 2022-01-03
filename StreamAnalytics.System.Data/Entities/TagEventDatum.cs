namespace StreamAnalytics.System.Data.Entities
{
  public partial class TagEventDatum
  {
    public Guid TagEventId { get; set; }
    public Guid TenantId { get; set; }
    public string? TagName { get; set; }
    public Guid? IoTGatewayId { get; set; }
    public Guid? PhysicalAssetId { get; set; }
    public Guid? EventTypeId { get; set; }
    public Guid? DataTypeId { get; set; }
    public bool? Value { get; set; }
    public bool? Quality { get; set; }
    public Guid? Severity { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public Guid OpcGatewayId { get; set; }
    public string? EventReason { get; set; }
    public Guid? EventId { get; set; }

    public virtual DataType? DataType { get; set; }
    public virtual PhysicalAsset? PhysicalAsset { get; set; }
  }
}

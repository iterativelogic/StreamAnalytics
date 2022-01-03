namespace StreamAnalytics.System.Data.Entities
{
  public partial class OpcTag
  {
    public OpcTag()
    {
      OpcTagThresholdOpcTags = new HashSet<OpcTagThreshold>();
    }

    public Guid TenantId { get; set; }
    public Guid TagId { get; set; }
    public Guid IoTGatewayId { get; set; }
    public Guid? PhysicalAssetId { get; set; }
    public Guid? EventTypeId { get; set; }
    public string TagName { get; set; } = null!;
    public Guid? DataTypeId { get; set; }
    public Guid? StreamTypeId { get; set; }
    public string? ValuePayload { get; set; }
    public Guid? SeverityId { get; set; }
    public string? TagAlias { get; set; }
    public string? Value { get; set; }
    public string? Quality { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public string? LastGoodValue { get; set; }
    public DateTimeOffset? LastGoodTimestamp { get; set; }

    public virtual DataType? DataType { get; set; }
    public virtual EventType? EventType { get; set; }
    public virtual IoTGateway IoTGateway { get; set; } = null!;
    public virtual PhysicalAsset? PhysicalAsset { get; set; }
    public virtual StreamType? StreamType { get; set; }
    public virtual OpcTagEvent OpcTagEvent { get; set; } = null!;
    public virtual OpcTagStream OpcTagStream { get; set; } = null!;
    public virtual OpcTagThreshold OpcTagThresholdT { get; set; } = null!;
    public virtual ICollection<OpcTagThreshold> OpcTagThresholdOpcTags { get; set; }
  }
}

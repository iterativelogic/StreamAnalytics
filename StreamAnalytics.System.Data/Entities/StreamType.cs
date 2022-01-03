namespace StreamAnalytics.System.Data.Entities
{
  public partial class StreamType
  {
    public StreamType()
    {
      OpcTagStreams = new HashSet<OpcTagStream>();
      OpcTags = new HashSet<OpcTag>();
    }

    public Guid TenantId { get; set; }
    public Guid StreamTypeId { get; set; }
    public string StreamTypeName { get; set; } = null!;
    public Guid DataTypeId { get; set; }
    public bool IsDefaultStream { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? CreatedTimestamp { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedTimestamp { get; set; }

    public virtual DataType DataType { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<OpcTagStream> OpcTagStreams { get; set; }
    public virtual ICollection<OpcTag> OpcTags { get; set; }
  }
}

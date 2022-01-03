namespace StreamAnalytics.System.Data.Entities
{
  public partial class DataType
  {
    public DataType()
    {
      MasterStreamTypes = new HashSet<MasterStreamType>();
      OpcTags = new HashSet<OpcTag>();
      StreamTypes = new HashSet<StreamType>();
      TagEventData = new HashSet<TagEventDatum>();
    }

    public Guid DataTypeId { get; set; }
    public string DataTypeName { get; set; } = null!;

    public virtual ICollection<MasterStreamType> MasterStreamTypes { get; set; }
    public virtual ICollection<OpcTag> OpcTags { get; set; }
    public virtual ICollection<StreamType> StreamTypes { get; set; }
    public virtual ICollection<TagEventDatum> TagEventData { get; set; }
  }
}

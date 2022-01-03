namespace StreamAnalytics.System.Data.Entities
{
  public partial class EventType
  {
    public EventType()
    {
      OpcTagEvents = new HashSet<OpcTagEvent>();
      OpcTags = new HashSet<OpcTag>();
    }

    public Guid EventTypeId { get; set; }
    public string EventTypeName { get; set; } = null!;

    public virtual ICollection<OpcTagEvent> OpcTagEvents { get; set; }
    public virtual ICollection<OpcTag> OpcTags { get; set; }
  }
}

namespace StreamAnalytics.System.Data.Entities
{
  public partial class MeasurementUnit
  {
    public MeasurementUnit()
    {
      OpcTagStreams = new HashSet<OpcTagStream>();
    }

    public Guid UnitId { get; set; }
    public string UnitGroup { get; set; } = null!;
    public string UnitAbbreviation { get; set; } = null!;
    public string MeasurementSystem { get; set; } = null!;

    public virtual ICollection<OpcTagStream> OpcTagStreams { get; set; }
  }
}

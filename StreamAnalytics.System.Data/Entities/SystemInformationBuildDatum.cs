namespace StreamAnalytics.System.Data.Entities
{
  public partial class SystemInformationBuildDatum
  {
    public byte SystemInformationBuildDataId { get; set; }
    public string BuildNumber { get; set; } = null!;
    public DateTimeOffset AppliedDate { get; set; }
  }
}

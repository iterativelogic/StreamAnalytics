namespace StreamAnalytics.Core.Models
{
  public class EventData
  {
    public DateTimeOffset Timestamp { get; set; }
    public string TagName { get; set; }
    public string AssetName { get; set; }
    public string StreamType { get; set; }
    public bool State { get; set; }
    public string Source { get; set; }
    public string Severity { get; set; }
  }
}

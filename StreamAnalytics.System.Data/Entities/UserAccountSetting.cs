namespace StreamAnalytics.System.Data.Entities
{
  public partial class UserAccountSetting
  {
    public Guid TenantId { get; set; }
    public Guid AccountId { get; set; }
    public string? DisplayTheme { get; set; }
    public string? Timezone { get; set; }
    public string? NumberFormat { get; set; }
    public string? TimeFormat { get; set; }
    public string? DateFormat { get; set; }
  }
}

namespace StreamAnalytics.System.Data.Entities
{
  public partial class DeletedResourceTask
  {
    public Guid TenantId { get; set; }
    public Guid DeletedId { get; set; }
    public string IdType { get; set; } = null!;
    public string IndexingTask { get; set; } = null!;
    public DateTimeOffset SubmittedTimestamp { get; set; }
  }
}

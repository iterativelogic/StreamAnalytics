namespace StreamAnalytics.System.Data.Entities
{
  public partial class Attribute
  {
    public Attribute()
    {
      AttributeValues = new HashSet<AttributeValue>();
    }

    public Guid TenantId { get; set; }
    public Guid AttributeId { get; set; }
    public string? AttributeName { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<AttributeValue> AttributeValues { get; set; }
  }
}

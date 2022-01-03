namespace StreamAnalytics.System.Data.Entities
{
  public partial class AttributeValue
  {
    public AttributeValue()
    {
      PhysicalAssetAttributes = new HashSet<PhysicalAssetAttribute>();
    }

    public Guid TenantId { get; set; }
    public Guid AttributeValueId { get; set; }
    public Guid AttributeId { get; set; }
    public string? AttributeValue1 { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<PhysicalAssetAttribute> PhysicalAssetAttributes { get; set; }
  }
}

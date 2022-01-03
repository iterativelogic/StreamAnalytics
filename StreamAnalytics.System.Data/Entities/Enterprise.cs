namespace StreamAnalytics.System.Data.Entities
{
  public partial class Enterprise
  {
    public Enterprise()
    {
      Tenants = new HashSet<Tenant>();
    }

    public Guid EnterpriseId { get; set; }
    public string EnterpriseName { get; set; } = null!;
    public bool? Active { get; set; }

    public virtual ICollection<Tenant> Tenants { get; set; }
  }
}

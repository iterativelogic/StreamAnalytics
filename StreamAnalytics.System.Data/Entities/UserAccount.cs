namespace StreamAnalytics.System.Data.Entities
{
  public partial class UserAccount
  {
    public UserAccount()
    {
      Roles = new HashSet<Role>();
    }

    public Guid TenantId { get; set; }
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public bool AdminUser { get; set; }
    public bool? Active { get; set; }
    public string Email { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; }
  }
}

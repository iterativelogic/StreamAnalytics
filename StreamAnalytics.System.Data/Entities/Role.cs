namespace StreamAnalytics.System.Data.Entities
{
  public partial class Role
  {
    public Role()
    {
      Permissions = new HashSet<Permission>();
      UserAccounts = new HashSet<UserAccount>();
    }

    public Guid RoleId { get; set; }
    public string RoleDisplayName { get; set; } = null!;
    public bool? Active { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; }
    public virtual ICollection<UserAccount> UserAccounts { get; set; }
  }
}

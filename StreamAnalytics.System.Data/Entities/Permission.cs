namespace StreamAnalytics.System.Data.Entities
{
  public partial class Permission
  {
    public Permission()
    {
      Roles = new HashSet<Role>();
    }

    public Guid PermissionId { get; set; }
    public string PermissionCode { get; set; } = null!;
    public string PermissionDisplayName { get; set; } = null!;
    public bool? Active { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
  }
}

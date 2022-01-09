using System.Text.Json.Serialization;
using StreamAnalytics.System.Serialization;

namespace StreamAnalytics.System.Models.Tenant
{
  [JsonConverter(typeof(TenantIdJsonConverter))]
  public struct TenantId : IEquatable<TenantId>
  {
    public TenantId(Guid tenantId)
    {
      Value = tenantId;
    }

    public TenantId(string tenantId)
    {
      Value = Guid.Parse(tenantId);
    }

    public Guid Value { get; init; }

    public override bool Equals(object obj)
    {
      return obj is TenantId id && Equals(id);
    }

    public bool Equals(TenantId other)
    {
      return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Value);
    }

    public override string ToString()
    {
      return Value.ToString();
    }

    public static bool operator ==(TenantId left, TenantId right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(TenantId left, TenantId right)
    {
      return !(left == right);
    }

    public static implicit operator TenantId(string tenantId) => new TenantId(tenantId);

    public static implicit operator TenantId(Guid tenantId) => new TenantId(tenantId);
  }
}

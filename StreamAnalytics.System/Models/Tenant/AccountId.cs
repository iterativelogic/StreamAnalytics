namespace StreamAnalytics.System.Models.Tenant
{
  public struct AccountId : IEquatable<AccountId>
  {
    public AccountId(Guid accountId)
    {
      Value = accountId;
    }

    public AccountId(string accountId)
    {
      Value = Guid.Parse(accountId);
    }

    public Guid Value { get; init; }

    public override bool Equals(object? obj)
    {
      return obj is AccountId id && Equals(id);
    }

    public bool Equals(AccountId other)
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

    public static bool operator ==(AccountId left, AccountId right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(AccountId left, AccountId right)
    {
      return !(left == right);
    }

    public static implicit operator AccountId(string accountId) => new AccountId(accountId);

    public static implicit operator AccountId(Guid accountId) => new AccountId(accountId);
  }
}

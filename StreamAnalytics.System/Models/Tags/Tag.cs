using StreamAnalytics.System.Constants;
using StreamAnalytics.System.Models.Tags.Values;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Models.Tags
{
  public abstract class Tag
  {
    public TenantId TenantId { get; set; }
    public Guid TagId { get; set; }
    public Guid GatewayId { get; set; }
    public Guid? PhysicalAssetId { get; set; }
    public string TagName { get; set; }
    public string TagAlias { get; set; }
    public Guid? SeverityId { get; set; }
    public bool Quality { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public DateTimeOffset LastGoodTimestamp { get; set; }
    public Guid SourceId { get; set; }

    public abstract TagValue Value { get; set; }
    public abstract TagValue LastGoodValue { get; set; }
    public abstract Guid DataTypeId { get; }
    public abstract Guid StreamType { get; set; }

    public static Tag Create(Guid dataTypeId)
    {
      if (dataTypeId == DataTypes.Boolean)
        return new BooleanTag();
      else
        return new NumericalTag();
    }

    public static Guid GetDefaultStreamType(Guid dataTypeId)
    {
      return RunDataTypeBasedFunction(
        dataTypeId,
        () => BooleanStreamType.Other,
        () => NumericalStreamTypes.Continuous,
        () => StringStreamType.Other);
    }

    public static TOutput RunDataTypeBasedFunction<TOutput>(Guid dataTypeId,
                                                            Func<TOutput> booleanAction,
                                                            Func<TOutput> numericalAction,
                                                            Func<TOutput> stringAction)
    {
      if (dataTypeId == DataTypes.Boolean) {
        return booleanAction();
      }
      else if (dataTypeId == DataTypes.Numerical) {
        return numericalAction();
      }
      else if (dataTypeId == DataTypes.String) {
        return stringAction();
      }

      return default;
    }
  }
}

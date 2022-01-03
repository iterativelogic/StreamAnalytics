using StreamAnalytics.System.Models.Tags.Values;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.System.Models.Tags
{
  public class TagBuilder
  {
    private readonly Tag _tag;

    public TagBuilder(Guid dataTypeId)
    {
      _tag = Tag.Create(dataTypeId);
    }

    public TenantId TenantId { set => _tag.TenantId = value; }
    public Guid TagId { set => _tag.TagId = value; }
    public Guid GatewayId { set => _tag.GatewayId = value; }
    public Guid PhysicalAssetId { set => _tag.PhysicalAssetId = value; }
    public string TagName { set => _tag.TagName = value; }
    public string TagAlias { set => _tag.TagAlias = value; }
    public Guid? SeverityId { set => _tag.SeverityId = value; }
    public bool Quality { set => _tag.Quality = value; }
    public DateTimeOffset Timestamp { set => _tag.Timestamp = value; }
    public DateTimeOffset LastGoodTimestamp { set => _tag.LastGoodTimestamp = value; }
    public TagValue Value {
      set => _tag.Value = value;
    }
    public TagValue LastGoodValue { set => _tag.LastGoodValue = value; }
    public Guid? StreamTypeId { set => _tag.StreamTypeId = value; }

    public Tag Build() => _tag;
  }
}

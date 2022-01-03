using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Models.Tags;

namespace StreamAnalytics.System.Data.Extensions.Mapping
{
  public static class OpcTagExtensions
  {
    public static Tag Map(this OpcTag tag)
    {
      return new TagBuilder(tag.DataTypeId.Value) {
        TenantId = tag.TenantId,
        TagId = tag.TagId,
        GatewayId = tag.IoTGatewayId,
        PhysicalAssetId = tag.PhysicalAssetId.Value,
        TagName = tag.TagName,
        TagAlias = tag.TagAlias,
        SeverityId = tag.SeverityId,
        Quality = Convert.ToBoolean(tag.Quality),
        Timestamp = tag.Timestamp.GetValueOrDefault(),
        LastGoodTimestamp = tag.LastGoodTimestamp.GetValueOrDefault(),
        Value = tag.Value,
        LastGoodValue = tag.LastGoodValue,
        StreamTypeId = tag.StreamTypeId
      }.Build();
    }
  }
}

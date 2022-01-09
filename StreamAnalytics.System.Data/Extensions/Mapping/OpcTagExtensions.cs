using StreamAnalytics.System.Constants;
using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Models.Tags;
using StreamAnalytics.System.Models.Tags.Values;

namespace StreamAnalytics.System.Data.Extensions.Mapping
{
  public static class OpcTagExtensions
  {
    public static Tag Map(this OpcTag tag)
    {
      TagValue tagValue = tag.Value;
      return new TagBuilder(tagValue.DataTypeId) {
        TenantId = tag.TenantId,
        TagId = tag.TagId,
        GatewayId = tag.IoTGatewayId,
        PhysicalAssetId = tag.PhysicalAssetId ?? null,
        TagName = tag.TagName,
        TagAlias = tag.TagAlias,
        SeverityId = tag.SeverityId,
        Quality = Convert.ToBoolean(tag.Quality),
        Timestamp = tag.Timestamp.GetValueOrDefault(),
        LastGoodTimestamp = tag.LastGoodTimestamp.GetValueOrDefault(),
        Value = tagValue,
        LastGoodValue = tag.LastGoodValue,
        SourceId = tag.SourceId,
        StreamType = Tag.RunDataTypeBasedFunction(tagValue.DataTypeId,
          () => tag.EventTypeId.GetValueOrDefault(),
          () => tag.StreamTypeId.GetValueOrDefault(),
          () => tag.StreamTypeId.GetValueOrDefault())
      }.Build();
    }

    public static OpcTag ToEntity(this Tag tag)
    {
      return new OpcTag {
        TenantId = tag.TenantId.Value,
        TagId = tag.TagId,
        IoTGatewayId = tag.GatewayId,
        PhysicalAssetId = tag.PhysicalAssetId,
        TagName = tag.TagName,
        TagAlias = tag.TagAlias,
        DataTypeId = tag.DataTypeId,
        SeverityId = tag.SeverityId,
        Quality = tag.Quality.ToString(),
        Timestamp = tag.Timestamp,
        LastGoodTimestamp = tag.LastGoodTimestamp,
        Value = tag.Value.StringValue,
        LastGoodValue = tag.LastGoodValue.StringValue,
        StreamTypeId = tag.DataTypeId == DataTypes.Numerical || tag.DataTypeId == DataTypes.String ? tag.StreamType : null,
        EventTypeId = tag.DataTypeId == DataTypes.Boolean ? tag.StreamType : null,
        SourceId = tag.SourceId
      };
    }
  }
}

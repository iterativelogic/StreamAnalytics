using StreamAnalytics.System.Models.Tags.Values;

namespace StreamAnalytics.System.Models.Tags
{
  public class BooleanTag : Tag
  {
    override public TagValue Value { get; set; }
    public override TagValue LastGoodValue { get; set; }
    public override Guid DataTypeId => Value.DataTypeId;
    public override Guid StreamType { get; set; }
  }
}

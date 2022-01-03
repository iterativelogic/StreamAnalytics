using StreamAnalytics.System.Models.Assets;
using StreamAnalytics.System.Models.Tags;

namespace StreamAnalytics.Ingest.System.Dataflow.Objects
{
  public class AssetTagsTransform
  {
    public IngestDataTransform IngestData { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
    public IEnumerable<Asset> Assets { get; set; }
  }
}

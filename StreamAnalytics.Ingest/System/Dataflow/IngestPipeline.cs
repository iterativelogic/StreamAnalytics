using System.Threading.Tasks.Dataflow;
using StreamAnalytics.Ingest.System.Dataflow.Objects;
using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Data.Repositories;
using StreamAnalytics.System.Models;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Ingest.System.Dataflow
{
  public class IngestPipeline
  {
    private readonly ILogger<IngestPipeline> logger;

    public IngestPipeline(ILogger<IngestPipeline> logger)
    {
      BufferBlock = new BufferBlock<OpcIngestRequest>();
      IngestDataTransformBlock = new TransformBlock<OpcIngestRequest, IngestDataTransform>(request => IngestDataTransformFn(request));
      AssetTagsTransformBlock = new TransformBlock<IngestDataTransform, AssetTagsTransform>(ingestData => AssetTagsTransformBlockFn(ingestData));
      LoggerBlock = new ActionBlock<AssetTagsTransform>(t => 
      {
        TList.Add(t);
      });

      BufferBlock.LinkTo(IngestDataTransformBlock);
      IngestDataTransformBlock.LinkTo(AssetTagsTransformBlock);
      AssetTagsTransformBlock.LinkTo(LoggerBlock);
      this.logger = logger;
      logger.LogInformation("CONSTRUCTOR CALLED");
    }
    public BufferBlock<OpcIngestRequest> BufferBlock { get; }
    public TransformBlock<OpcIngestRequest, IngestDataTransform> IngestDataTransformBlock { get; }
    public TransformBlock<IngestDataTransform, AssetTagsTransform> AssetTagsTransformBlock { get; }
    public ActionBlock<AssetTagsTransform> LoggerBlock { get; set; }
    public List<AssetTagsTransform> TList { get; set; } = new List<AssetTagsTransform>();

    private IngestDataTransform IngestDataTransformFn(OpcIngestRequest request) => request.Payload.Data
        .GroupBy(v => v.TagName)
        .Select(v => v.OrderByDescending(data => data.Timestamp).First())
        .Aggregate(new IngestDataTransform {
          TenantId = request.TenantId,
          AccountId = request.AccountId,
          GatewayId = request.GatewayId
        }, (transform, data) => {
          transform.DataList.Add(data);
          return transform;
        });

    private async Task<AssetTagsTransform> AssetTagsTransformBlockFn(IngestDataTransform ingestData)
    {
      try {
        logger.LogInformation($"Begin {nameof(this.AssetTagsTransformBlockFn)}");
        using var dbContext = new StreamAnalyticsDbContext();
        var tagRepository = new TagRepository(dbContext);
        var tags = await tagRepository.GetTagsAsync(ingestData.TenantId, ingestData.GatewayId, ingestData.DataList.Select(data => data.TagName).ToArray());

        var assetRepository = new AssetRepository(dbContext);
        var assets = await assetRepository.GetAssetsAsync(ingestData.TenantId, tags.Select(tag => tag.PhysicalAssetId).ToArray());

        return new AssetTagsTransform {
          IngestData = ingestData,
          Assets = assets,
          Tags = tags
        };
      }
      catch (Exception ex) {
        logger.LogError(ex, ex.Message);
        throw;
      }
    }
  }
}

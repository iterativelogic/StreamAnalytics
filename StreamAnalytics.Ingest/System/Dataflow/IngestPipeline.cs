using System.Security.Cryptography.Xml;
using System.Threading.Tasks.Dataflow;
using StreamAnalytics.Ingest.System.Dataflow.Objects;
using StreamAnalytics.Ingest.System.Producer;
using StreamAnalytics.System.Constants;
using StreamAnalytics.System.Data.Entities;
using StreamAnalytics.System.Data.Repositories;
using StreamAnalytics.System.Models;
using StreamAnalytics.System.Models.Assets;
using StreamAnalytics.System.Models.Tags;
using StreamAnalytics.System.Models.Tags.Values;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Ingest.System.Dataflow
{
  public class IngestPipeline
  {
    private readonly OpcIngestSinkDataProducer producer;
    private readonly ILogger<IngestPipeline> logger;

    public IngestPipeline(OpcIngestSinkDataProducer producer, ILogger<IngestPipeline> logger)
    {
      BufferBlock = new BufferBlock<OpcIngestRequest>();
      IngestDataTransformBlock = new TransformBlock<OpcIngestRequest, IngestDataTransform>(request => IngestDataTransformFn(request));
      AssetTagsTransformBlock = new TransformBlock<IngestDataTransform, AssetTagsTransform>(ingestData => AssetTagsTransformBlockFn(ingestData));
      BroadcastAssetTagsTransformBlock = new BroadcastBlock<AssetTagsTransform>(transform => transform);
      SynchronizeDatabaseBlock = new ActionBlock<AssetTagsTransform>(transform => SynchronizeDatabaseBlockFn(transform));
      SendToKafkaTransformManyBlock = new TransformManyBlock<AssetTagsTransform, PayloadMapTransform>(transform => SendToKafkaTransformManyBlockFn(transform));
      BroadcastPayloadMapTransformBlock = new BroadcastBlock<PayloadMapTransform>(transform => transform);
      SendToIngestDataTopicBlock = new ActionBlock<PayloadMapTransform>(transform => SendToIngestDataTopicBlockFn(transform));
      SendToIngestNumericalDataTopicBlock = new ActionBlock<PayloadMapTransform>(transform => SendToIngestNumericalDataTopicBlockFn(transform));
      SendToThresholdValueTopicBlock = new ActionBlock<PayloadMapTransform>(transform => SendToThresholdValueTopicBlockFn(transform));
      SendToEventDataDataTopicBlock = new ActionBlock<PayloadMapTransform>(transform => SendToEventDataDataTopicBlockFn(transform));

      BufferBlock.LinkTo(IngestDataTransformBlock);
      IngestDataTransformBlock.LinkTo(AssetTagsTransformBlock);
      AssetTagsTransformBlock.LinkTo(BroadcastAssetTagsTransformBlock);

      BroadcastAssetTagsTransformBlock.LinkTo(SynchronizeDatabaseBlock);
      BroadcastAssetTagsTransformBlock.LinkTo(SendToKafkaTransformManyBlock);

      SendToKafkaTransformManyBlock.LinkTo(BroadcastPayloadMapTransformBlock);

      BroadcastPayloadMapTransformBlock.LinkTo(SendToIngestDataTopicBlock);
      BroadcastPayloadMapTransformBlock.LinkTo(SendToIngestNumericalDataTopicBlock);
      BroadcastPayloadMapTransformBlock.LinkTo(SendToThresholdValueTopicBlock);
      BroadcastPayloadMapTransformBlock.LinkTo(SendToEventDataDataTopicBlock);

      this.producer = producer;
      this.logger = logger;

      logger.LogInformation("CONSTRUCTOR CALLED");
    }

    public BufferBlock<OpcIngestRequest> BufferBlock { get; }
    public TransformBlock<OpcIngestRequest, IngestDataTransform> IngestDataTransformBlock { get; }
    public TransformBlock<IngestDataTransform, AssetTagsTransform> AssetTagsTransformBlock { get; }
    public BroadcastBlock<AssetTagsTransform> BroadcastAssetTagsTransformBlock { get; }
    public ActionBlock<AssetTagsTransform> SynchronizeDatabaseBlock { get; }
    public TransformManyBlock<AssetTagsTransform, PayloadMapTransform> SendToKafkaTransformManyBlock { get; }
    public BroadcastBlock<PayloadMapTransform> BroadcastPayloadMapTransformBlock { get; }
    public ActionBlock<PayloadMapTransform> SendToIngestDataTopicBlock { get; }
    public ActionBlock<PayloadMapTransform> SendToIngestNumericalDataTopicBlock { get; }
    public ActionBlock<PayloadMapTransform> SendToThresholdValueTopicBlock { get; }
    public ActionBlock<PayloadMapTransform> SendToEventDataDataTopicBlock { get; }

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

        var existingTags = await tagRepository.GetTagsAsync(
          ingestData.TenantId,
          ingestData.GatewayId,
          ingestData.DataList
            .Select(data => data.TagName)
            .ToArray());

        foreach (var (tag, data) in existingTags.Join(ingestData.DataList,
            tag => tag.TagName,
            data => data.TagName,
            (tag, data) => (tag, data))) {
          tag.Value = data.Value;
          tag.Timestamp = DateTimeOffset.UtcNow;
          tag.LastGoodValue = data.Value;
          tag.LastGoodTimestamp = DateTimeOffset.UtcNow;
        }

        var newTags = ingestData.DataList
         .Where(data => !existingTags.Any(tag => tag.TenantId == ingestData.TenantId
            && string.Equals(data.TagName, tag.TagName, StringComparison.OrdinalIgnoreCase)))
         .Select(data => {
           TagValue value = data.Value;
           return new TagBuilder(value.DataTypeId) {
             TenantId = ingestData.TenantId,
             TagId = Guid.NewGuid(),
             GatewayId = ingestData.GatewayId,
             TagName = data.TagName,
             TagAlias = data.TagName,
             Quality = data.Quality,
             StreamType = Tag.GetDefaultStreamType(value.DataTypeId),
             Value = value,
             LastGoodValue = value,
             Timestamp = DateTimeOffset.UtcNow,
             LastGoodTimestamp = DateTimeOffset.UtcNow,
             SourceId = ingestData.GatewayId
           }.Build();
         }).ToList();

        var combinedTags = existingTags.Concat(newTags);

        return new AssetTagsTransform {
          IngestData = ingestData,
          Tags = combinedTags,
          ExistingTags = existingTags
        };
      }
      catch (Exception ex) {
        logger.LogError(ex, ex.Message);
        throw;
      }
    }

    private async Task SynchronizeDatabaseBlockFn(AssetTagsTransform transform)
    {
      logger.LogInformation($"Begin {nameof(SynchronizeDatabaseBlockFn)}");

      using var dbContext = new StreamAnalyticsDbContext();
      var tagRepository = new TagRepository(dbContext);

      await tagRepository.UpdateTagsAsync(transform.Tags, transform.ExistingTags);

      logger.LogInformation($"End {nameof(SynchronizeDatabaseBlockFn)}");
    }

    private IEnumerable<PayloadMapTransform> SendToKafkaTransformManyBlockFn(AssetTagsTransform transform)
    {
      logger.LogInformation($"Begin {nameof(SendToKafkaTransformManyBlockFn)}");

      var tagMap = transform.Tags.ToDictionary(tag => tag.TagName, tag => tag);

      var maps = transform.IngestData.DataList
        .Select(data => new PayloadMapTransform {
          TenantId = transform.IngestData.TenantId,
          AccountId = transform.IngestData.AccountId,
          GatewayId = transform.IngestData.GatewayId,
          Payload = data,
          Tag = tagMap[data.TagName]
        }).ToList();

      logger.LogInformation($"End {nameof(SendToKafkaTransformManyBlockFn)}");

      return maps;
    }

    private async Task SendToIngestDataTopicBlockFn(PayloadMapTransform transform)
    {
      await producer.ProduceAsync(transform.ConvertToEvent(), KafkaTopics.CM_OPC_INGEST_DATA);
      await producer.ProduceAsync(transform.ConvertToEvent(), $"{KafkaTopics.CM_OPC_INGEST_DATA}-{transform.TenantId.ToString().ToLower()}");
    }

    private async Task SendToIngestNumericalDataTopicBlockFn(PayloadMapTransform transform)
    {
      if (transform.Tag.DataTypeId == DataTypes.Numerical && transform.Tag.StreamType != NumericalStreamTypes.Threshold)
        await producer.ProduceAsync(transform.ConvertToEvent(), KafkaTopics.CM_OPC_NUMERICAL_DATA);
    }

    private async Task SendToThresholdValueTopicBlockFn(PayloadMapTransform transform)
    {
      if (transform.Tag.DataTypeId == DataTypes.Numerical && transform.Tag.StreamType == NumericalStreamTypes.Threshold)
        await producer.ProduceAsync(transform.ConvertToEvent(), KafkaTopics.CM_OPC_THRESHOLD_VALUE);
    }

    private async Task SendToEventDataDataTopicBlockFn(PayloadMapTransform transform)
    {
      if (transform.Tag.DataTypeId == DataTypes.Boolean)
        await producer.ProduceAsync(transform.ConvertToEvent(), KafkaTopics.CM_EVENT_DATA);
    }
  }
}

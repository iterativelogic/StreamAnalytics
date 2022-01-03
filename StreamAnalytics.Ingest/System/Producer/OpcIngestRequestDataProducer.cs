﻿using System.Text.Json;
using Confluent.Kafka;
using StreamAnalytics.System.Constants;
using StreamAnalytics.System.Models;

namespace StreamAnalytics.Ingest.System.Producer
{
  public class OpcIngestRequestDataProducer : IDisposable
  {
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<OpcIngestRequestDataProducer> _logger;
    private bool disposedValue;

    public OpcIngestRequestDataProducer(ILogger<OpcIngestRequestDataProducer> logger)
    {
      var builder = new ProducerBuilder<string, string>(new ProducerConfig {
        BootstrapServers = "localhost:9093"
      });

      _producer = builder.Build();
      _logger = logger;
    }

    public async Task ProduceAsync(OpcIngestRequest request)
    {
      var deliveryResult = await _producer.ProduceAsync(KafkaTopics.CM_OPC_INGEST_REQUEST, CreateMessage(request));
      _logger.LogInformation($"Delivered to partition:{deliveryResult.TopicPartition} offset:{deliveryResult.Offset} for topic {KafkaTopics.CM_OPC_INGEST_REQUEST}");
    }

    private static Message<string, string> CreateMessage(OpcIngestRequest request)
    {
      var key = $"{request.TenantId}{request.GatewayId}";
      var value = JsonSerializer.Serialize(request);

      return new Message<string, string> {
        Key = key,
        Value = value
      };
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue) {
        if (disposing) {
          _producer.Dispose();
        }

        disposedValue = true;
      }
    }

    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}

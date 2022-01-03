using Microsoft.AspNetCore.Mvc;
using StreamAnalytics.Ingest.System.Producer;
using StreamAnalytics.System.Models;

namespace StreamAnalytics.Ingest.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class IngestController : ControllerBase
  {
    private readonly OpcIngestRequestDataProducer _producer;
    private readonly ILogger<IngestController> _logger;

    public IngestController(OpcIngestRequestDataProducer producer, ILogger<IngestController> logger)
    {
      _producer = producer;
      _logger = logger;
    }

    [HttpPost("{id}/opc-data")]
    public async Task<IActionResult> Post(Guid id, [FromBody] OpcIngestPayload payload)
    {
      _logger.LogInformation("Received INGEST REQUEST");

      var request = new OpcIngestRequest {
        TenantId = HttpContext.Request.Headers["X-Plex-Impersonation-TenantId"].ToString(),
        AccountId = HttpContext.Request.Headers["X-Plex-Impersonation-AccountId"].ToString(),
        GatewayId = id,
        Payload = payload
      };

      await _producer.ProduceAsync(request);

      return Accepted();
    }

  }
}
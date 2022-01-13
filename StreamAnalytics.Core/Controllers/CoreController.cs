using Microsoft.AspNetCore.Mvc;

namespace StreamAnalytics.Core.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CoreController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<CoreController> _logger;

    public CoreController(ILogger<CoreController> logger)
    {
      _logger = logger;
    }

    [HttpGet("ping")]
    public string Ping() => "pong";
  }
}
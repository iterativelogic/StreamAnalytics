using Microsoft.AspNetCore.Mvc;
using StreamAnalytics.Core.Services;
using StreamAnalytics.System.Models.Tenant;

namespace StreamAnalytics.Core.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CoreController : ControllerBase
  {
    private readonly DruidDataService dataService;
    private readonly ILogger<CoreController> _logger;

    public CoreController(DruidDataService dataService, ILogger<CoreController> logger)
    {
      this.dataService = dataService;
      _logger = logger;
    }

    [HttpGet("ping")]
    public string Ping() => "pong";

    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs()
    {
      var info = new TenantInfo(Guid.NewGuid(), Guid.NewGuid());
      var logs = await dataService.GetLogs(info, HttpContext.RequestAborted);
      return Ok(logs);
    }
  }
}
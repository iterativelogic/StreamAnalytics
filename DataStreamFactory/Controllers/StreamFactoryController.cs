using DataStreamFactory.System;
using Microsoft.AspNetCore.Mvc;

namespace DataStreamFactory.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class StreamFactoryController : ControllerBase
  {
    private readonly StreamFactory streamFactory;

    public StreamFactoryController(StreamFactory streamFactory)
    {
      this.streamFactory = streamFactory;
    }

    [HttpGet("generate")]
    public IActionResult Generate()
    {
      return Ok(streamFactory.CreateStreamJson("Test.Tag.Booleam.Stream"));
    }
  }
}
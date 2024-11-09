using Microsoft.AspNetCore.Mvc;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController: ControllerBase
{
    readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogCritical($"Testowy log: {nameof(TestController)}");

        return Ok();
    }
}

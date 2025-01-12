using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
public class TestController: ControllerBase
{
    readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

#if DEBUG

    [HttpGet(WebAPIEndpointsProvider.GetTestPath)]
    //[Authorize]
    public IActionResult Get()
    {
        var userId = User.FindFirst("id")?.Value;
        _logger.LogCritical($"Testowy log: {userId}");
        //return Ok("123");
        return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(MoriaApiExceptionReason.DefaultExceptionCheckStatusCode, MoriaApiException.ApiExceptionThrownStatusCode));
        //return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, JsonSerializer.Serialize(new MoriaApiException(MoriaApiExceptionReason.DefaultExceptionCheckStatusCode, MoriaApiException.ApiExceptionThrownStatusCode)));
    }

#endif
}

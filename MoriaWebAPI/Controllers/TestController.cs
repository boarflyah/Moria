﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public IActionResult Get()
    {
        var userId = User.FindFirst("id")?.Value;
        _logger.LogCritical($"Testowy log: {userId}");

        return Ok();
    }

#endif
}

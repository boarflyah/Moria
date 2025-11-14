using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaWebAPIServices.Services.Subiekt.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class UpdateController : ControllerBase
{
    readonly ISalesOrderService _salesOrderService;
    readonly ILogger<UpdateController> _logger;

    public UpdateController(ISalesOrderService salesOrderService, ILogger<UpdateController> logger)
    {
        _salesOrderService = salesOrderService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> UpdateEntities()
    {
        try
        {
            var updatedCount = _salesOrderService.UpdateEntities();
            return Ok(updatedCount);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, $"Method: {nameof(UpdateEntities)}");
            return StatusCode(501, ex.Message);
        }
    }
}


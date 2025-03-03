using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class ComponentController: ControllerBase
{
    readonly ILogger<ComponentController> _logger;
    readonly IComponentControllerService _controllerService;

    public ComponentController(ILogger<ComponentController> logger, IComponentControllerService controllerService)
    {
        _logger = logger;
        _controllerService = controllerService;
    }

    [HttpGet($"{WebAPIEndpointsProvider.GetComponentPath}/{{id}}")]
    [Produces<ComponentDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var component = await _controllerService.GetComponent(id);

            return Ok(component);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Get)}");
            return StatusCode(501, ex.Message);
        }
    }
}

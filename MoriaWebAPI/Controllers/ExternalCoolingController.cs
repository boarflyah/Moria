using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class ExternalCoolingController : ControllerBase
{
    readonly IExternalCoolingControllerService _externalCoolingController;
    readonly ILogger<ExternalCoolingController> _logger;

    public ExternalCoolingController(IExternalCoolingControllerService externalCoolingControllerService, ILogger<ExternalCoolingController> logger)
    {
        _externalCoolingController = externalCoolingControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetExternalCoolingsPath)]
    [Produces<IEnumerable<ExternalCoolingDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var externalCoolings = await _externalCoolingController.GetAllExternalCoolings();

            return Ok(externalCoolings);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetExternalCoolingPath}/{{id}}")]
    [Produces<ExternalCoolingDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var externalCooling = await _externalCoolingController.GetExternalCoolingById(id);

            return Ok(externalCooling);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostExternalCoolingPath}")]
    [Produces<ExternalCoolingDo>]
    public async Task<IActionResult> Post(ExternalCoolingDo externalCoolingDo)
    {
        try
        {
            var externalCooling = await _externalCoolingController.CreateExternalCooling(externalCoolingDo );

            return Ok(externalCooling);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Post)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpPut($"{WebAPIEndpointsProvider.PutExternalCoolingPath}")]
    [Produces<ExternalCoolingDo>]
    public async Task<IActionResult> Put(ExternalCoolingDo externalCoolingDo)
    {
        try
        {
            var externalCooling = await _externalCoolingController.EditExternalCooling(externalCoolingDo);

            return Ok(externalCooling);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Put)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteExternalCoolingPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _externalCoolingController.DeleteExternalCooling(id);

            return Ok(isDeleted);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Delete)}");
            return StatusCode(501, ex.Message);
        }
    }
}

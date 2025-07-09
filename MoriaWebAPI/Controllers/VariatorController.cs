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
public class VariatorController : ControllerBase
{
    readonly IVariatorControllerService _variatorController;
    readonly ILogger<VariatorController> _logger;

    public VariatorController(IVariatorControllerService variatorControllerService, ILogger<VariatorController> logger)
    {
        _variatorController = variatorControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetVariatorsPath)]
    [Produces<IEnumerable<VariatorDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var variators = await _variatorController.GetAllVariators();

            return Ok(variators);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetVariatorPath}/{{id}}")]
    [Produces<VariatorDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var variator = await _variatorController.GetVariatorsById(id);

            return Ok(variator);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostVariatorPath}")]
    [Produces<VariatorDo>]
    public async Task<IActionResult> Post(VariatorDo variatorDo)
    {
        try
        {
            var variator = await _variatorController.CreateVariator(variatorDo);

            return Ok(variator);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutVariatorPath}")]
    [Produces<VariatorDo>]
    public async Task<IActionResult> Put(VariatorDo variatorDo)
    {
        try
        {
            var variator = await _variatorController.EditVariator(variatorDo);

            return Ok(variator);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteVariatorPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _variatorController.DeleteVariator(id);

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

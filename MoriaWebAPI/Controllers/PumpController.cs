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
public class PumpController : ControllerBase
{
    readonly IPumpControllerService _pumpController;
    readonly ILogger<PumpController> _logger;

    public PumpController(IPumpControllerService pumpControllerService, ILogger<PumpController> logger)
    {
        _pumpController = pumpControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetPumpsPath)]
    [Produces<IEnumerable<PumpDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var pumps = await _pumpController.GetAllPumps();

            return Ok(pumps);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetPumpPath}/{{id}}")]
    [Produces<PumpDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var pump = await _pumpController.GetPumpById(id);

            return Ok(pump);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostPumpPath}")]
    [Produces<PumpDo>]
    public async Task<IActionResult> Post(PumpDo pumpDo)
    {
        try
        {
            var pump = await _pumpController.CreatePump(pumpDo);

            return Ok(pump);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutPumpPath}")]
    [Produces<PumpDo>]
    public async Task<IActionResult> Put(PumpDo pumpDo)
    {
        try
        {
            var pumpResult = await _pumpController.EditPump(pumpDo);

            return Ok(pumpResult);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeletePumpPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _pumpController.DeletePump(id);

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

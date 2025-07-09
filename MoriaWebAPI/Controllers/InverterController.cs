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
public class InverterController : ControllerBase
{
    readonly IInverterControllerService _inverterController;
    readonly ILogger<InverterController> _logger;

    public InverterController(IInverterControllerService inverterControllerService, ILogger<InverterController> logger)
    {
        _inverterController = inverterControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetInvertersPath)]
    [Produces<IEnumerable<InverterDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var inverters = await _inverterController.GetAllInverters();

            return Ok(inverters);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetInverterPath}/{{id}}")]
    [Produces<InverterDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var inverter = await _inverterController.GetInverterById(id);

            return Ok(inverter);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostInverterPath}")]
    [Produces<InverterDo>]
    public async Task<IActionResult> Post(InverterDo inverterDo)
    {
        try
        {
            var inverter = await _inverterController.CreateInverter(inverterDo);

            return Ok(inverter);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutInverterPath}")]
    [Produces<InverterDo>]
    public async Task<IActionResult> Put(InverterDo inverter)
    {
        try
        {
            var inverterResult = await _inverterController.EditInverter(inverter);

            return Ok(inverterResult);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteInverterPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _inverterController.DeleteInverter(id);

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

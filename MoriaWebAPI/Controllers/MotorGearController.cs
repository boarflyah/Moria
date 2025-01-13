using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class MotorGearController : ControllerBase
{
    readonly IMotorGearControllerService _motorGearController;
    readonly ILogger<MotorGearController> _logger;

    public MotorGearController(IMotorGearControllerService motorGearControllerService, ILogger<MotorGearController> logger)
    {
        _motorGearController = motorGearControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetMotorGearsPath)]
    [Produces<IEnumerable<MotorGearDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var motorGears = await _motorGearController.GetAllMotorGears();

            return Ok(motorGears);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetMotorGearPath}/{{id}}")]
    [Produces<MotorGearDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var motorGear = await _motorGearController.GetMotorGearById(id);

            return Ok(motorGear);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostMotorGearPath}")]
    [Produces<MotorGearDo>]
    public async Task<IActionResult> Post(MotorGearDo motorGearDo)
    {
        try
        {
            var motorGear = await _motorGearController.CreateMotorGear(motorGearDo);

            return Ok(motorGear);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutMotorGearPath}")]
    [Produces<MotorGearDo>]
    public async Task<IActionResult> Put(MotorGearDo motorGearDo)
    {
        try
        {
            var motorGear = await _motorGearController.EditMotorGear(motorGearDo);

            return Ok(motorGear);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteMotorGearPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _motorGearController.DeleteMotorGear(id);

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

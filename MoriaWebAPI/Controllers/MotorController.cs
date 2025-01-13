using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPI.Controllers;
[ApiController]
[Route("")]
[Authorize]
public class MotorController : ControllerBase
{
    readonly IMotorControllerService _motorController;
    readonly ILogger<MotorController> _logger;

    public MotorController(IMotorControllerService motorControllerService, ILogger<MotorController> logger)
    {
        _motorController = motorControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetMotorsPath)]
    [Produces<IEnumerable<MotorDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var motors = await _motorController.GetAllMotors();

            return Ok(motors);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetMotorPath}/{{id}}")]
    [Produces<MotorDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var motor = await _motorController.GetMotorById(id);

            return Ok(motor);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostMotorPath}")]
    [Produces<MotorDo>]
    public async Task<IActionResult> Post(MotorDo motorDo)
    {
        try
        {
            var motor = await _motorController.CreateMotor(motorDo);

            return Ok(motor);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutMotorPath}")]
    [Produces<MotorDo>]
    public async Task<IActionResult> Put(MotorDo motorDo)
    {
        try
        {
            var motor = await _motorController.EditMotor(motorDo);

            return Ok(motor);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteMotorPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _motorController.DeleteMotor(id);

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

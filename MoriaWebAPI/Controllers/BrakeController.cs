using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class BrakeController : ControllerBase
{
    readonly IBrakeControllerService _brakeController;
    readonly ILogger<BrakeController> _logger;

    public BrakeController(IBrakeControllerService brakeControllerService, ILogger<BrakeController> logger)
    {
        _brakeController = brakeControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetBrakesPath)]
    [Produces<IEnumerable<BrakeDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var brakes = await _brakeController.GetAllBrakes();

            return Ok(brakes);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetBrakePath}/{{id}}")]
    [Produces<BrakeDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var brake = await _brakeController.GetBrakeById(id);

            return Ok(brake);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostBrakePath}")]
    [Produces<BrakeDo>]
    public async Task<IActionResult> Post(BrakeDo brakeDo)
    {
        try
        {
            var color = await _brakeController.CreateBrake(brakeDo);

            return Ok(color);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutBrakePath}")]
    [Produces<BrakeDo>]
    public async Task<IActionResult> Put(BrakeDo brakeDo)
    {
        try
        {
            var brake = await _brakeController.EditBrake(brakeDo);

            return Ok(brake);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteBrakePath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _brakeController.DeleteBrake(id);

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

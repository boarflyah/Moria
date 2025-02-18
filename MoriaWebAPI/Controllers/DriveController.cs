using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class DriveController: ControllerBase
{
    readonly IDriveControllerService _controllerService;
    readonly ILogger<CategoryController> _logger;

    public DriveController(IDriveControllerService controllerService, ILogger<CategoryController> logger)
    {
        _controllerService = controllerService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GetDrivesPath)]
    [Produces<IEnumerable<DriveDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _controllerService.GetDrives();

            return Ok(categories);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetDrivePath}/{{id}}")]
    [Produces<DriveDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _controllerService.GetDrive(id);

            return Ok(category);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostDrivePath}")]
    [Produces<DriveDo>]
    public async Task<IActionResult> Post(DriveDo drive)
    {
        try
        {
            var createdCategory = await _controllerService.CreateDrive(drive);

            return base.Ok((object)createdCategory);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutDrivePath}")]
    [Produces<DriveDo>]
    public async Task<IActionResult> Put(DriveDo drive)
    {
        try
        {
            var updatedCategory = await _controllerService.UpdateDrive(drive);

            return Ok(updatedCategory);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteDrivePath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _controllerService.DeleteDrive(id);

            return Ok(deleted);
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

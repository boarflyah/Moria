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
public class SupplementController : ControllerBase
{
    readonly ISupplementControllerService _supplementController;
    readonly ILogger<SupplementController> _logger;

    public SupplementController(ISupplementControllerService supplementControllerService, ILogger<SupplementController> logger)
    {
        _supplementController = supplementControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetSupplementsPath)]
    [Produces<IEnumerable<SupplementDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var supplements = await _supplementController.GetAllSupplements();

            return Ok(supplements);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetSupplementPath}/{{id}}")]
    [Produces<SupplementDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var supplement = await _supplementController.GetSupplementById(id);

            return Ok(supplement);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostSupplementPath}")]
    [Produces<SupplementDo>]
    public async Task<IActionResult> Post(SupplementDo supplementDo)
    {
        try
        {
            var supplement = await _supplementController.CreateSupplement(supplementDo);

            return Ok(supplement);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutSupplementPath}")]
    [Produces<SupplementDo>]
    public async Task<IActionResult> Put(SupplementDo supplementDo)
    {
        try
        {
            var supplementResult = await _supplementController.EditSupplement(supplementDo);

            return Ok(supplementResult);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteSupplementPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _supplementController.DeleteSupplement(id);

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

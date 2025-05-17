using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaModels.Models.Electrical;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class ElectricalCabinetController : ControllerBase
{
    readonly IElectricalCabinetControllerService _cabinetController;
    readonly ILogger<ElectricalCabinetController> _logger;

    public ElectricalCabinetController(IElectricalCabinetControllerService cabinetControllerService, ILogger<ElectricalCabinetController> logger)
    {
        _cabinetController = cabinetControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetElectricalCabinetsPath)]
    [Produces<IEnumerable<ColorDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var colors = await _cabinetController.GetElectricalCabinets();

            return Ok(colors);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetElectricalCabinetPath}/{{id}}")]
    [Produces<ColorDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var color = await _cabinetController.GetElectricalCabinet(id);

            return Ok(color);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostElectricalCabinetPath}")]
    [Produces<ElectricalCabinetDo>]
    public async Task<IActionResult> Post(ElectricalCabinetDo cabinetdo)
    {
        try
        {
            var color = await _cabinetController.CreateElectricalCabinet(cabinetdo);

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

    [HttpPut($"{WebAPIEndpointsProvider.PutElectricalCabinetPath}")]
    [Produces<ElectricalCabinetDo>]
    public async Task<IActionResult> Put(ElectricalCabinetDo cabinetdo)
    {
        try
        {
            var color = await _cabinetController.UpdateElectricalCabinet(cabinetdo);

            return Ok(color);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteElectricalCabinetPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _cabinetController.DeleteElectricalCabinet(id);

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

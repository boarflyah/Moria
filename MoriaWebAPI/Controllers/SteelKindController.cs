using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class SteelKindController : ControllerBase
{
    readonly ISteelKindControllerService _steelKindService;
    readonly ILogger<SteelKindController> _logger;

    public SteelKindController(ISteelKindControllerService steelKindControllerService, ILogger<SteelKindController> logger)
    {
        _steelKindService = steelKindControllerService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GeSteelKindsPath)]
    [Produces<IEnumerable<SteelKindDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var steelKinds = await _steelKindService.GetAllSteelKinds();

            return Ok(steelKinds);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetSteelKindPath}/{{id}}")]
    [Produces<SteelKindDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var steelKind = await _steelKindService.GetSteelKindById(id);

            return Ok(steelKind);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostSteelKindPath}")]
    [Produces<SteelKindDo>]
    public async Task<IActionResult> Post(SteelKindDo steelKindDo)
    {
        try
        {
            var steelKind = await _steelKindService.CreateSteelKind(steelKindDo);

            return Ok(steelKind);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutSteelKindPath}")]
    [Produces<SteelKindDo>]
    public async Task<IActionResult> Put(SteelKindDo steelKindDo)
    {
        try
        {
            var steelKind = await _steelKindService.EditSteelKind(steelKindDo);

            return Ok(steelKind);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteSteelKindPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _steelKindService.DeleteSteelKind(id);

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

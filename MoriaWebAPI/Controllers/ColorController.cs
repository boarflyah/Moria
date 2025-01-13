using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPI.Controllers;
[ApiController]
[Route("")]
[Authorize]
public class ColorController : ControllerBase
{
    readonly IColorControllerService _colorController;
    readonly ILogger<ColorController> _logger;

    public ColorController(IColorControllerService colorControllerService, ILogger<ColorController> logger)
    {
        _colorController = colorControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetColorsPath)]
    [Produces<IEnumerable<ColorDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var colors = await _colorController.GetAllColors();

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

    [HttpGet($"{WebAPIEndpointsProvider.GetColorPath}/{{id}}")]
    [Produces<ColorDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var color = await _colorController.GetColorById(id);

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

    [HttpPost($"{WebAPIEndpointsProvider.PostColorPath}")]
    [Produces<ColorDo>]
    public async Task<IActionResult> Post(ColorDo colorDo)
    {
        try
        {
            var color = await _colorController.CreateColor(colorDo);

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

    [HttpPut($"{WebAPIEndpointsProvider.PutColorPath}")]
    [Produces<ColorDo>]
    public async Task<IActionResult> Put(ColorDo colorDo)
    {
        try
        {
            var color = await _colorController.EditColor(colorDo);

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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteColorPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _colorController.DeleteColor(id);

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

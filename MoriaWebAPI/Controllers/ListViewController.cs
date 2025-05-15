using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModels.Models.Helpers;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Base;
using MoriaWebAPIServices.Services;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class ListViewController : ControllerBase
{

    readonly ILogger<ListViewController> _logger;
    readonly IListViewControllerService _listViewControllerService;
    readonly ModelTypeConverter _typeConverter;

    public ListViewController(ILogger<ListViewController> logger, IListViewControllerService listViewControllerService, ModelTypeConverter typeConverter)
    {
        _logger = logger;
        _listViewControllerService = listViewControllerService;
        _typeConverter = typeConverter;
    }

    [HttpPut($"{WebAPIEndpointsProvider.GetSearchPath}")]
    // [Produces<ColorDo>]
    public async Task<IActionResult> GetSearch(SearchRequest searchRequest)
    {
        try
        {
            var entityType = _typeConverter.GetModelType(Type.GetType(searchRequest.ModelDoType));
            if (entityType == null)
                throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

            var method = typeof(IListViewControllerService).GetMethod(nameof(IListViewControllerService.SearchAsync))
                 ?.MakeGenericMethod(entityType);

            if (method == null)
                return StatusCode(500, "Nie udało się odnaleźć metody wyszukiwania.");

            var task = (Task)method.Invoke(_listViewControllerService, new object[] { searchRequest.SearchText });


            var resultProperty = task.GetType().GetProperty("Result");
            var result = resultProperty.GetValue(task);

            return Ok(result);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetSearch)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpPut(WebAPIEndpointsProvider.PutListViewSetupPath)]
    public async Task<IActionResult> PutListViewSetup(ListViewSetupDo setup)
    {
        try
        {
            var employeeId = 0;
            if (int.TryParse(User.FindFirst("id")?.Value, out employeeId))
            {
                await _listViewControllerService.CreateUpdateListViewSetup(employeeId, setup);

                return Ok();
            }

            return ValidationProblem();
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(PutListViewSetup)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpGet($"{WebAPIEndpointsProvider.GetListViewSetupPath}/{{listViewName}}")]
    public async Task<IActionResult> GetListViewSetup(string listViewName)
    {
        try
        {
            var employeeId = 0;
            if (int.TryParse(User.FindFirst("id")?.Value, out employeeId))
            {
                var result = await _listViewControllerService.GetListViewSetupDo(employeeId, listViewName);

                return Ok(System.Text.Json.JsonSerializer.Serialize(result));
            }

            return ValidationProblem();
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetListViewSetup)}");
            return StatusCode(501, ex.Message);
        }
    }

}

using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModels.Models.Helpers;
using MoriaWebAPIServices.Services;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class LookupController: ControllerBase
{
    readonly ILogger<LockController> _logger;
    readonly ILookupControllerService _controllerService;
    readonly IListViewControllerService _listviewControllerService;
    readonly ModelTypeConverter _typeConverter;

    public LookupController(ILogger<LockController> logger, ILookupControllerService controllerService, ModelTypeConverter converter,
        IListViewControllerService listViewControllerService)
    {
        _logger = logger;
        _controllerService = controllerService;
        _typeConverter = converter;
        _listviewControllerService = listViewControllerService;
    }

    [HttpGet($"{WebAPIEndpointsProvider.GetLookupPath}")]
    [Produces<PagedList<LookupModel>>]
    public async Task<IActionResult> Get([FromBody]LookupBodyHelper helper)
    {
        try
        {
            var entityType = _typeConverter.GetModelType(Type.GetType(helper.ModelDoType));
            if (entityType.GetCustomAttribute(typeof(LookupHeadersAttribute)) is LookupHeadersAttribute attribute)
            {
                var headersMetadata = attribute.GetLookupHeadersMetadata();
                if (headersMetadata != null)
                    Response.Headers.Add(new(WebAPIEndpointsProvider.LookupMetadataHeaderKey, new StringValues(JsonSerializer.Serialize(headersMetadata))));
            }

            var collection = await _controllerService.GetObjects(entityType, helper.LastId, helper.PageSize);

            Response.Headers.Append(WebAPIEndpointsProvider.PagedListHasNextHeaderKey, new StringValues(JsonSerializer.Serialize(collection.HasNext)));
            Response.Headers.Append(WebAPIEndpointsProvider.PagedListLastId, new StringValues(JsonSerializer.Serialize(collection.LastId)));

            return Ok(collection);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutLookupFilteredPath}")]
    [Produces<PagedList<LookupModel>>]
    public async Task<IActionResult> GetFiltered(SearchRequest request)
    {
        try
        {
            var entityType = _typeConverter.GetModelType(Type.GetType(request.ModelDoType));
            if (entityType.GetCustomAttribute(typeof(LookupHeadersAttribute)) is LookupHeadersAttribute attribute)
            {
                var headersMetadata = attribute.GetLookupHeadersMetadata();
                if (headersMetadata != null)
                    Response.Headers.Add(new(WebAPIEndpointsProvider.LookupMetadataHeaderKey, new StringValues(JsonSerializer.Serialize(headersMetadata))));
            }

            var collection = await _listviewControllerService.GetLookupObjects(entityType, request.SearchText);

            //Response.Headers.Append(WebAPIEndpointsProvider.PagedListHasNextHeaderKey, new StringValues(JsonSerializer.Serialize(collection.HasNext)));
            //Response.Headers.Append(WebAPIEndpointsProvider.PagedListLastId, new StringValues(JsonSerializer.Serialize(collection.LastId)));

            return Ok(collection);
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
}

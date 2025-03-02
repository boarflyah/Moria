﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaModelsDo.Models.Contacts;
using Microsoft.Extensions.Primitives;
using MoriaModels.Models.EntityPersonel;
using System.Reflection;
using MoriaBaseModels.Attributes;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class PositionController : ControllerBase
{
    readonly IPositionControllerService _positionService;
    readonly ILogger<PositionController> _logger;

    public PositionController(IPositionControllerService positionControllerService, ILogger<PositionController> logger)
    {
        _positionService = positionControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetPositionsPath)]
    [Produces<IEnumerable<PositionDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var positions = await _positionService.GetAllPositions();

            return Ok(positions);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetPositionPath}/{{id}}")]
    [Produces<PositionDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var position = await _positionService.GetPositionById(id);

            return Ok(position);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostPositionPath}")]
    [Produces<PositionDo>]
    public async Task<IActionResult> Post(PositionDo positionDo)
    {
        try
        {
            var position = await _positionService.CreatePosition(positionDo);

            return Ok(position);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutPositionPath}")]
    [Produces<PositionDo>]
    public async Task<IActionResult> Put(PositionDo positionDo)
    {
        try
        {
            var position = await _positionService.EditPosition(positionDo);

            return Ok(position);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeletePositionPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _positionService.DeletePosition(id);

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

    [HttpGet($"{WebAPIEndpointsProvider.GetPositionLookupObjectsPath}/{{latestId?}}/{{pageSize?}}")]
    [Produces<IEnumerable<PositionDo>>]
    public async Task<IActionResult> GetLookupObjects(int latestId = 0, int pageSize = int.MaxValue)
    {
        try
        {
            if (typeof(Position).GetCustomAttribute(typeof(LookupHeadersAttribute)) is LookupHeadersAttribute attribute)
            {
                var headersMetadata = attribute.GetLookupHeadersMetadata();
                if(headersMetadata != null)
                    Response.Headers.Add(new(WebAPIEndpointsProvider.LookupMetadataHeaderKey, new StringValues(System.Text.Json.JsonSerializer.Serialize(headersMetadata))));
            }
            var lookupObjects = await _positionService.GetLookupObjects(latestId, pageSize);
            return Ok(lookupObjects);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetLookupObjects)}");
            return StatusCode(501, ex.Message);
        }

    }
}

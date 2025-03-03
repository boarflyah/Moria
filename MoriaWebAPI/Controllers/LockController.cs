using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Base;
using MoriaWebAPIServices.Services;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class LockController : ControllerBase
{
    readonly ILogger<LockController> _logger;
    readonly ILockControllerService _controllerService;

    public LockController(ILogger<LockController> logger, ILockControllerService controllerService)
    {
        _logger = logger;
        _controllerService = controllerService;
    }

    [HttpPut($"{WebAPIEndpointsProvider.PutLockPath}")]
    [Produces<bool>]
    public async Task<IActionResult> Lock(LockHelper lockHelper)
    {
        try
        {
            var user = await _controllerService.Lock(lockHelper);

            return Ok(user);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Lock)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpPut($"{WebAPIEndpointsProvider.PutUnlockPath}")]
    [Produces<bool>]
    public async Task<IActionResult> Unlock(LockHelper lockHelper)
    {
        try
        {
            var user = await _controllerService.Unlock(lockHelper);

            return Ok(user);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Unlock)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpPut($"{WebAPIEndpointsProvider.KeepAlivePath}")]
    [Produces<bool>]
    public async Task<IActionResult> KeepAlive(LockHelper lockHelper)
    {
        try
        {
            var result = await _controllerService.KeepAlive(lockHelper);
            
            return Ok();
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(KeepAlive)}");
            return StatusCode(501, ex.Message);
        }
    }   

    [HttpDelete($"{WebAPIEndpointsProvider.RemoveObjectKeepAlivePath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> RemoveObjectKeepAlive(int id)
    {
        try
        {
            var result = await _controllerService.RemoveObjectKeepAlive(id);
            ;
            return Ok();
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(RemoveObjectKeepAlive)}");
            return StatusCode(501, ex.Message);
        }
    }
}

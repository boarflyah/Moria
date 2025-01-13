using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class WarehouseController : ControllerBase
{
    readonly IWarehouseControllerService _warehouseService;
    readonly ILogger<WarehouseController> _logger;

    public WarehouseController(IWarehouseControllerService warehouseService, ILogger<WarehouseController> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GetWarehousesPath)]
    [Produces<IEnumerable<WarehouseDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var warehouses = await _warehouseService.GetAllWarehouses();

            return Ok(warehouses);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetWarehousePath}/{{id}}")]
    [Produces<WarehouseDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var warehouse = await _warehouseService.GetWarehouseById(id);

            return Ok(warehouse);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostWarehousePath}")]
    [Produces<WarehouseDo>]
    public async Task<IActionResult> Post(WarehouseDo warehouseDo)
    {
        try
        {
            var warehouse = await _warehouseService.CreateWarehouse(warehouseDo);

            return Ok(warehouse);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutWarehousePath}")]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Put(WarehouseDo warehouseDo)
    {
        try
        {
            var warehouse = await _warehouseService.EditWarehouse(warehouseDo);

            return Ok(warehouse);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteWarehousePath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _warehouseService.DeleteWarehouse(id);

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

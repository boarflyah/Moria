using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Services.Interfaces.Orders;
using MoriaModelsDo.Models.Orders;

namespace MoriaWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("")]
public class OrderController: ControllerBase
{
    readonly IOrderControllerService _controllerService;
    readonly ILogger<CategoryController> _logger;

    public OrderController(IOrderControllerService controllerService, ILogger<CategoryController> logger)
    {
        _controllerService = controllerService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GetOrdersPath)]
    [Produces<IEnumerable<OrderDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _controllerService.GetOrders();

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

    [HttpGet($"{WebAPIEndpointsProvider.GetOrderPath}/{{id}}")]
    [Produces<OrderDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _controllerService.GetOrder(id);

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

    [HttpPost($"{WebAPIEndpointsProvider.PostOrderPath}")]
    [Produces<OrderDo>]
    public async Task<IActionResult> Post(OrderDo order)
    {
        try
        {
            var createdCategory = await _controllerService.CreateOrder(order);

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

    [HttpPut($"{WebAPIEndpointsProvider.PutOrderPath}")]
    [Produces<OrderDo>]
    public async Task<IActionResult> Put(OrderDo category)
    {
        try
        {
            var updatedCategory = await _controllerService.UpdateOrder(category);

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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteOrderPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _controllerService.DeleteOrder(id);

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

    [HttpGet(WebAPIEndpointsProvider.GetImportOrdersPath)]
    [AllowAnonymous]
    public async Task<IActionResult> ImportOrders()
    {
        try
        {
            await _controllerService.ImportOrders();

            return Ok();
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(ImportOrders)}");
            return StatusCode(501, ex.Message);
        }
    }


    [HttpGet($"{WebAPIEndpointsProvider.GetCalendarOrdersPath}/{{weekNumber}}")]
    [Produces<IEnumerable<OrderDo>>]
    public async Task<IActionResult> GetCalendarOrders(int weekNumber)
    {
        try
        {
            var category = await _controllerService.GetCalendarOrders(weekNumber);

            return Ok(category);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetCalendarOrders)}");
            return StatusCode(501, ex.Message);
        }
    }
}

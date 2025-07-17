using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class ProductController: ControllerBase
{
    readonly IProductControllerService _controllerService;
    readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, IProductControllerService controllerService)
    {
        _controllerService = controllerService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GetProductsPath)]
    [Produces<IEnumerable<ProductDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var products = await _controllerService.GetProducts();

            return Ok(products);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetProductPath}/{{id}}")]
    [Produces<ProductDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var product = await _controllerService.GetProduct(id);

            return Ok(product);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostProductPath}")]
    [Produces<ProductDo>]
    public async Task<IActionResult> Post(ProductDo product)
    {
        try
        {
            var createdProduct = await _controllerService.CreateProduct(product);

            return base.Ok((object)createdProduct);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutProductPath}")]
    [Produces<ProductDo>]
    public async Task<IActionResult> Put(ProductDo product)
    {
        try
        {
            var updatedProduct = await _controllerService.UpdateProduct(product);

            return Ok(updatedProduct);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteProductPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _controllerService.DeleteProduct(id);

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

    [HttpGet($"{WebAPIEndpointsProvider.GetProductDrives}/{{id}}")]
    [Produces<IEnumerable<ComponentToOrderItemDo>>]
    public async Task<IActionResult> GetProductDrives(int id)
    {
        try
        {
            var deleted = await _controllerService.GetProductDrives(id);

            return Ok(deleted);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, new MoriaApiException(mae.Reason, mae.Status, mae.AdditionalMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetProductDrives)}");
            return StatusCode(501, ex.Message);
        }
    }
}

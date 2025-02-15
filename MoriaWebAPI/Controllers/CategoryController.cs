using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("")]
public class CategoryController: ControllerBase
{
    readonly ICategoryControllerService _controllerService;
    readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryControllerService controllerService, ILogger<CategoryController> logger)
    {
        _controllerService = controllerService;
        _logger = logger;
    }

    [HttpGet(WebAPIEndpointsProvider.GetCategoriesPath)]
    [Produces<IEnumerable<CategoryDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _controllerService.GetCategories();

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

    [HttpGet($"{WebAPIEndpointsProvider.GetCategoryPath}/{{id}}")]
    [Produces<CategoryDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _controllerService.GetCategory(id);

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

    [HttpPost($"{WebAPIEndpointsProvider.PostCategoryPath}")]
    [Produces<CategoryDo>]
    public async Task<IActionResult> Post(CategoryDo category)
    {
        try
        {
            var createdCategory = await _controllerService.CreateCategory(category);

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

    [HttpPut($"{WebAPIEndpointsProvider.PutCategoryPath}")]
    [Produces<CategoryDo>]
    public async Task<IActionResult> Put(CategoryDo category)
    {
        try
        {
            var updatedCategory = await _controllerService.UpdateCategory(category);

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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteProductPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _controllerService.DeleteCategory(id);

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

}

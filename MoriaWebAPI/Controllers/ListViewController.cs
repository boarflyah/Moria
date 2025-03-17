using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaModels.Models.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoriaModelsDo.Base;
using MoriaWebAPIServices.Services;
using Microsoft.IdentityModel.Logging;

namespace MoriaWebAPI.Controllers
{

    [ApiController]
    [Route("")]
    [Authorize]
    public class ListViewController : ControllerBase
    {

        readonly ILogger<ListViewController> _logger;
        readonly IListViewControllerService _listViewControllerService;
        readonly ModelTypeConverter _typeConverter;

        public ListViewController(ILogger<ListViewController> logger , IListViewControllerService listViewControllerService, ModelTypeConverter typeConverter)
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

    }
}

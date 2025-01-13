using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaBaseServices;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPI.Controllers;
[ApiController]
[Route("")]
[Authorize]
public class ContactController : ControllerBase
{
    readonly IContactControllerService _contactController;
    readonly ILogger<ContactController> _logger;

    public ContactController(IContactControllerService contactControllerService, ILogger<ContactController> logger)
    {
        _contactController = contactControllerService;
        _logger = logger;
    }


    [HttpGet(WebAPIEndpointsProvider.GetContactsPath)]
    [Produces<IEnumerable<ContactDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var contacts = await _contactController.GetAllContacts();

            return Ok(contacts);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetContactPath}/{{id}}")]
    [Produces<ContactDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var contact = await _contactController.GetContactById(id);

            return Ok(contact);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostContactPath}")]
    [Produces<ContactDo>]
    public async Task<IActionResult> Post(ContactDo contactDo)
    {
        try
        {
            var contact = await _contactController.CreateContact(contactDo);

            return Ok(contact);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutContactPath}")]
    [Produces<ContactDo>]
    public async Task<IActionResult> Put(ContactDo contactDo)
    {
        try
        {
            var contact = await _contactController.EditContact(contactDo);

            return Ok(contact);
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

    [HttpDelete($"{WebAPIEndpointsProvider.DeleteContactPath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _contactController.DeleteContact(id);

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

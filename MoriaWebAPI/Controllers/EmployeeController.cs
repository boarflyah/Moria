using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class EmployeeController: ControllerBase
{
    readonly IEmployeeControllerService _employeeService;
    readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeControllerService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpPost(WebAPIEndpointsProvider.PostLoginPath)]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Login(UserCredentials credentials)
    {
        try
        {
            var user = await _employeeService.LogIn(credentials.Username, credentials.Password);
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Login)}");
            return StatusCode(501, ex.Message);
        }

    }

    [HttpGet(WebAPIEndpointsProvider.GetEmployeesPath)]
    [Produces<IEnumerable<EmployeeDo>>]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _employeeService.GetEmployees();

            return Ok(user);
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

    [HttpGet($"{WebAPIEndpointsProvider.GetEmployeePath}/{{id}}")]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var user = await _employeeService.GetEmployee(id);

            return Ok(user);
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

    [HttpPost($"{WebAPIEndpointsProvider.PostEmployeePath}")]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Post(EmployeeDo employee)
    {
        try
        {
            var user = await _employeeService.CreateEmployee(employee);

            return Ok(user);
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

    [HttpPut($"{WebAPIEndpointsProvider.PutEmployeePath}")]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Put(EmployeeDo employee)
    {
        try
        {
            var user = await _employeeService.UpdateEmployee(employee);

            return Ok(user);
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

    [HttpDelete($"{WebAPIEndpointsProvider.PostEmployeePath}/{{id}}")]
    [Produces<bool>]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var user = await _employeeService.DeleteEmployee(id);

            return Ok(user);
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

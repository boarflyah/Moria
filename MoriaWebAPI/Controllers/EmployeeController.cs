using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class EmployeeController: ControllerBase
{
    readonly IEmployeeService _employeeService;
    readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
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
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, mae);
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
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, mae);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Get)}");
            return StatusCode(501, ex.Message);
        }
    }

    [HttpPost($"{WebAPIEndpointsProvider.PostEmployeePath}")]
    [Produces<bool>]
    public async Task<IActionResult> Post(EmployeeDo employee)
    {
        try
        {
            var user = await _employeeService.CreateEmployee(employee);

            return Ok(user);
        }
        catch (MoriaApiException mae)
        {
            return StatusCode(MoriaApiException.ApiExceptionThrownStatusCode, mae);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Post)}");
            return StatusCode(501, ex.Message);
        }
    }
}

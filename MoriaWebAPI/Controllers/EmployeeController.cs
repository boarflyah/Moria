using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MoriaBaseServices.Services;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
public class EmployeeController: ControllerBase
{
    readonly IUserService _userService;
    readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost(WebAPIEndpointsProvider.PostLoginPath)]
    [Authorize]
    [Produces<EmployeeDo>]
    public async Task<IActionResult> Login(UserCredentials credentials)
    {
        try
        {
            var user = await _userService.LogIn(credentials.Username, credentials.Password);
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
}

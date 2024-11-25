using Microsoft.AspNetCore.Mvc;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
public class EmployeeController: ControllerBase
{
    readonly IUserService _userService;

    public EmployeeController(IUserService userService)
    {
        _userService = userService;
    }


}

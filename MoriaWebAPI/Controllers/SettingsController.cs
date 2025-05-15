using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class SettingsController: ControllerBase
{
    readonly ILogger<SettingsController> _logger;

    public SettingsController(ILogger<SettingsController> logger)
    {
        _logger = logger;
    }
}

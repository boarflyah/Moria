using Microsoft.AspNetCore.Mvc;
using MoriaBaseServices.Services;
using MoriaModels.Models.EntityPersonel;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPI.Controllers;

[ApiController]
[Route("")]
public class TokenController : ControllerBase
{
    readonly ITokenGeneratorService _tokenGeneratorService;
    readonly IUserService _userService;
    readonly ILogger<TokenController> _logger;

    public TokenController(ITokenGeneratorService tokenGeneratorService, IUserService userService, ILogger<TokenController> logger)
    {
        _logger = logger;
        _tokenGeneratorService = tokenGeneratorService;
        _userService = userService;
    }

    [HttpPost(WebAPIEndpointsProvider.PostTokenPath)]
    public IActionResult Post(UserCredentials credentials)
    {
        try
        {
            var userId = _userService.LogIn(credentials.Username, credentials.Password);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            return Ok(_tokenGeneratorService.GenerateJwtToken(userId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(Post)}");
            return StatusCode(501, ex.Message);
        }
    }

#if DEBUG

    /// <summary>
    /// Only for testing purposes, always returns token for userid = 123
    /// </summary>
    /// <returns></returns>
    [HttpGet(WebAPIEndpointsProvider.GetTokenGetTokenPath)]
    public IActionResult GetToken()
    {
        try
        {
            var token = _tokenGeneratorService.GenerateJwtToken("123");

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Method: {nameof(GetToken)}");
            return StatusCode(501, ex.Message);
        }
    }

#endif
}

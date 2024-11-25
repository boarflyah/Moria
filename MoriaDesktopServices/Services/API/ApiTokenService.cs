using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Services.API;

/// <summary>
/// Used for reading enpoints in TokenController in MoriaApi
/// </summary>
public class ApiTokenService : IApiTokenService
{
    readonly IApiService _apiService;
    readonly ITokensManager _tokensManager;

    public ApiTokenService(IApiService apiService, ITokensManager tokensManager)
    {
        _apiService = apiService;
        _tokensManager = tokensManager;
    }

    public async Task<EmployeeDo> GetUserWithToken(string username, string password)
    {
        var employee = await _apiService.Post<EmployeeDo>(string.Empty, WebAPIEndpointsProvider.PostTokenPath, null, new UserCredentials() { Username = username, Password = password });

        //saving token and expiration date to tokens manager
        if (employee != null)
        {
            _tokensManager.StoreToken(username, employee.Token, employee.ValidTo);
        }

        return employee;
    }
}

using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Services.API;

/// <summary>
/// Used for reading enpoints in TokenController in MoriaApi
/// </summary>
public class ApiEmployeeService : IApiEmployeeService
{
    readonly IApiService _apiService;

    public ApiEmployeeService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<EmployeeDo> Login(string username, string password)
    {
        return await _apiService.Post<EmployeeDo>(username, WebAPIEndpointsProvider.PostLoginPath, null, new UserCredentials() { Username = username, Password = password }); 
    }
}

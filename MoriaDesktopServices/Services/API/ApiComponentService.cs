using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Services.API;
public class ApiComponentService: IApiComponentService
{
    readonly IApiService _apiService;

    public ApiComponentService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<ComponentDo> GetComponent(string username, int id)
    {
        return await _apiService.Get<ComponentDo>(username, WebAPIEndpointsProvider.GetComponentPath, null, null, id);
    }

}

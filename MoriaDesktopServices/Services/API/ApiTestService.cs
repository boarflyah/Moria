using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;

namespace MoriaDesktopServices.Services.API;
public class ApiTestService
{
    readonly IApiService _apiService;

    public ApiTestService(IApiService apiService)
    {
        _apiService = apiService;
    }

#if DEBUG
    public async Task<string> Get()
    {
        return await _apiService.Get("123", WebAPIEndpointsProvider.GetTestPath, null, null);
    }
#endif
}

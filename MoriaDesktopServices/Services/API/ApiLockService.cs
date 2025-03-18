using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Services.API;
public class ApiLockService: IApiLockService
{
    readonly IApiService _apiService;

    public ApiLockService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<bool> Lock(string username, Type modelType, int id)
    {
        return await _apiService.Put<bool>(username, WebAPIEndpointsProvider.PutLockPath, null, new LockHelper() { Id = id, ModelDoType = modelType.AssemblyQualifiedName, Username = username });
    }

    public async Task<bool> Unlock(string username, Type modelType, int id)
    {
        return await _apiService.Put<bool>(username, WebAPIEndpointsProvider.PutUnlockPath, null, new LockHelper() { Id = id, ModelDoType = modelType.AssemblyQualifiedName, Username = username });
    }

    public async Task<bool> RemoveObjectKeepAlive(string username,  int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.RemoveObjectKeepAlivePath, null, id);
    }
}

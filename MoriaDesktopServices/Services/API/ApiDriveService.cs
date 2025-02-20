using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Services.API;
public class ApiDriveService : IApiDriveService
{
    readonly IApiService _apiService;

    public ApiDriveService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<DriveDo> CreateDrive(string username, DriveDo drive)
    {
        return await _apiService.Post<DriveDo>(username, WebAPIEndpointsProvider.PostDrivePath, null, drive);
    }

    public async Task<bool> DeleteDrive(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteDrivePath, null, id);
    }

    public async Task<DriveDo> GetDrive(string username, int id)
    {
        return await _apiService.Get<DriveDo>(username, WebAPIEndpointsProvider.GetDrivePath, null, null, id);
    }

    public async Task<IEnumerable<DriveDo>> GetDrives(string username)
    {
        var result = await _apiService.Get<IEnumerable<DriveDo>>(username, WebAPIEndpointsProvider.GetDrivesPath, null, null);
        if (result == null)
            return new List<DriveDo>();

        return result;
    }

    public async Task<DriveDo> UpdateDrive(string username, DriveDo drive)
    {
        return await _apiService.Put<DriveDo>(username, WebAPIEndpointsProvider.PutDrivePath, null, drive);
    }
}

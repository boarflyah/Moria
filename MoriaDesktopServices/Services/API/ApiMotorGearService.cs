using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.DriveComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in MotorGearController in MoriaApi
/// </summary>
public class ApiMotorGearService : IApiMotorGearService
{
    readonly IApiService _apiService;

    public ApiMotorGearService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<MotorGearDo>> GetMotorGears(string username)
    {
        var result = await _apiService.Get<IEnumerable<MotorGearDo>>(username, WebAPIEndpointsProvider.GetMotorGearsPath, null, null);
        if (result == null)
            return new List<MotorGearDo>();

        return result;
    }

    public async Task<MotorGearDo> GetMotorGear(string username, int id)
    {
        return await _apiService.Get<MotorGearDo>(username, WebAPIEndpointsProvider.GetMotorGearPath, null, null, parameters: id);
    }

    public async Task<MotorGearDo> CreateMotorGear(string username, MotorGearDo motorGear)
    {
        return await _apiService.Post<MotorGearDo>(username, WebAPIEndpointsProvider.PostMotorGearPath, null, motorGear);
    }

    public async Task<MotorGearDo> UpdateMotorGear(string username, MotorGearDo motorGear)
    {
        return await _apiService.Put<MotorGearDo>(username, WebAPIEndpointsProvider.PutMotorGearPath, null, motorGear);
    }

    public async Task<bool> DeleteMotorGear(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteMotorGearPath, null, id);
    }
}

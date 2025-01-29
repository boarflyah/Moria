using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in MotorController in MoriaApi
/// </summary>
public class ApiMotorService : IApiMotorService
{
    readonly IApiService _apiService;

    public ApiMotorService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<MotorDo>> GetMotors(string username)
    {
        var result = await _apiService.Get<IEnumerable<MotorDo>>(username, WebAPIEndpointsProvider.GetMotorsPath, null, null);
        if (result == null)
            return new List<MotorDo>();

        return result;
    }

    public async Task<MotorDo> GetMotor(string username, int id)
    {
        return await _apiService.Get<MotorDo>(username, WebAPIEndpointsProvider.GetMotorPath, null, null, parameters: id);
    }

    public async Task<MotorDo> CreateMotor(string username, MotorDo motor)
    {
        return await _apiService.Post<MotorDo>(username, WebAPIEndpointsProvider.PostMotorPath, null, motor);
    }

    public async Task<MotorDo> UpdateMotor(string username, MotorDo motor)
    {
        return await _apiService.Put<MotorDo>(username, WebAPIEndpointsProvider.PutMotorPath, null, motor);
    }

    public async Task<bool> DeleteMotor(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteMotorPath, null, id);
    }
}

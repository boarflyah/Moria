using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Services.API;

public class ApiPumpService : IApiPumpService
{
    readonly IApiService _apiService;

    public ApiPumpService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<PumpDo> CreatePump(string username, PumpDo pumpDo)
    {
        return await _apiService.Post<PumpDo>(username, WebAPIEndpointsProvider.PostPumpPath, null, pumpDo);
    }

    public async Task<bool> DeletePump(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeletePumpPath, null, id);
    }

    public async Task<PumpDo> GetPump(string username, int id)
    {
        return await _apiService.Get<PumpDo>(username, WebAPIEndpointsProvider.GetPumpPath, null, null, parameters: id);
    }

    public async Task<IEnumerable<PumpDo>> GetPumps(string username)
    {
        var result = await _apiService.Get<IEnumerable<PumpDo>>(username, WebAPIEndpointsProvider.GetPumpsPath, null, null);
        if (result == null)
            return new List<PumpDo>();

        return result;
    }

    public async Task<PumpDo> UpdatePump(string username, PumpDo pumpDo)
    {
        return await _apiService.Put<PumpDo>(username, WebAPIEndpointsProvider.PutPumpPath, null, pumpDo);
    }
}

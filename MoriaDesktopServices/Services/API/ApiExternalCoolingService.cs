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

public class ApiExternalCoolingService : IApiExternalCoolingService
{
    readonly IApiService _apiService;

    public ApiExternalCoolingService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<ExternalCoolingDo> CreateExternalCooling(string username, ExternalCoolingDo externalCoolingDo)
    {
        return await _apiService.Post<ExternalCoolingDo>(username, WebAPIEndpointsProvider.PostExternalCoolingPath, null, externalCoolingDo);
    }

    public async Task<bool> DeleteExternalCooling(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteColorPath, null, id);
    }

    public async Task<ExternalCoolingDo> GetExternalCooling(string username, int id)
    {
        return await _apiService.Get<ExternalCoolingDo>(username, WebAPIEndpointsProvider.GetExternalCoolingPath, null, null, parameters: id);
    }

    public async Task<IEnumerable<ExternalCoolingDo>> GetExternalCoolings(string username)
    {
        var result = await _apiService.Get<IEnumerable<ExternalCoolingDo>>(username, WebAPIEndpointsProvider.GetExternalCoolingsPath, null, null);
        if (result == null)
            return new List<ExternalCoolingDo>();

        return result;
    }

    public async Task<ExternalCoolingDo> UpdateExternalCooling(string username, ExternalCoolingDo externalCoolingDo)
    {
        return await _apiService.Put<ExternalCoolingDo>(username, WebAPIEndpointsProvider.PutExternalCoolingPath, null, externalCoolingDo);
    }
}

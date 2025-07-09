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

public class ApiVariatorService : IApiVariatorService
{
    readonly IApiService _apiService;

    public ApiVariatorService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<VariatorDo> CreateVariator(string username, VariatorDo variatorDo)
    {
        return await _apiService.Post<VariatorDo>(username, WebAPIEndpointsProvider.PostVariatorPath, null, variatorDo);
    }

    public async Task<bool> DeleteVariator(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteVariatorPath, null, id);
    }

    public async Task<VariatorDo> GetVariator(string username, int id)
    {
        return await _apiService.Get<VariatorDo>(username, WebAPIEndpointsProvider.GetVariatorPath, null, null, parameters: id);
    }

    public async Task<IEnumerable<VariatorDo>> GetVariators(string username)
    {
        var result = await _apiService.Get<IEnumerable<VariatorDo>>(username, WebAPIEndpointsProvider.GetVariatorsPath, null, null);
        if (result == null)
            return new List<VariatorDo>();

        return result;
    }

    public async Task<VariatorDo> UpdateVariator(string username, VariatorDo variatorDo)
    {
        return await _apiService.Put<VariatorDo>(username, WebAPIEndpointsProvider.PostVariatorPath, null, variatorDo);
    }
}

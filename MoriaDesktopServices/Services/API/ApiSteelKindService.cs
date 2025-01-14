using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in SteelKindController in MoriaApi
/// </summary>
public class ApiSteelKindService : IApiSteelKindService
{
    readonly IApiService _apiService;

    public ApiSteelKindService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<SteelKindDo>> GetSteelKinds(string username)
    {
        var result = await _apiService.Get<IEnumerable<SteelKindDo>>(username, WebAPIEndpointsProvider.GetSteelKindsPath, null);
        if (result == null)
            return new List<SteelKindDo>();

        return result;
    }

    public async Task<SteelKindDo> GetSteelKind(string username, int id)
    {
        return await _apiService.Get<SteelKindDo>(username, WebAPIEndpointsProvider.GetSteelKindPath, null, id);
    }

    public async Task<SteelKindDo> CreateSteelKind(string username, SteelKindDo steelKind)
    {
        return await _apiService.Post<SteelKindDo>(username, WebAPIEndpointsProvider.PostSteelKindPath, null, steelKind);
    }

    public async Task<SteelKindDo> UpdateSteelKind(string username, SteelKindDo steelKind)
    {
        return await _apiService.Put<SteelKindDo>(username, WebAPIEndpointsProvider.PutSteelKindPath, null, steelKind);
    }

    public async Task<bool> DeleteSteelKind(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteSteelKindPath, null, id);
    }
}

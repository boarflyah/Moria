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
/// Used for reading enpoints in PositionController in MoriaApi
/// </summary>
public class ApiPositionService : IApiPositionService
{
    readonly IApiService _apiService;

    public ApiPositionService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<PositionDo>> GetPositions(string username)
    {
        var result = await _apiService.Get<IEnumerable<PositionDo>>(username, WebAPIEndpointsProvider.GetPositionsPath, null, null);
        if (result == null)
            return new List<PositionDo>();

        return result;
    }

    public async Task<PositionDo> GetPosition(string username, int id)
    {
        return await _apiService.Get<PositionDo>(username, WebAPIEndpointsProvider.GetPositionPath, null, null, parameters: id);
    }

    public async Task<PositionDo> CreatePosition(string username, PositionDo position)
    {
        return await _apiService.Post<PositionDo>(username, WebAPIEndpointsProvider.PostPositionPath, null, position);
    }

    public async Task<PositionDo> UpdatePosition(string username, PositionDo position)
    {
        return await _apiService.Put<PositionDo>(username, WebAPIEndpointsProvider.PutPositionPath, null, position);
    }

    public async Task<bool> DeletePosition(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeletePositionPath, null, id);
    }
}

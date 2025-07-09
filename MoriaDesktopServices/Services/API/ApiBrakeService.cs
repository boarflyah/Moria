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

public class ApiBrakeService : IApiBrakeService
{
    readonly IApiService _apiService;

    public ApiBrakeService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<BrakeDo> CreateBrake(string username, BrakeDo brakeDo)
    {
        return await _apiService.Post<BrakeDo>(username, WebAPIEndpointsProvider.PostBrakePath, null, brakeDo);
    }

    public async Task<bool> DeleteBrake(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteBrakePath, null, id);
    }

    public async Task<BrakeDo> GetBrake(string username, int id)
    {
        return await _apiService.Get<BrakeDo>(username, WebAPIEndpointsProvider.GetBrakePath, null, null, parameters: id);
    }

    public async Task<IEnumerable<BrakeDo>> GetBrakes(string username)
    {
        var result = await _apiService.Get<IEnumerable<BrakeDo>>(username, WebAPIEndpointsProvider.GetBrakesPath, null, null);
        if (result == null)
            return new List<BrakeDo>();

        return result;
    }

    public async Task<BrakeDo> UpdateBrake(string username, BrakeDo brakeDo)
    {
        return await _apiService.Put<BrakeDo>(username, WebAPIEndpointsProvider.PutBrakePath, null, brakeDo);
    }
}

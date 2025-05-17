using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Services.API;

public class ApiElectricalCabinetService : IApiElectricalCabinetService
{
    readonly IApiService _apiService;
    public ApiElectricalCabinetService(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<ElectricalCabinetDo> CreateElectricalCabinet(string username, ElectricalCabinetDo cabinet)
    {
        return await _apiService.Post<ElectricalCabinetDo>(username, WebAPIEndpointsProvider.PostElectricalCabinetPath, null, cabinet);
    }

    public async Task<bool> DeleteElectricalCabinet(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteElectricalCabinetPath, null, id);
    }

    public async Task<ElectricalCabinetDo> GetElectricalCabinet(string username, int id)
    {
        return await _apiService.Get<ElectricalCabinetDo>(username, WebAPIEndpointsProvider.GetElectricalCabinetPath, null, null, parameters: id);
    }
    

    public async Task<IEnumerable<ElectricalCabinetDo>> GetElectricalCabinets(string username)
    {
        var result = await _apiService.Get<IEnumerable<ElectricalCabinetDo>>(username, WebAPIEndpointsProvider.GetElectricalCabinetsPath, null, null);
        if (result == null)
            return new List<ElectricalCabinetDo>();

        return result;
    }

    public async Task<ElectricalCabinetDo> UpdateElectricalCabinet(string username, ElectricalCabinetDo cabinet)
    {
        return await _apiService.Put<ElectricalCabinetDo>(username, WebAPIEndpointsProvider.PutElectricalCabinetPath, null, cabinet);
    }
}

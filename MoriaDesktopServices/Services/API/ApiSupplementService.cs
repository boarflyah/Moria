using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Services.API;

public class ApiSupplementService : IApiSupplementService
{
    readonly IApiService _apiService;
    public ApiSupplementService(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<SupplementDo> CreateSupplement(string username, SupplementDo supplementDo)
    {
        return await _apiService.Post<SupplementDo>(username, WebAPIEndpointsProvider.PostSupplementPath, null, supplementDo);
    }

    public async Task<bool> DeleteSupplement(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteSupplementPath, null, id);
    }    

    public async Task<SupplementDo> GetSupplement(string username, int id)
    {
        return await _apiService.Get<SupplementDo>(username, WebAPIEndpointsProvider.GetSupplementPath, null, null, parameters: id);
    }

    public async Task<IEnumerable<SupplementDo>> GetSupplements(string username)
    {
        var result = await _apiService.Get<IEnumerable<SupplementDo>>(username, WebAPIEndpointsProvider.GetSupplementsPath, null, null);
        if (result == null)
            return new List<SupplementDo>();

        return result;
    }

    public async Task<SupplementDo> UpdateSupplement(string username, SupplementDo supplementDo)
    {
        return await _apiService.Put<SupplementDo>(username, WebAPIEndpointsProvider.PutSupplementPath, null, supplementDo);
    }
}

using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in WarehouseController in MoriaApi
/// </summary>
public class ApiWarehouseService : IApiWarehouseService
{
    readonly IApiService _apiService;

    public ApiWarehouseService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<WarehouseDo>> GetWarehouses(string username)
    {
        var result = await _apiService.Get<IEnumerable<WarehouseDo>>(username, WebAPIEndpointsProvider.GetWarehousesPath, null, null);
        if (result == null)
            return new List<WarehouseDo>();

        return result;
    }

    public async Task<WarehouseDo> GetWarehouse(string username, int id)
    {
        return await _apiService.Get<WarehouseDo>(username, WebAPIEndpointsProvider.GetWarehousePath, null, null, parameters: id);
    }

    public async Task<WarehouseDo> CreateWarehouse(string username, WarehouseDo warehouse)
    {
        return await _apiService.Post<WarehouseDo>(username, WebAPIEndpointsProvider.PostWarehousePath, null, warehouse);
    }

    public async Task<WarehouseDo> UpdateWarehouse(string username, WarehouseDo warehouse)
    {
        return await _apiService.Put<WarehouseDo>(username, WebAPIEndpointsProvider.PutWarehousePath, null, warehouse);
    }

    public async Task<bool> DeleteWarehouse(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteWarehousePath, null, id);
    }
}

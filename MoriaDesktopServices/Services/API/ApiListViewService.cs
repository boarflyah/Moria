using MoriaBaseModels.Models;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.Helpers;
using MoriaModelsDo.Models.Base;

namespace MoriaDesktopServices.Services.API;

public class ApiListViewService : IApiListViewService
{
    private readonly IApiService _apiService;   
    public ApiListViewService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<TDo>> Search<TDo>(string username, string searchText)
    {
        var result = await _apiService.Put<IEnumerable<TDo>>(username, WebAPIEndpointsProvider.GetSearchPath, null,new SearchRequest() { ModelDoType = typeof(TDo).AssemblyQualifiedName, SearchText = searchText });
        if (result == null)
            return new List<TDo>();

        return result;
    }

    public async Task CreateUpdateListViewSetup(string username, string listViewId, IList<ListViewColumnProvider> columns)
    {
        var lvs = new ListViewSetupDo()
        {
            ListViewId = listViewId,
            Columns = columns,
        };

        await _apiService.Put(username, WebAPIEndpointsProvider.PutListViewSetupPath, null, lvs);
    }

    public async Task<ListViewSetupDo> GetListViewSetup(string username, string listViewId)
    {
        return await _apiService.Get<ListViewSetupDo>(username, WebAPIEndpointsProvider.GetListViewSetupPath, null, null, listViewId);
    }
}    

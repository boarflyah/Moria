
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.Helpers;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Services.API
{
    public class ListViewService : IListViewService
    {
        private readonly IApiService _apiService;   
        public ListViewService(IApiService apiService)
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
    }    
}

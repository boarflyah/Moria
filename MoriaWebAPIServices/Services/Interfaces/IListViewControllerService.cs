using MoriaBaseModels.Models;
using MoriaModelsDo.Models.Base;

namespace MoriaWebAPIServices.Services.Interfaces;

public interface IListViewControllerService
{
    Task<IEnumerable<TDo>> SearchAsync<TDo>(string searchText)
    where TDo : class;
    Task<List<LookupModel>> GetLookupObjects(Type modelType, string searchText);
    Task CreateUpdateListViewSetup(int employeeId, ListViewSetupDo setup);
    Task<ListViewSetupDo> GetListViewSetupDo(int employeeId, string listViewName);
}

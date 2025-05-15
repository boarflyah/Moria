using MoriaBaseModels.Models;
using MoriaModelsDo.Models.Base;

namespace MoriaDesktopServices.Interfaces;

public interface IApiListViewService
{
    Task<IEnumerable<TDo>> Search<TDo>(string username, string searchText);
    Task<ListViewSetupDo> GetListViewSetup(string username, string listViewId);
    Task CreateUpdateListViewSetup(string username, string listViewId, IList<ListViewColumnProvider> columns);
}

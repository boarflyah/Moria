
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Interfaces{
    public interface IListViewService
    {
        Task<IEnumerable<TDo>> Search<TDo>(string username, string searchText);
    }
}

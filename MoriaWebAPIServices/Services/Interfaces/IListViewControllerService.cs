using MoriaModelsDo.Base;

namespace MoriaWebAPIServices.Services.Interfaces;

public interface IListViewControllerService
{
    Task<IEnumerable<TDo>> SearchAsync<TDo>(string searchText)
    where TDo : class;
}

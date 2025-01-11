using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class WarehouseListViewModel : BaseListViewModel
{
    public WarehouseListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties

    public ObservableCollection<WarehouseDo> Warehouses { get; set; }

    #endregion

    public override Task OnLoaded()
    {
        throw new NotImplementedException();
    }

    public override void OnRowSelected(object row)
    {
        throw new NotImplementedException();
    }
}

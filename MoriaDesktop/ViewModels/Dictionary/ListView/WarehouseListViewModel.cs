using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class WarehouseListViewModel : BaseListViewModel
{
    public WarehouseListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }

    #region properties

    public ObservableCollection<WarehouseDo> Warehouses { get; set; }

    #endregion

    protected override Task LoadList() => throw new NotImplementedException();

    public override void OnRowSelected(object row)
    {
        throw new NotImplementedException();
    }

    protected override void New() => throw new NotImplementedException();

    protected override Task<bool> SendDeleteRequest() => throw new NotImplementedException();
}

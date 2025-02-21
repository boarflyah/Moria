using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class WarehouseListViewModel : BaseListViewModel
{
    readonly IApiWarehouseService _warehouseService;
    public WarehouseListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiWarehouseService apiWarehouseService) : base(logger, appStateService, navigationService)
    {
        _warehouseService = apiWarehouseService;

        Warehouses = new();
        Title = "Magazyny";
    }

    #region properties

    public ObservableCollection<WarehouseDo> Warehouses { get; set; }

    PermissionDo _Permission_Symbol;
    public PermissionDo Permission_Symbol
    {
        get => _Permission_Symbol;
        set
        {
            _Permission_Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_WarehouseName;
    public PermissionDo Permission_WarehouseName
    {
        get => _Permission_WarehouseName;
        set
        {
            _Permission_WarehouseName = value;
            RaisePropertyChanged(value);
        }
    }
    #endregion

    protected async override Task LoadList()
    {
        Warehouses.Clear();

        var warehouses = await ExecuteApiRequest(_warehouseService.GetWarehouses, _appStateService.LoggedUser.Username);
        if (warehouses != null)
        {
            foreach (var warehouse in warehouses)
                Warehouses.Add(warehouse);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }
    public override void OnRowSelected(object row)
    {
        if (row is WarehouseDo wdo)
            _navigationService.NavigateTo(typeof(WarehouseDetailViewModel), false, wdo.Id);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(WarehouseDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_warehouseService.DeleteWarehouse, _appStateService.LoggedUser.Username, (Selected as WarehouseDo)?.Id ?? 0);
}

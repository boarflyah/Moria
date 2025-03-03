using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class WarehouseDetailViewModel : BaseDetailViewModel
{
    readonly IApiWarehouseService _warehouseService;
    public WarehouseDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiWarehouseService warehouseService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _warehouseService = warehouseService;
    }

    #region properties

    string _Symbol;
    [ObjectChangedValidate]
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    string _WarehouseName;
    [ObjectChangedValidate]
    public string WarehouseName
    {
        get => _WarehouseName;
        set
        {
            _WarehouseName = value;
            RaisePropertyChanged(value);
        }
    }

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

    public override Type GetModelType() => typeof(WarehouseDo);

    protected async override Task LoadObject()
    {
        Clear();

        var warehouse = await ExecuteApiRequest(_warehouseService.GetWarehouse, _appStateService.LoggedUser.Username, objectId);
        if (warehouse != null)
            Setup(warehouse);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;

    #endregion

    public override void Clear()
    {
        Symbol = string.Empty;
        WarehouseName = string.Empty;
    }
    void Setup(WarehouseDo warehouse)
    {
        Symbol = warehouse.Symbol;
        WarehouseName = warehouse.Name;
    }

    public override BaseDo GetDo()
        => new WarehouseDo()
        {
            Name = this.WarehouseName,
            Symbol = this.Symbol
        };
}

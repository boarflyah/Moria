using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class SteelKindDetailViewModel : BaseDetailViewModel
{
    readonly IApiSteelKindService _steelKindService;
    public SteelKindDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiSteelKindService steelKindService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _steelKindService = steelKindService;
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

    string _Name;
    [ObjectChangedValidate]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
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

    PermissionDo _Permission_Name;
    public PermissionDo Permission_Name
    {
        get => _Permission_Name;
        set
        {
            _Permission_Name = value;
            RaisePropertyChanged(value);
        }
    }

    public override Type GetModelType() => typeof(SteelKindDo);

    protected async override Task LoadObject()
    {
        Clear();

        var steelKind = await ExecuteApiRequest(_steelKindService.GetSteelKind, _appStateService.LoggedUser.Username, objectId);
        if (steelKind != null)
            Setup(steelKind);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected async override Task<bool> SaveNewObject()
    {
        var steelKind = GetDo() as SteelKindDo;
        var created = await ExecuteApiRequest(_steelKindService.CreateSteelKind, _appStateService.LoggedUser.Username, steelKind);
        if (created != null)
        {
            objectId = created.Id;
            return true;
        }

        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var steelKind = GetDo() as SteelKindDo;
        var created = await ExecuteApiRequest(_steelKindService.UpdateSteelKind, _appStateService.LoggedUser.Username, steelKind);
        if (created != null)
            return true;

        return false;
    }

    #endregion

    public override void Clear()
    {
        Symbol = string.Empty;
        Name = string.Empty;
    }

    void Setup(SteelKindDo steelKind)
    {
        Symbol = steelKind.Symbol;
        Name = steelKind.Name;
    }

    public override BaseDo GetDo()
        => new SteelKindDo()
        {
            Name = this.Name,
            Symbol = this.Symbol
        };
}


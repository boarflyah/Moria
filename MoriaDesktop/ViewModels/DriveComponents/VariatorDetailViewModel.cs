using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class VariatorDetailViewModel : BaseDetailViewModel
{
    readonly IApiVariatorService _apiVariatorService;
    public VariatorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiVariatorService apiVariatorService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiVariatorService = apiVariatorService;
    }

    string _Type;
    [ObjectChangedValidate]
    [DefaultProperty]
    public string Type
    {
        get => _Type;
        set
        {
            _Type = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Type;
    public PermissionDo Permission_Type
    {
        get => _Permission_Type;
        set
        {
            _Permission_Type = value;
            RaisePropertyChanged(value);
        }
    }

    public override void Clear()
    {
        Type = string.Empty;
        LastModified = string.Empty;
    }

    void Setup(VariatorDo variator)
    {
        Type = variator.Type;
        LastModified = variator.LastModified;
    }

    public override BaseDo GetDo() 
        => new PumpDo()
    {
        Id = objectId,
        Type = this.Type,
        LastModified = _appStateService.LoggedUser.Username,
    };

    public override Type GetModelType() => typeof(PumpDo);

    protected async override Task LoadObject()
    {
        Clear();

        var variator = await ExecuteApiRequest(_apiVariatorService.GetVariator, _appStateService.LoggedUser.Username, objectId);
        if (variator != null)
            Setup(variator);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var varia = GetDo() as VariatorDo;
        var newObject = await _apiVariatorService.CreateVariator(_appStateService.LoggedUser.Username, varia);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var variator = GetDo() as VariatorDo;
        var updated = await _apiVariatorService.UpdateVariator(_appStateService.LoggedUser.Username, variator);
        return updated != null;
    }
}

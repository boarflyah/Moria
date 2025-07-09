using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class ExternalCoolingDetailViewModel : BaseDetailViewModel
{
    readonly IApiExternalCoolingService _apiExternalCooling;

    public ExternalCoolingDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiExternalCoolingService apiExternalCooling) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiExternalCooling = apiExternalCooling;
    }

    #region properties

    decimal _Power;
    [ObjectChangedValidate]
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
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

    PermissionDo _Permission_Power;
    public PermissionDo Permission_Power
    {
        get => _Permission_Power;
        set
        {
            _Permission_Power = value;
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

    #endregion


    public override void Clear()
    {
        Power = 0;
        Type = string.Empty;
        LastModified = string.Empty;
    }
    void Setup(ExternalCoolingDo externalCoolingDo)
    {
        Type = externalCoolingDo.Type;
        Power = externalCoolingDo.Power;
        LastModified = externalCoolingDo.LastModified;
    }

    public override BaseDo GetDo() => new ExternalCoolingDo()
    {
        Id = objectId,
        Power = this.Power,
        Type = this.Type,
        LastModified = _appStateService.LoggedUser.Username,
    };

    public override Type GetModelType() => typeof(ExternalCoolingDo);

    protected async override Task LoadObject()
    {
        Clear();

        var externalCoolingDo = await ExecuteApiRequest(_apiExternalCooling.GetExternalCooling, _appStateService.LoggedUser.Username, objectId);
        if (externalCoolingDo != null)
            Setup(externalCoolingDo);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var externalCoolingDo = GetDo() as ExternalCoolingDo;
        var newObject = await _apiExternalCooling.CreateExternalCooling(_appStateService.LoggedUser.Username, externalCoolingDo);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var externalCoolingDo = GetDo() as ExternalCoolingDo;
        var updated = await _apiExternalCooling.UpdateExternalCooling(_appStateService.LoggedUser.Username, externalCoolingDo);
        return updated != null;
    }
}

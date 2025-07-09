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

public class InverterDetailViewModel : BaseDetailViewModel
{

    readonly IApiInverterService _apiInverter;
    public InverterDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiInverterService apiInverter) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiInverter = apiInverter;
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
        Type = string.Empty;
        Power = 0;
        LastModified = string.Empty;
    }

    void Setup(InverterDo inverter)
    {
        Type = inverter.Type;
        Power = inverter.Power;
        LastModified = inverter.LastModified;
    }

    public override BaseDo GetDo() 
        => new InverterDo()
    {
        Id = objectId,
        Type = this.Type,
        Power = this.Power,
        LastModified = _appStateService.LoggedUser.Username,
    };

    public override Type GetModelType() => typeof(InverterDo);

    protected async override Task LoadObject()
    {
        Clear();

        var inverter = await ExecuteApiRequest(_apiInverter.GetInverter, _appStateService.LoggedUser.Username, objectId);
        if (inverter != null)
            Setup(inverter);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var inv = GetDo() as InverterDo;
        var newObject = await _apiInverter.CreateInverter(_appStateService.LoggedUser.Username, inv);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var inverter = GetDo() as InverterDo;
        var updated = await _apiInverter.UpdateInverter(_appStateService.LoggedUser.Username, inverter);
        return updated != null;
    }
}

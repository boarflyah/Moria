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

public class BrakeDetailViewModel : BaseDetailViewModel
{
    readonly IApiBrakeService _brakeService;

    public BrakeDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiBrakeService apiBrakeService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _brakeService = apiBrakeService;
    }

    #region properties

    string _Kind;
    [ObjectChangedValidate]
    public string Kind
    {
        get => _Kind;
        set
        {
            _Kind = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Kind;
    public PermissionDo Permission_Kind
    {
        get => _Permission_Kind;
        set
        {
            _Permission_Kind = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    public override void Clear()
    {
        Kind = string.Empty;
        LastModified = string.Empty;
    }

    public override BaseDo GetDo() 
        => new BrakeDo()
    {
        Id = objectId,
        Kind = this.Kind,
        LastModified = _appStateService.LoggedUser.Username,
    };

    public override Type GetModelType() => typeof(BrakeDo);

    protected async override Task LoadObject()
    {
        Clear();

        var brake = await ExecuteApiRequest(_brakeService.GetBrake, _appStateService.LoggedUser.Username, objectId);
        if (brake != null)
            Setup(brake);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }
    void Setup(BrakeDo brake)
    {
        Kind = brake.Kind;
        LastModified = brake.LastModified;
    }

    protected async override Task<bool> SaveNewObject()
    {
        var brake = GetDo() as BrakeDo;
        var newObject = await _brakeService.CreateBrake(_appStateService.LoggedUser.Username, brake);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var brake = GetDo() as BrakeDo;
        var updated = await _brakeService.UpdateBrake(_appStateService.LoggedUser.Username, brake);
        return updated != null;
    }
}

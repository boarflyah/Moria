using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class MotorGearDetailViewModel : BaseDetailViewModel
{
    readonly IApiMotorGearService _motorGearService;
    public MotorGearDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiMotorGearService motorGearService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _motorGearService = motorGearService;
    }

    # region properties

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
    [DefaultProperty]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    string _Ratio;
    [ObjectChangedValidate]
    public string Ratio
    {
        get => _Ratio;
        set
        {
            _Ratio = value;
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

    PermissionDo _Permission_Ratio;
    public PermissionDo Permission_Ratio
    {
        get => _Permission_Ratio;
        set
        {
            _Permission_Ratio = value;
            RaisePropertyChanged(value);
        }
    }

    public override Type GetModelType() => typeof(MotorGearDo);

    protected async override Task LoadObject()
    {
        Clear();

        var motorGear = await ExecuteApiRequest(_motorGearService.GetMotorGear, _appStateService.LoggedUser.Username, objectId);
        if (motorGear != null)
            Setup(motorGear);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var motorgear = GetDo() as MotorGearDo;
        var newObject = await _motorGearService.CreateMotorGear(_appStateService.LoggedUser.Username, motorgear);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var motorGear = GetDo() as MotorGearDo;
        var updated = await _motorGearService.UpdateMotorGear(_appStateService.LoggedUser.Username, motorGear);
        return updated != null;
    }

    #endregion
    public override void Clear()
    {
        Symbol = string.Empty;
        Name = string.Empty;
        Ratio = string.Empty;
        LastModified = string.Empty;
    }
    void Setup(MotorGearDo motorGear)
    {
        Symbol = motorGear.Symbol;
        Name = motorGear.Name;
        Ratio = motorGear.Ratio;
        LastModified = motorGear.LastModified;
    }

    public override BaseDo GetDo()
        => new MotorGearDo()
        {
            Id = objectId,
            Name = this.Name,
            Ratio = this.Ratio,
            Symbol = this.Symbol,
            LastModified = _appStateService.LoggedUser.Username,
        };
}

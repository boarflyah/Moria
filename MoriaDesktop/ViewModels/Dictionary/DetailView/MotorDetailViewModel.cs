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
public class MotorDetailViewModel : BaseDetailViewModel
{
    readonly IApiMotorService _motorService;
    public MotorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiMotorService motorService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _motorService = motorService;
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


    public override Type GetModelType() => typeof(MotorDo);

    protected async override Task LoadObject()
    {
        Clear();

        var motor = await ExecuteApiRequest(_motorService.GetMotor, _appStateService.LoggedUser.Username, objectId);
        if (motor != null)
            Setup(motor);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var motor = GetDo() as MotorDo;
        var newObject = await _motorService.CreateMotor(_appStateService.LoggedUser.Username, motor);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var motor = GetDo() as MotorDo;
        var updated = await _motorService.UpdateMotor(_appStateService.LoggedUser.Username, motor);
        return updated != null;
    }

    #endregion

    public override void Clear()
    {
        Symbol = string.Empty;
        Name = string.Empty;
        Power = 0;
        LastModified = string.Empty;
    }
    void Setup(MotorDo motor)
    {
        Symbol = motor.Symbol;
        Name = motor.Name;
        Power = motor.Power;
        LastModified = motor.LastModified;
    }

    public override BaseDo GetDo()
        => new MotorDo()
        {
            Name = this.Name,
            Power = this.Power,
            Symbol = this.Symbol,
            LastModified = _appStateService.LoggedUser.Username,
        };
}

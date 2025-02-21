using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;

namespace MoriaDesktop.ViewModels.DriveComponents;
public class DriveDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiDriveService _apiService;

    public DriveDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IApiDriveService apiService) 
        : base(logger, appStateService, apiLockService, navigationService)
    {
        _apiService = apiService;

        Title = "Napęd";
    }

    #region Properties

    MotorDo _Motor;
    [ObjectChangedValidate]
    public MotorDo Motor
    {
        get => _Motor;
        set
        {
            _Motor = value;
            RaisePropertyChanged(value);
        }
    }

    bool _Variator;
    [ObjectChangedValidate]
    public bool Variator
    {
        get => _Variator;
        set
        {
            _Variator = value;
            RaisePropertyChanged(value);
        }
    }

    bool _Inverter;
    [ObjectChangedValidate]
    public bool Inverter
    {
        get => _Inverter;
        set
        {
            _Inverter = value;
            RaisePropertyChanged(value);
        }
    }

    byte _Quantity;
    [ObjectChangedValidate]
    public byte Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }


    PermissionDo _Permission_Motor;
    public PermissionDo Permission_Motor
    {
        get => _Permission_Motor;
        set
        {
            _Permission_Motor = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Variator;
    public PermissionDo Permission_Variator
    {
        get => _Permission_Variator;
        set
        {
            _Permission_Variator = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Inverter;
    public PermissionDo Permission_Inverter
    {
        get => _Permission_Inverter;
        set
        {
            _Permission_Inverter = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Quantity;
    public PermissionDo Permission_Quantity
    {
        get => _Permission_Quantity;
        set
        {
            _Permission_Quantity = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region Nestedlistview methods

    protected override string GetObjectsListViewTitle() => "Przekładnie";

    protected async override Task NestedNew()
    {
        Objects.Add(new MotorGearToDriveDo()
        {
            ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added
        });

        HasObjectChanged = true;
    }

    #endregion

    #region Methods

    public override Type GetModelType() => typeof(DriveDo);
    protected async override Task LoadObject()
    {
        Clear();

        var drive = await ExecuteApiRequest(_apiService.GetDrive, _appStateService.LoggedUser.Username, objectId);
        if (drive != null)
            Setup(drive);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected async override Task<bool> SaveNewObject()
    {
        var drive = GetDo() as DriveDo;
        var newObject = await _apiService.CreateDrive(_appStateService.LoggedUser.Username, drive);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var product = GetDo() as DriveDo;
        var updated = await _apiService.UpdateDrive(_appStateService.LoggedUser.Username, product);
        return updated != null;
    }

    public override BaseDo GetDo()
    {
        var result = new DriveDo()
        {
            Id = objectId,
            Inverter = Inverter,
            Variator = Variator,
            Quantity = Quantity,
            Motor = Motor,
            LastModified = _appStateService.LoggedUser.Username,
        };

        foreach (var gearbox in Objects.Where(x => x.ChangeType != MoriaModelsDo.Base.Enums.SystemChangeType.None).OfType<MotorGearToDriveDo>())
            result.Gearboxes = result.Gearboxes.Append(gearbox);

        return result;
    }
    public override void Clear()
    {
        Inverter = default;
        Variator = default;
        Quantity = default;
        Motor = null;
        Objects?.Clear();
    }

    void Setup(DriveDo drive)
    {
        Inverter = drive.Inverter;
        Motor = drive.Motor;
        Quantity = drive.Quantity;
        Variator = drive.Variator;
        if (drive.Gearboxes != null && drive.Gearboxes.Any())
            foreach (var gearbox in drive.Gearboxes)
                Objects.Add(gearbox);
    }

    #endregion
}

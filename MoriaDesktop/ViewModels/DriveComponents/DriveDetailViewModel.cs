using System.Text;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
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

    public DriveDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IApiDriveService apiService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiService = apiService;

        Title = "Napęd";
    }

    #region Properties


    private string _Name;
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

    MotorDo _Motor;
    [ObjectChangedValidate]
    public MotorDo Motor
    {
        get => _Motor;
        set
        {
            _Motor = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    VariatorDo _Variator;
    [ObjectChangedValidate]
    public VariatorDo Variator
    {
        get => _Variator;
        set
        {
            _Variator = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    InverterDo _Inverter;
    [ObjectChangedValidate]
    public InverterDo Inverter
    {
        get => _Inverter;
        set
        {
            _Inverter = value;
            RaisePropertyChanged(value);
            SetDriveName();
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
            SetDriveName();
        }
    }


    private PumpDo _Pump;
    [ObjectChangedValidate]
    public PumpDo Pump
    {
        get => _Pump;
        set
        {
            _Pump = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    private ExternalCoolingDo _ExternalCooling;
    [ObjectChangedValidate]
    public ExternalCoolingDo ExternalCooling
    {
        get => _ExternalCooling;
        set
        {
            _ExternalCooling = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    private BrakeDo _Brake;
    [ObjectChangedValidate]
    public BrakeDo Brake
    {
        get => _Brake;
        set
        {
            _Brake = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    private SupplementDo _Supplement;
    [ObjectChangedValidate]
    public SupplementDo Supplement
    {
        get => _Supplement;
        set
        {
            _Supplement = value;
            RaisePropertyChanged(value);
            SetDriveName();
        }
    }

    PermissionDo _Permission_Supplement;
    public PermissionDo Permission_Supplement
    {
        get => _Permission_Supplement;
        set
        {
            _Permission_Supplement = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Brake;
    public PermissionDo Permission_Brake
    {
        get => _Permission_Brake;
        set
        {
            _Permission_Brake = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_ExternalCooling;
    public PermissionDo Permission_ExternalCooling
    {
        get => _Permission_ExternalCooling;
        set
        {
            _Permission_ExternalCooling = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Pump;
    public PermissionDo Permission_Pump
    {
        get => _Permission_Pump;
        set
        {
            _Permission_Pump = value;
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

    private PermissionDo _Permission_Name;
    public PermissionDo Permission_Name
    {
        get => _Permission_Name;
        set
        {
            _Permission_Name = value;
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

        HasObjectChanged = false;

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
            Pump = Pump,
            Supplement = Supplement,
            ExternalCooling = ExternalCooling,
            Brake = Brake,
            Inverter = Inverter,
            Variator = Variator,
            Quantity = Quantity,
            Motor = Motor,
            Name = Name,
            LastModified = _appStateService.LoggedUser.Username,
        };

        foreach (var gearbox in Objects.Where(x => x.ChangeType != MoriaModelsDo.Base.Enums.SystemChangeType.None).OfType<MotorGearToDriveDo>())
        {
            gearbox.LastModified = _appStateService.LoggedUser.Username;
            result.Gearboxes = result.Gearboxes.Append(gearbox);
        }

        return result;
    }
    public override void Clear()
    {
        Inverter = default;
        Variator = default;
        Pump = default;
        ExternalCooling = default;
        Supplement = default;
        Brake = default;
        Quantity = default;
        Name = string.Empty;
        Motor = null;
        LastModified = string.Empty;
        Objects?.Clear();
    }

    void Setup(DriveDo drive)
    {
        Inverter = drive.Inverter;
        Motor = drive.Motor;
        Quantity = drive.Quantity;
        Variator = drive.Variator;
        Pump = drive.Pump;
        Supplement = drive.Supplement;
        ExternalCooling = drive.ExternalCooling;
        Brake = drive.Brake;
        Name = drive.Name;
        LastModified = drive.LastModified;
        if (drive.Gearboxes != null && drive.Gearboxes.Any())
            foreach (var gearbox in drive.Gearboxes)
                Objects.Add(gearbox);
    }

    void SetDriveName()
    {
        StringBuilder builder = new();
        if (Pump != null)
            builder.Append($"{Pump?.Type ?? string.Empty} {Pump?.Size ?? string.Empty} {Pump?.IProperty ?? string.Empty}");

        if (Pump != null && Supplement != null)
            builder.Append(" | ");

        if (Supplement != null)
            builder.Append($"{Supplement.Type} {Supplement.Size} {Supplement.IProperty}");

        if ((Pump != null || Supplement != null) && Variator != null)
            builder.Append(" | ");

        if (Variator != null)
            builder.Append($"{Variator.Type}");

        if ((Pump != null || Supplement != null || Variator != null) && Motor != null)
            builder.Append(" | ");

        if (Motor != null)
            builder.Append($"{Motor.Name} {Motor.Power}kW ({Motor.RPM})");

        if ((Pump != null || Supplement != null || Variator != null || Motor != null) && ExternalCooling != null)
            builder.Append(" | ");

        if (ExternalCooling != null)
            builder.Append($"{ExternalCooling.Type} {ExternalCooling.Power}kW");

        if((Pump != null || Supplement != null || Variator != null || Motor != null || ExternalCooling != null) && Brake != null)
            builder.Append(" | ");

        if (Brake != null)
            builder.Append($"{Brake.Kind}");

        if (Pump != null || Supplement != null || Variator != null || Motor != null || ExternalCooling != null || Brake != null)
            builder.Append(" • ");

        builder.Append($"[{Quantity}szt]");

        if (!builder.ToString().Equals(Name))
            Name = builder.ToString();

    }

    #endregion
}

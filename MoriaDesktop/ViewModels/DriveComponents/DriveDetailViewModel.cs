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
    public DriveDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService) 
        : base(logger, appStateService, apiLockService, navigationService)
    {
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
    protected override Task LoadObject() => throw new NotImplementedException();
    protected override Task<bool> SaveNewObject() => throw new NotImplementedException();
    protected override Task<bool> UpdateExistingObject() => throw new NotImplementedException();
    public override BaseDo GetDo()
    {
        var result = new DriveDo()
        {
            Inverter = Inverter,
            Variator = Variator,
            Quantity = Quantity,
            Motor = Motor,
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

using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.Views.Dictionary;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class MotorGearDetailViewModel : BaseDetailViewModel
{
    readonly IApiMotorGearService _motorGearService;
    public MotorGearDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiMotorGearService motorGearService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _motorGearService = motorGearService;
    }

    # region properties

    string _Symbol;
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
    public string Ratio
    {
        get => _Ratio;
        set
        {
            _Ratio = value;
            RaisePropertyChanged(value);
        }
    }

    protected override Type GetModelType() => typeof(MotorGearDo);

    protected async override Task LoadObject()
    {
        Clear();

        var motorGear = await ExecuteApiRequest(_motorGearService.GetMotorGear, _appStateService.LoggedUser.Username, objectId);
        if (motorGear != null)
            Setup(motorGear);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;

    protected override bool CheckPropertyName(string propertyName) =>
        propertyName.Equals(nameof(Symbol)) || propertyName.Equals(nameof(Name)) || propertyName.Equals(nameof(Ratio));

    #endregion
    void Clear()
    {
        Symbol = string.Empty;
        Name = string.Empty;
        Ratio = string.Empty;
    }
    void Setup(MotorGearDo motorGear)
    {
        Symbol = motorGear.Symbol;
        Name = motorGear.Name;
        Ratio = motorGear.Ratio;
    }
}

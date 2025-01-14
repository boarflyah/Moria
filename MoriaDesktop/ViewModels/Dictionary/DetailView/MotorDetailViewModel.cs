using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.DriveComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class MotorDetailViewModel : BaseDetailViewModel
{
    readonly IApiMotorService _motorService;
    public MotorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiMotorService motorService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _motorService = motorService;
    }

    #region properties

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

    decimal _Power;
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
    }


    protected override Type GetModelType() => typeof(MotorDo);

    protected async override Task LoadObject()
    {
        Clear();

        var motor = await ExecuteApiRequest(_motorService.GetMotor, _appStateService.LoggedUser.Username, objectId);
        if (motor != null)
            Setup(motor);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;
    protected override bool CheckPropertyName(string propertyName) =>
        propertyName.Equals(nameof(Symbol)) || propertyName.Equals(nameof(Name)) || propertyName.Equals(nameof(Power));

    #endregion

    void Clear()
    {
        Symbol = string.Empty;
        Name = string.Empty;
        Power = 0;
    }
    void Setup(MotorDo motor)
    {
        Symbol = motor.Symbol;
        Name = motor.Name;
        Power = motor.Power;
    }
}

using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class MotorListViewModel : BaseListViewModel
{
    readonly IApiMotorService _motorService;
    public MotorListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiMotorService apiMotorService) : base(logger, appStateService, navigationService)
    {
        _motorService = apiMotorService;

        Motors = new();
        Title = "Silniki";
    }

    #region properties

    public ObservableCollection<MotorDo> Motors { get; set; }

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

    #endregion

    protected async override Task LoadList()
    {
        Motors.Clear();

        var motors = await ExecuteApiRequest(_motorService.GetMotors, _appStateService.LoggedUser.Username);
        if (motors != null)
        {
            foreach (var motor in motors)
                Motors.Add(motor);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is MotorDo mdo)
            _navigationService.NavigateTo(typeof(MotorDetailViewModel), false, mdo.Id);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(MotorDetailViewModel), false, null);


    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_motorService.DeleteMotor, _appStateService.LoggedUser.Username, (Selected as MotorDo)?.Id ?? 0);
}


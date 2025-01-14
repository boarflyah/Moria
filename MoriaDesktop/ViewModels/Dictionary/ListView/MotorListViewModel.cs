using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
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


using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.DriveComponents;
public sealed class DriveListViewModel : BaseListViewModel
{
    readonly IApiDriveService _apiService;

    public DriveListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiDriveService apiService) : base(logger, appStateService, navigationService)
    {
        _apiService = apiService;

        Drives = new();
    }

    #region properties

    public ObservableCollection<DriveDo> Drives
    {
        get; set;
    }

    #endregion

    #region methods

    public override void OnRowSelected(object row)
    {
        if (row is DriveDo ddo)
            _navigationService.NavigateTo(typeof(DriveDetailViewModel), false, ddo.Id);
    }

    protected async override Task LoadList()
    {
        Drives.Clear();

        var drives = await ExecuteApiRequest(_apiService.GetDrives, _appStateService.LoggedUser.Username);
        if (drives != null && drives.Any())
        {
            foreach (var drive in drives)
                Drives.Add(drive);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }
    protected override void New()
        => _navigationService.NavigateTo(typeof(DriveDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest()
        => await ExecuteApiRequest(_apiService.DeleteDrive, _appStateService.LoggedUser.Username, (Selected as BaseDo)?.Id ?? 0);

    #endregion
}

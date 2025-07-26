using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.DriveComponents;
public sealed class DriveListViewModel : BaseListViewModel
{
    readonly IApiDriveService _apiService;

    public DriveListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiDriveService apiService, IApiListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiService = apiService;
        Title = "Napędy";
        Drives = new();
    }

    #region properties

    public ObservableCollection<DriveDo> Drives
    {
        get; set;
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

    private PermissionDo _Permission_Pump;
    public PermissionDo Permission_Pump
    {
        get => _Permission_Pump;
        set
        {
            _Permission_Pump = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_Supplement;
    public PermissionDo Permission_Supplement
    {
        get => _Permission_Supplement;
        set
        {
            _Permission_Supplement = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_ExternalCooling;
    public PermissionDo Permission_ExternalCooling
    {
        get => _Permission_ExternalCooling;
        set
        {
            _Permission_ExternalCooling = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_Brake;
    public PermissionDo Permission_Brake
    {
        get => _Permission_Brake;
        set
        {
            _Permission_Brake = value;
            RaisePropertyChanged(value);
        }
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

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<DriveDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Drives.Clear();
                    foreach (var item in result)
                    {
                        Drives.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, $"Brak danych do wczytania. {ex.Message}", true);
            }
        }
        else
            await LoadList();
    }

    #endregion
}

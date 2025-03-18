using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class MotorGearListViewModel : BaseListViewModel
{
    readonly IApiMotorGearService _motorGearService;
    public MotorGearListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiMotorGearService apiMotorGearService, IListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _motorGearService = apiMotorGearService;

        MotorGears = new();
        Title = "Przekładnie";
    }

    #region properties

    public ObservableCollection<MotorGearDo> MotorGears { get; set; }

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

    PermissionDo _Permission_Ratio;
    public PermissionDo Permission_Ratio
    {
        get => _Permission_Ratio;
        set
        {
            _Permission_Ratio = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    protected async override Task LoadList()
    {
        MotorGears.Clear();

        var motorGears = await ExecuteApiRequest(_motorGearService.GetMotorGears, _appStateService.LoggedUser.Username);
        if (motorGears != null)
        {
            foreach (var motorGear in motorGears)
                MotorGears.Add(motorGear);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is MotorGearDo mdo)
            _navigationService.NavigateTo(typeof(MotorGearDetailViewModel), false, mdo.Id);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(MotorGearDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_motorGearService.DeleteMotorGear, _appStateService.LoggedUser.Username, (Selected as MotorGearDo)?.Id ?? 0);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<MotorGearDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    MotorGears.Clear();
                    foreach (var item in result)
                    {
                        MotorGears.Add(item);
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
}

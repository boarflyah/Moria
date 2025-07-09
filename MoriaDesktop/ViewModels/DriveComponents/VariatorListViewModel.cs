using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class VariatorListViewModel : BaseListViewModel
{
    readonly IApiVariatorService _apiVariatorService;

    public VariatorListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiVariatorService apiVariatorService) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiVariatorService = apiVariatorService;
        Variators = new();
        Title = "Wariatory";
    }

    public ObservableCollection<VariatorDo> Variators { get; set; }


    PermissionDo _Permission_Type;
    public PermissionDo Permission_Type
    {
        get => _Permission_Type;
        set
        {
            _Permission_Type = value;
            RaisePropertyChanged(value);
        }
    }

    public override void OnRowSelected(object row)
    {
        if (row is VariatorDo cdo)
            _navigationService.NavigateTo(typeof(VariatorDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        Variators.Clear();

        var variators = await ExecuteApiRequest(_apiVariatorService.GetVariators, _appStateService.LoggedUser.Username);
        if (variators != null)
        {
            foreach (var inv in variators)
                Variators.Add(inv);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(VariatorDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<VariatorDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Variators.Clear();
                    foreach (var item in result)
                    {
                        Variators.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_apiVariatorService.DeleteVariator, _appStateService.LoggedUser.Username, (Selected as VariatorDo)?.Id ?? 0);
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class InverterListViewModel : BaseListViewModel
{
    readonly IApiInverterService _apiInverter;

    public InverterListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiInverterService apiInverter) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiInverter = apiInverter;
        Inverters = new();
        Title = "Falowniki";
    }

    public ObservableCollection<InverterDo> Inverters { get; set; }

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
        if (row is InverterDo cdo)
            _navigationService.NavigateTo(typeof(InverterDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        Inverters.Clear();

        var inverters = await ExecuteApiRequest(_apiInverter.GetInverters, _appStateService.LoggedUser.Username);
        if (inverters != null)
        {
            foreach (var inv in inverters)
                Inverters.Add(inv);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(InverterDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<InverterDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Inverters.Clear();
                    foreach (var item in result)
                    {
                        Inverters.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_apiInverter.DeleteInverter, _appStateService.LoggedUser.Username, (Selected as InverterDo)?.Id ?? 0);
}

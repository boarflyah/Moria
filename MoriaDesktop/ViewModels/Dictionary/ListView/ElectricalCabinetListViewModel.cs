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
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public class ElectricalCabinetListViewModel : BaseListViewModel
{
    readonly IApiElectricalCabinetService _cabinetService;

    public ElectricalCabinetListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiElectricalCabinetService apiElectricalCabinetService) : base(logger, appStateService, navigationService, listViewService)
    {
        _cabinetService = apiElectricalCabinetService;

        ElectricalCabinets = new();
        Title = "Szafy elektryczne";
    }

    #region properties

    public ObservableCollection<ElectricalCabinetDo> ElectricalCabinets { get; set; }

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

    #endregion

    public override void OnRowSelected(object row)
    {
        if (row is ElectricalCabinetDo cdo)
            _navigationService.NavigateTo(typeof(ElectricalCabinetDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        ElectricalCabinets.Clear();

        var cabinets = await ExecuteApiRequest(_cabinetService.GetElectricalCabinets, _appStateService.LoggedUser.Username);
        if (cabinets != null)
        {
            foreach (var cabinet in cabinets)
                ElectricalCabinets.Add(cabinet);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected override void New() => _navigationService.NavigateTo(typeof(ElectricalCabinetDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<ElectricalCabinetDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    ElectricalCabinets.Clear();
                    foreach (var item in result)
                    {
                        ElectricalCabinets.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_cabinetService.DeleteElectricalCabinet, _appStateService.LoggedUser.Username, (Selected as ElectricalCabinetDo)?.Id ?? 0);
}

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
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class PumpListViewModel : BaseListViewModel
{
    readonly IApiPumpService _apiPumpService;
    public PumpListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiPumpService apiPumpService) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiPumpService = apiPumpService;
        Pumps = new();
        Title = "Pompy";
    }

    public ObservableCollection<PumpDo> Pumps { get; set; }

    PermissionDo _Permission_Size;
    public PermissionDo Permission_Size
    {
        get => _Permission_Size;
        set
        {
            _Permission_Size = value;
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

    PermissionDo _Permission_IProperty;
    public PermissionDo Permission_IProperty
    {
        get => _Permission_IProperty;
        set
        {
            _Permission_IProperty = value;
            RaisePropertyChanged(value);
        }
    }

    public override void OnRowSelected(object row)
    {
        if (row is PumpDo cdo)
            _navigationService.NavigateTo(typeof(PumpDetailViewModel), false, cdo.Id);
    }
    

    protected async override Task LoadList()
    {
        Pumps.Clear();

        var pumps = await ExecuteApiRequest(_apiPumpService.GetPumps, _appStateService.LoggedUser.Username);
        if (pumps != null)
        {
            foreach (var pump in pumps)
                Pumps.Add(pump);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected override void New() => _navigationService.NavigateTo(typeof(PumpDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<PumpDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Pumps.Clear();
                    foreach (var item in result)
                    {
                        Pumps.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_apiPumpService.DeletePump, _appStateService.LoggedUser.Username, (Selected as PumpDo)?.Id ?? 0);
}

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

public class BrakeListViewModel : BaseListViewModel
{
    readonly IApiBrakeService _brakeService;

    public BrakeListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiBrakeService apiBrakeService) : base(logger, appStateService, navigationService, listViewService)
    {
        _brakeService = apiBrakeService;
        Brakes = new();
        Title = "Hamulce";
    }

    public ObservableCollection<BrakeDo> Brakes { get; set; }

    PermissionDo _Permission_Kind;
    public PermissionDo Permission_Kind
    {
        get => _Permission_Kind;
        set
        {
            _Permission_Kind = value;
            RaisePropertyChanged(value);
        }
    }

    public override void OnRowSelected(object row)
    {
        if (row is BrakeDo cdo)
            _navigationService.NavigateTo(typeof(BrakeDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        Brakes.Clear();

        var brakes = await ExecuteApiRequest(_brakeService.GetBrakes, _appStateService.LoggedUser.Username);
        if (brakes != null)
        {
            foreach (var color in brakes)
                Brakes.Add(color);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(BrakeDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<BrakeDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Brakes.Clear();
                    foreach (var item in result)
                    {
                        Brakes.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_brakeService.DeleteBrake, _appStateService.LoggedUser.Username, (Selected as BrakeDo)?.Id ?? 0);
}

using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class SteelKindListViewModel : BaseListViewModel
{
    readonly IApiSteelKindService _steelKindService;
    public SteelKindListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiSteelKindService apiSteelKindService) : base(logger, appStateService, navigationService)
    {
        _steelKindService = apiSteelKindService;

        SteelKinds = new();
        Title = "Rodzaje stali";
    }

    #region properties

    public ObservableCollection<SteelKindDo> SteelKinds { get; set; }

    #endregion

    protected async override Task LoadList()
    {
        SteelKinds.Clear();

        var steelKinds = await ExecuteApiRequest(_steelKindService.GetSteelKinds, _appStateService.LoggedUser.Username);
        if (steelKinds != null)
        {
            foreach (var steelKind in steelKinds)
                SteelKinds.Add(steelKind);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is SteelKindDo sdo)
            _navigationService.NavigateTo(typeof(SteelKindDetailViewModel), false, sdo.Id);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(SteelKindDetailViewModel), false, null);
    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_steelKindService.DeleteSteelKind, _appStateService.LoggedUser.Username, (Selected as SteelKindDo)?.Id ?? 0);
}

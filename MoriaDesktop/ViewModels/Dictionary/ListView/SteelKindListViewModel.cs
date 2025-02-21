using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
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

        //var symbol = _appStateService.LoggedUser.Position.Permissions.FirstOrDefault(x => x.PropertyName.Equals("SteelKind_Symbol"));
        //if (symbol != null)
        //    Permission_Symbol = symbol;
    }

    #region properties

    public ObservableCollection<SteelKindDo> SteelKinds { get; set; }

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

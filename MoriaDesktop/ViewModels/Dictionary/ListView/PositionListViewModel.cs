using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class PositionListViewModel : BaseListViewModel
{
    readonly IApiPositionService _positionService;
    public PositionListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiPositionService apiPositionService) : base(logger, appStateService, navigationService)
    {
        _positionService = apiPositionService;

        Positions = new();
        Title = "Role pracowników";
    }

    #region properties

    public ObservableCollection<PositionDo> Positions { get; set; }

    #endregion

    protected async override Task LoadList()
    {
        Positions.Clear();

        var positions = await ExecuteApiRequest(_positionService.GetPositions, _appStateService.LoggedUser.Username);
        if (positions != null)
        {
            foreach (var position in positions)
                Positions.Add(position);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is PositionDo pdo)
            _navigationService.NavigateTo(typeof(PositionDetailViewModel), false, pdo.Id);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(PositionDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_positionService.DeletePosition, _appStateService.LoggedUser.Username, (Selected as PositionDo)?.Id ?? 0);
}

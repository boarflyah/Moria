using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class PositionListViewModel : BaseListViewModel
{
    readonly IApiPositionService _positionService;
    public PositionListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiPositionService apiPositionService, IApiListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _positionService = apiPositionService;

        Positions = new();
        Title = "Role pracowników";
    }

    #region properties

    public ObservableCollection<PositionDo> Positions { get; set; }

    PermissionDo _Permission_Code;
    public PermissionDo Permission_Code
    {
        get => _Permission_Code;
        set
        {
            _Permission_Code = value;
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

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<PositionDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Positions.Clear();
                    foreach (var item in result)
                    {
                        Positions.Add(item);
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

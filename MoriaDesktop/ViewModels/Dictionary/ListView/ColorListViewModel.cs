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
using System.Windows.Media;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class ColorListViewModel : BaseListViewModel
{
    readonly IApiColorService _colorService;
    public ColorListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiColorService apiColorService, IListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _colorService = apiColorService;

        Colors = new();
        Title = "Kolory";
    }

    #region properties

    public ObservableCollection<ColorDo> Colors { get; set; }

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
        Colors.Clear();

        var colors = await ExecuteApiRequest(_colorService.GetColors, _appStateService.LoggedUser.Username);
        if (colors != null)
        {
            foreach (var color in colors)
                Colors.Add(color);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    public override void OnRowSelected(object row)
    {
        if (row is ColorDo cdo)
            _navigationService.NavigateTo(typeof(ColorDetailViewModel), false, cdo.Id);
    }

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<ColorDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Colors.Clear();
                    foreach (var item in result)
                    {
                        Colors.Add(item);
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

    protected override void New() => _navigationService.NavigateTo(typeof(ColorDetailViewModel), false, null);
    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_colorService.DeleteColor, _appStateService.LoggedUser.Username, (Selected as ColorDo)?.Id ?? 0);
}

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

public class ExternalCoolingListViewModel : BaseListViewModel
{
    readonly IApiExternalCoolingService _externalCoolingService;

    public ExternalCoolingListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiExternalCoolingService externalCoolingService) : base(logger, appStateService, navigationService, listViewService)
    {
        _externalCoolingService = externalCoolingService;
        ExternalCoolings = new();
        Title = "Chłodzenia zewnętrzne";
    }

    public ObservableCollection<ExternalCoolingDo> ExternalCoolings { get; set; }


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
        if (row is ExternalCoolingDo cdo)
            _navigationService.NavigateTo(typeof(ExternalCoolingDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        ExternalCoolings.Clear();

        var result = await ExecuteApiRequest(_externalCoolingService.GetExternalCoolings, _appStateService.LoggedUser.Username);
        if (result != null)
        {
            foreach (var external in result)
                ExternalCoolings.Add(external);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(ExternalCoolingDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<ExternalCoolingDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    ExternalCoolings.Clear();
                    foreach (var item in result)
                    {
                        ExternalCoolings.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_externalCoolingService.DeleteExternalCooling, _appStateService.LoggedUser.Username, (Selected as ExternalCoolingDo)?.Id ?? 0);
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;

public class SupplementListViewModel : BaseListViewModel
{
    readonly IApiSupplementService _apiSupplementService;
    public SupplementListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiSupplementService apiSupplementService) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiSupplementService = apiSupplementService;
        Supplements = new();
        Title = "Wyposażenie dodatkowe";
    }
    public ObservableCollection<SupplementDo> Supplements { get; set; }

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
            _navigationService.NavigateTo(typeof(SupplementDetailViewModel), false, cdo.Id);
    }

    protected async override Task LoadList()
    {
        Supplements.Clear();

        var supplements = await ExecuteApiRequest(_apiSupplementService.GetSupplements, _appStateService.LoggedUser.Username);
        if (supplements != null)
        {
            foreach (var supp in supplements)
                Supplements.Add(supp);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New() => _navigationService.NavigateTo(typeof(SupplementDetailViewModel), false, null);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<SupplementDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Supplements.Clear();
                    foreach (var item in result)
                    {
                        Supplements.Add(item);
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

    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_apiSupplementService.DeleteSupplement, _appStateService.LoggedUser.Username, (Selected as SupplementDo)?.Id ?? 0);
}

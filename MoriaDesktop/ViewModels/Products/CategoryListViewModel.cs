using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
public class CategoryListViewModel : BaseListViewModel
{
    readonly IApiCategoryService _apiService;
    public CategoryListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiCategoryService apiService, IListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _apiService = apiService;

        Title = "Kategorie";
        Categories = new();
    }

    #region properties

    public ObservableCollection<CategoryDo> Categories
    {
        get; set;
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

    #region methods

    protected async override Task LoadList()
    {
        Categories.Clear();

        var categories = await ExecuteApiRequest(_apiService.GetCategories, _appStateService.LoggedUser.Username);
        if (categories != null && categories.Any())
        {
            foreach (var product in categories)
                Categories.Add(product);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is CategoryDo cdo)
            _navigationService.NavigateTo(typeof(CategoryDetailViewModel), false, cdo.Id);
    }

    protected override void New()
        => _navigationService.NavigateTo(typeof(CategoryDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest()
        => await ExecuteApiRequest(_apiService.DeleteCategory, _appStateService.LoggedUser.Username, (Selected as CategoryDo)?.Id ?? 0);

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<CategoryDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Categories.Clear();
                    foreach (var item in result)
                    {
                        Categories.Add(item);
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

    #endregion
}
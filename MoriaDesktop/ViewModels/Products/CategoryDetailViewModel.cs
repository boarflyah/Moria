using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
public class CategoryDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiCategoryService _apiService;

    public CategoryDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IApiCategoryService apiService, IKeepAliveWorker keepAliveWorker)
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiService = apiService;

        Title = "Nowa kategoria";
    }

    #region properties


    private string _Name;
    [ObjectChangedValidate]
    [DefaultProperty]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
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

    #region Commands methods

    protected override bool CanNestedDelete() => false;

    protected override bool CanNestedNew() => false;

    #endregion

    #region methods

    protected override string GetObjectsListViewTitle() => "Produkty";

    public override Type GetModelType() => typeof(CategoryDo);

    protected async override Task LoadObject()
    {
        Clear();

        var employee = await ExecuteApiRequest(_apiService.GetCategory, _appStateService.LoggedUser.Username, objectId);
        if (employee != null)
            Setup(employee);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected async override Task<bool> SaveNewObject()
    {
        var category = GetDo() as CategoryDo;
        var newObject = await _apiService.CreateCategory(_appStateService.LoggedUser.Username, category);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var category = GetDo() as CategoryDo;
        var updated = await _apiService.UpdateCategory(_appStateService.LoggedUser.Username, category);
        return updated != null;
    }

    public override void Clear()
    {
        Name = string.Empty;
        LastModified = string.Empty;
        if (Objects != null)
            Objects.Clear();
    }

    public override BaseDo GetDo()
    {
        return new CategoryDo()
        {
            Name = this.Name,
            LastModified = _appStateService.LoggedUser.Username,
            Id = objectId
        };
    }

    void Setup(CategoryDo category)
    {
        Name = category.Name;
        LastModified = category.LastModified;
        if (category.Products != null)
            foreach (var product in category.Products)
                Objects.Add(product);
    }

    #endregion
}

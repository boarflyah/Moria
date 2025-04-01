using System.Reflection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Attributes;
using MoriaDesktop.Models;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;

public class ComponentDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiComponentService _apiService;
    public ComponentDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IApiComponentService apiService, IKeepAliveWorker keepAliveWorker)
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _apiService = apiService;
    }

    #region properties

    ProductDo product;
    ComponentDo currentComponent;

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

    private ProductDo _ComponentProduct;
    [ObjectChangedValidate]
    public ProductDo ComponentProduct
    {
        get => _ComponentProduct;
        set
        {
            _ComponentProduct = value;
            RaisePropertyChanged(value);
        }
    }


    private double _Quantity;
    [ObjectChangedValidate]
    public double Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }


    private ColorDo _ComponentColor;
    [ObjectChangedValidate]
    public ColorDo ComponentColor
    {
        get => _ComponentColor;
        set
        {
            _ComponentColor = value;
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

    PermissionDo _Permission_ComponentProduct;
    public PermissionDo Permission_ComponentProduct
    {
        get => _Permission_ComponentProduct;
        set
        {
            _Permission_ComponentProduct = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_ComponentColor;
    public PermissionDo Permission_ComponentColor
    {
        get => _Permission_ComponentColor;
        set
        {
            _Permission_ComponentColor = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Quantity;
    public PermissionDo Permission_Quantity
    {
        get => _Permission_Quantity;
        set
        {
            _Permission_Quantity = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region nested listview

    protected override string GetObjectsListViewTitle() => "Napędy";

    protected async override Task NestedNew()
    {
        var dtcd = new DriveToComponentDo()
        {
            ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added
        };
        Objects.Add(dtcd);
        HasObjectChanged = true;
    }

    #endregion

    #region methods

    public override Type GetModelType() => typeof(ComponentDo);

    public override void Clear()
    {
        Objects?.Clear();
        Selected = null;
        ComponentColor = null;
        ComponentProduct = null;
        Quantity = 0;
        Name = string.Empty;
    }

    public override BaseDo GetDo()
    {
        currentComponent.LastModified = _appStateService.LoggedUser.Username;
        currentComponent.ComponentColor = ComponentColor;
        currentComponent.ComponentProduct = ComponentProduct;
        currentComponent.Quantity = Quantity;
        currentComponent.Name = Name;

        if (currentComponent.Drives == null)
            currentComponent.Drives = new List<DriveToComponentDo>();
        foreach (var dtc in Objects.OfType<DriveToComponentDo>())
            if (!currentComponent.Drives.Any(x => x == dtc))
                currentComponent.Drives.Add(dtc);

        return currentComponent;
    }

    void Setup(ComponentDo component)
    {
        ComponentColor = component.ComponentColor;
        ComponentProduct = component.ComponentProduct;
        Quantity = component.Quantity;
        Name = component.Name;
        LastModified = component.LastModified;
        if (component.Drives != null)
            foreach (var drive in component.Drives)
                Objects.Add(drive);
    }

    protected async override Task LoadObject()
    {
    }

    public async override Task Load()
    {
        PropertyChanged -= BaseDetailViewModel_PropertyChanged;
        try
        {
            Clear();
            Setup(currentComponent);

            var property = GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<DefaultPropertyAttribute>() != null);
            if (property == null)
                property = GetType().GetProperties().FirstOrDefault();

            _appStateService.SetupTitle(property?.GetValue(this)?.ToString() ?? "");
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            PropertyChanged += BaseDetailViewModel_PropertyChanged;
        }
    }

    protected async override Task<bool> SaveNewObject()
    {
        return false;
    }

    protected async override Task Save()
    {
        GetDo();
        _navigationService.NavigateTo(typeof(ProductDetailViewModel), true, null);
        var message = WeakReferenceMessenger.Default.Send(new NavigationMessage<ProductDo>(product));
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        return false;
    }

    public override void OnNavigatedTo(params object[] parameters)
    {
        if (parameters.Length > 2)
        {
            currentComponent = parameters.FirstOrDefault(x => x is ComponentDo) as ComponentDo;
            if (currentComponent?.Id > 0)
            {
                currentComponent.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Modified;
                //objectId = currentComponent.Id;
            }
            else
            {
                isNew = true;
                currentComponent.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added;
                //Setup(currentComponent);
            }
            product = parameters.FirstOrDefault(x => x is ProductDo) as ProductDo;
            if (parameters.FirstOrDefault(x => x is bool) is bool b)
                IsLocked = b;
            else
                IsLocked = true;
        }
    }

    public async override Task<bool> OnNavigatingFrom()
    {
        _appStateService.CurrentDetailViewObjectId = 0;
        _appStateService.CurrentDetailViewObjectType = null;

        return true;
    }

    #endregion
}

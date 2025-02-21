using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
public class ProductListViewModel : BaseListViewModel
{
    readonly IApiProductService _productService;

    public ProductListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiProductService productService)
        : base(logger, appStateService, navigationService)
    {
        _productService = productService;

        Products = new();
        Title = "Produkty";
    }

    #region properties

    public ObservableCollection<ProductDo> Products
    {
        get; set;
    }


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

    PermissionDo _Permission_IsMainMachine;
    public PermissionDo Permission_IsMainMachine
    {
        get => _Permission_IsMainMachine;
        set
        {
            _Permission_IsMainMachine = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_SerialNumber;
    public PermissionDo Permission_SerialNumber
    {
        get => _Permission_SerialNumber;
        set
        {
            _Permission_SerialNumber = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Category;
    public PermissionDo Permission_Category
    {
        get => _Permission_Category;
        set
        {
            _Permission_Category = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_SteelKind;
    public PermissionDo Permission_SteelKind
    {
        get => _Permission_SteelKind;
        set
        {
            _Permission_SteelKind = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region commands

    protected async override Task LoadList()
    {
        Products.Clear();

        var products = await ExecuteApiRequest(_productService.GetProducts, _appStateService.LoggedUser.Username);
        if (products != null && products.Any())
        {
            foreach (var product in products)
                Products.Add(product);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is ProductDo pdo)
            _navigationService.NavigateTo(typeof(ProductDetailViewModel), false, pdo.Id);
    }

    protected override void New() =>
        _navigationService.NavigateTo(typeof(ProductDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() =>
        await ExecuteApiRequest(_productService.DeleteProduct, _appStateService.LoggedUser.Username, (Selected as ProductDo)?.Id ?? 0);

    #endregion
}

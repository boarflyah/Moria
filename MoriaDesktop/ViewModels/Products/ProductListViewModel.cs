using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
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

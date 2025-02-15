using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
public sealed class ProductDetailViewModel : BaseDetailViewModel
{
    readonly IApiProductService _productService;

    public ProductDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService lockService,
        INavigationService navigationService, IApiProductService productService) 
        : base(logger, appStateService, lockService, navigationService)
    {
        _productService = productService;

        Title = "Nowy produkt";
    }

    #region properties


    private string _Name;
    [ObjectChangedValidate]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(nameof(Name));
        }
    }

    private string _Symbol;
    [ObjectChangedValidate]
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _IsMainMachine;
    [ObjectChangedValidate]
    public bool IsMainMachine
    {
        get => _IsMainMachine;
        set
        {
            _IsMainMachine = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SerialNumber;
    [ObjectChangedValidate]
    public string SerialNumber
    {
        get => _SerialNumber;
        set
        {
            _SerialNumber = value;
            RaisePropertyChanged(value);
        }
    }

    private CategoryDo _Category;
    [ObjectChangedValidate]
    public CategoryDo Category
    {
        get => _Category;
        set
        {
            _Category = value;
            RaisePropertyChanged(value);
        }
    }

    private SteelKindDo _SteelKind;
    [ObjectChangedValidate]
    public SteelKindDo SteelKind
    {
        get => _SteelKind;
        set
        {
            _SteelKind = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region methods

    public override Type GetModelType() => typeof(ProductDo);
    protected async override Task LoadObject()
    {
        Clear();

        var employee = await ExecuteApiRequest(_productService.GetProduct, _appStateService.LoggedUser.Username, objectId);
        if (employee != null)
            Setup(employee);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }
    protected async override Task<bool> SaveNewObject()
    {
        var product = GetDo() as ProductDo;
        var newObject = await _productService.CreateProduct(_appStateService.LoggedUser.Username, product);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }    
        return false;
    }
    protected async override Task<bool> UpdateExistingObject()
    {
        var product = GetDo() as ProductDo;
        var updated = await _productService.UpdateProduct(_appStateService.LoggedUser.Username, product);
        return updated != null;
    }
    public override void Clear()
    {
        Name = default;
        Symbol = default;
        IsMainMachine = default;
        SerialNumber = default;
        Category = null;
        SteelKind = null;
    }

    void Setup(ProductDo product)
    {
        Name = product.Name;
        Symbol = product.Symbol;
        IsMainMachine = product.IsMainMachine;
        SerialNumber = product.SerialNumber;
        Category = product.Category;
        SteelKind = product.SteelKind;
    }

    public override BaseDo GetDo()
        => new ProductDo()
        {
            Category = this.Category,
            IsMainMachine = this.IsMainMachine,
            Name = this.Name,
            SerialNumber = this.SerialNumber,
            SteelKind = this.SteelKind,
            Symbol = this.Symbol,
            Id = objectId
        };

    #endregion
}

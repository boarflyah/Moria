using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
public sealed class ProductDetailViewModel : BaseDetailViewModel
{
    public ProductDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService lockService, INavigationService navigationService) : base(logger, appStateService, lockService, navigationService)
    {
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

    protected override Type GetModelType() => typeof(ProductDo);
    protected override Task LoadObject() => throw new NotImplementedException();
    protected override Task<bool> SaveNewObject() => throw new NotImplementedException();
    protected override Task<bool> UpdateExistingObject() => throw new NotImplementedException();

    #endregion
}

using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.Views.Products;

public partial class ProductDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext;

    public ProductDetailView(ProductDetailViewModel vm, ILookupService lookupService)
    {
        InitializeComponent();
        DataContext = vm;
        _lookupService = lookupService;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as ProductDetailViewModel).Load();
    }

    private async void CategoryBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var category = await _lookupService.ShowLookup<CategoryDo>();
        if (category != null)
            (DataContext as ProductDetailViewModel).Category = category;
    }

    private async void SteelKindBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var steelKind = await _lookupService.ShowLookup<SteelKindDo>();
        if (steelKind != null)
            (DataContext as ProductDetailViewModel).SteelKind = steelKind;
    }
}

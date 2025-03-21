using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
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

    private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement fe && fe.DataContext is ComponentDo cdo)
            (DataContext as ProductDetailViewModel).OnComponentSelected(cdo);
    }

    private async void CategoryLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var category = await _lookupService.ShowLookup<CategoryDo>();
        if (category != null)
            (DataContext as ProductDetailViewModel).Category = category;
    }

    private async void SteelKindLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var steelKind = await _lookupService.ShowLookup<SteelKindDo>();
        if (steelKind != null)
            (DataContext as ProductDetailViewModel).SteelKind = steelKind;
    }
}

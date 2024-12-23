using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Products;

public partial class ProductDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;
    public ProductDetailView(ProductDetailViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as ProductDetailViewModel).Load();
    }
}

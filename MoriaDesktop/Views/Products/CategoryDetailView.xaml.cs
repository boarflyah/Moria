using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Products;

public partial class CategoryDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public CategoryDetailView(CategoryDetailViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel).Load();
    }

    private void ProductsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Products;

public partial class ProductListView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public ProductListView(ProductListViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void ProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();

        var vm = DataContext as ProductListViewModel;
        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            ProductDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Name.CanRead)
        {
            ProductDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_SerialNumber.CanRead)
        {
            ProductDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Category.CanRead)
        {
            ProductDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }
    }

}

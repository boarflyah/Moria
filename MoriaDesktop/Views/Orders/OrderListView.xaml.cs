using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktop.ViewModels.Products;

namespace MoriaDesktop.Views.Orders;

public partial class OrderListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => OrderDataGrid;

    public OrderListView(OrderListViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();

        var vm = DataContext as ProductListViewModel;
        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            OrderDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Name.CanRead)
        {
            OrderDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_SerialNumber.CanRead)
        {
            OrderDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Category.CanRead)
        {
            OrderDataGrid .Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }
        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private void OrderDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();
    }
}

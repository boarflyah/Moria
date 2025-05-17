using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;

namespace MoriaDesktop.Views.Orders;

public partial class ElectricalOrderItemListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => ElectricOrderItemDataGrid;

    public ElectricalOrderItemListView(ElectricalOrderItemListViewModel electricalOrderItemListViewModel)
    {
        InitializeComponent();
        DataContext = electricalOrderItemListViewModel;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();
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

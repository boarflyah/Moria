using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class ElectricalCabinetListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => CabinetsDataGrid;

    public ElectricalCabinetListView(ElectricalCabinetListViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ColorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as ElectricalCabinetListViewModel).OnLoaded();

        var vm = DataContext as ElectricalCabinetListViewModel;
        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            CabinetsDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }
        await (this as IListViewModelContent).SetColumnsOrder();

    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();
    }
}

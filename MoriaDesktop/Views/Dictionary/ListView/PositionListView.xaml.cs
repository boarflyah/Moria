using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class PositionListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => PositionDataGrid;

    public PositionListView(PositionListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }
    public PositionListView()
    {
        InitializeComponent();
    }

    private void PositionDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as PositionListViewModel).OnLoaded();

        var vm = DataContext as PositionListViewModel;
        if (vm != null && !vm.Permission_Name.CanRead)
        {
            PositionDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Code.CanRead)
        {
            PositionDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await (this as IListViewModelContent).SaveColumnsOrder();
    }
}

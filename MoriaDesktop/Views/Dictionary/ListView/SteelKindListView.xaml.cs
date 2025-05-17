using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class SteelKindListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => SteelKindDataGrid;

    public SteelKindListView(SteelKindListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }

    public SteelKindListView()
    {
        InitializeComponent();
    }

    private void SteelKindsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await(DataContext as SteelKindListViewModel).OnLoaded();

        var vm = DataContext as SteelKindListViewModel;
        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            SteelKindDataGrid.Columns[0].Visibility = Visibility.Collapsed; 
        }

        if (vm != null && !vm.Permission_Name.CanRead)
        {
            SteelKindDataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }
        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();
    }
}
